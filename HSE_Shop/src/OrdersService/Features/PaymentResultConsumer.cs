using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrdersService.Persistence;
using OrdersService.Persistence.Entities;
using Shared.Events;

namespace OrdersService.Features;

public class PaymentResultConsumer(OrdersDbContext dbContext, ILogger<PaymentResultConsumer> logger) 
    : IConsumer<PaymentResultEvent>
{
    public async Task Consume(ConsumeContext<PaymentResultEvent> context)
    {
        var message = context.Message;
        logger.LogInformation("Получено событие PaymentResultEvent для заказа: {OrderId}", message.OrderId);

        var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == message.OrderId);

        if (order == null)
        {
            logger.LogError("Заказ с Id {OrderId} не найден.", message.OrderId);
            return;
        }

        var oldStatus = order.Status;
        order.Status = message.IsSuccess ? OrderStatus.Finished : OrderStatus.Cancelled;

        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Статус заказа {OrderId} обновлен с '{OldStatus}' на '{NewStatus}'. Причина: {Reason}",
            order.Id,
            oldStatus,
            order.Status,
            message.IsSuccess ? "Оплата прошла успешно" : message.FailureReason
        );
    }
}