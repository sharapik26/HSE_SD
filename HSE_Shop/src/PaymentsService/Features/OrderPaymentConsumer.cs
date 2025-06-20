using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentsService.Persistence;
using PaymentsService.Persistence.Entities;
using Shared.Events;
using System.Text.Json;

namespace PaymentsService.Features;

public class OrderPaymentConsumer(PaymentsDbContext dbContext, ILogger<OrderPaymentConsumer> logger) 
    : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        logger.LogInformation("Получен OrderCreatedEvent для OrderId: {OrderId}", message.OrderId);

        if (await dbContext.InboxMessages.AnyAsync(im => im.MessageId == context.MessageId))
        {
            logger.LogWarning("MessageId {MessageId} уже обработан.", context.MessageId);
            return;
        }
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            dbContext.InboxMessages.Add(new InboxMessage
            {
                MessageId = context.MessageId.Value,
                ProcessedOn = DateTime.UtcNow
            });
            
            var account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.UserId == message.UserId);

            PaymentResultEvent paymentResultEvent;

            if (account == null)
            {
                logger.LogWarning("Аккаунт не найден для UserId: {UserId}", message.UserId);
                paymentResultEvent = CreateFailureEvent(message, "Аккаунт не найден.");
            }
            else if (account.Balance < message.Amount)
            {
                logger.LogWarning("Недостаточно денег у UserId: {UserId}. Баланс: {Balance}, Необходимо: {Amount}", 
                    message.UserId, account.Balance, message.Amount);
                paymentResultEvent = CreateFailureEvent(message, "Недостаточно денег.");
            }
            else
            {
                account.Balance -= message.Amount;
                logger.LogInformation("Списание средств прошло успешно для UserId: {UserId}. Новый баланс: {Balance}",
                    message.UserId, account.Balance);
                paymentResultEvent = new PaymentResultEvent
                {
                    OrderId = message.OrderId,
                    UserId = message.UserId,
                    IsSuccess = true
                };
            }
            
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.UtcNow,
                Type = nameof(PaymentResultEvent),
                Data = JsonSerializer.Serialize(paymentResultEvent)
            };
            dbContext.OutboxMessages.Add(outboxMessage);

            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            logger.LogInformation("Процесс оплаты для OrderId {OrderId} завершен. Результат: {Result}", 
                message.OrderId, paymentResultEvent.IsSuccess ? "Успех" : "Неудача");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Ошибка при оплате для OrderId: {OrderId}. Повторная отправка.", message.OrderId);
            throw; 
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Случилась непредвиденная ошибка при оплате для OrderId: {OrderId}", message.OrderId);
            throw;
        }
    }

    private PaymentResultEvent CreateFailureEvent(OrderCreatedEvent message, string reason)
    {
        return new PaymentResultEvent
        {
            OrderId = message.OrderId,
            UserId = message.UserId,
            IsSuccess = false,
            FailureReason = reason
        };
    }
}