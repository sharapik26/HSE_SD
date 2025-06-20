using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrdersService.Persistence;
using Shared.Events;
using System.Text.Json;

namespace OrdersService.BackgroundServices;

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
        var dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
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

        logger.LogInformation("Обнаружено {Count} сообщений в Outbox для обработки.", messages.Count);

        foreach (var message in messages)
        {
            try
            {
                if (message.Type == nameof(OrderCreatedEvent))
                {
                    var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message.Data);
                    if (orderCreatedEvent != null)
                    {
                        await publishEndpoint.Publish(orderCreatedEvent, stoppingToken);
                    }
                }

                message.ProcessedOn = DateTime.UtcNow;
                await dbContext.SaveChangesAsync(stoppingToken);

                logger.LogInformation("Сообщение {MessageId} обработано и отправлено.", message.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обработке сообщения {MessageId} из Outbox.", message.Id);
            }
        }
    }
}