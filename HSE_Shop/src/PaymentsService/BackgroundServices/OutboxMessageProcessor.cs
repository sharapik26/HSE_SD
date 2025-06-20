using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentsService.Persistence;
using Shared.Events;
using System.Text.Json;

namespace PaymentsService.BackgroundServices;

public class OutboxMessageProcessor(IServiceProvider serviceProvider, ILogger<OutboxMessageProcessor> logger) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessOutboxMessages(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task ProcessOutboxMessages(CancellationToken stoppingToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        
        var messages = await dbContext.OutboxMessages
            .Where(m => m.ProcessedOn == null)
            .OrderBy(m => m.OccurredOn)
            .Take(20)
            .ToListAsync(stoppingToken);

        if (!messages.Any())
        {
            return;
        }
        
        logger.LogInformation("Обнаружено {Count} сообщений в Outbox сервиса Payments для обработки.", messages.Count);

        foreach (var message in messages)
        {
            try
            {
                if (message.Type == nameof(PaymentResultEvent))
                {
                    var paymentResultEvent = JsonSerializer.Deserialize<PaymentResultEvent>(message.Data);
                    if (paymentResultEvent != null)
                    {
                        await publishEndpoint.Publish(paymentResultEvent, stoppingToken);
                        logger.LogInformation("Сообщение о результате платежа {MessageId} отправлено.", message.Id);
                    }
                }
                else
                {
                    logger.LogWarning("Обнаружено сообщение неизвестного типа в Outbox: {MessageType}", message.Type);
                }
                
                message.ProcessedOn = DateTime.UtcNow;
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обработке сообщения {MessageId} из Outbox сервиса Payments.", message.Id);
            }
        }
    }
}