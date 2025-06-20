using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OrdersService.Features;
using OrdersService.Persistence;
using OrdersService.Persistence.Entities;
using Shared.Events;
using Xunit;

namespace OrdersService.UnitTests;

public class PaymentResultConsumerTests
{
    private readonly DbContextOptions<OrdersDbContext> _dbOptions;
    private readonly Mock<ILogger<PaymentResultConsumer>> _loggerMock;

    public PaymentResultConsumerTests()
    {
        _dbOptions = new DbContextOptionsBuilder<OrdersDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _loggerMock = new Mock<ILogger<PaymentResultConsumer>>();
    }

    private ConsumeContext<PaymentResultEvent> CreateConsumeContext(PaymentResultEvent message)
    {
        var contextMock = new Mock<ConsumeContext<PaymentResultEvent>>();
        contextMock.Setup(c => c.Message).Returns(message);
        return contextMock.Object;
    }

    [Fact]
    public async Task Consume_WhenPaymentIsSuccessful_UpdatesOrderStatusToFinished()
    {
        var orderId = Guid.NewGuid();
        await using var dbContext = new OrdersDbContext(_dbOptions);
        var order = new Order { Id = orderId, Status = OrderStatus.New, UserId = Guid.NewGuid() };
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();

        var consumer = new PaymentResultConsumer(dbContext, _loggerMock.Object);
        var paymentEvent = new PaymentResultEvent { OrderId = orderId, IsSuccess = true };
        var consumeContext = CreateConsumeContext(paymentEvent);
        
        await consumer.Consume(consumeContext);
        
        var updatedOrder = await dbContext.Orders.FindAsync(orderId);
        Assert.NotNull(updatedOrder);
        Assert.Equal(OrderStatus.Finished, updatedOrder.Status);
    }

    [Fact]
    public async Task Consume_WhenPaymentIsUnsuccessful_UpdatesOrderStatusToCancelled()
    {
        var orderId = Guid.NewGuid();
        await using var dbContext = new OrdersDbContext(_dbOptions);
        var order = new Order { Id = orderId, Status = OrderStatus.New, UserId = Guid.NewGuid() };
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();

        var consumer = new PaymentResultConsumer(dbContext, _loggerMock.Object);
        var paymentEvent = new PaymentResultEvent { OrderId = orderId, IsSuccess = false };
        var consumeContext = CreateConsumeContext(paymentEvent);
        
        await consumer.Consume(consumeContext);
        
        var updatedOrder = await dbContext.Orders.FindAsync(orderId);
        Assert.NotNull(updatedOrder);
        Assert.Equal(OrderStatus.Cancelled, updatedOrder.Status);
    }

    [Fact]
    public async Task Consume_WhenOrderNotFound_DoesNotThrowException()
    {
        var nonExistentOrderId = Guid.NewGuid();
        await using var dbContext = new OrdersDbContext(_dbOptions);
        var consumer = new PaymentResultConsumer(dbContext, _loggerMock.Object);
        var paymentEvent = new PaymentResultEvent { OrderId = nonExistentOrderId, IsSuccess = true };
        var consumeContext = CreateConsumeContext(paymentEvent);
        
        var exception = await Record.ExceptionAsync(() => consumer.Consume(consumeContext));
        
        Assert.Null(exception);
    }
}