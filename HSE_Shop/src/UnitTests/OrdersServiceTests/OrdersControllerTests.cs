using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OrdersService.Controllers;
using OrdersService.Persistence;
using OrdersService.Persistence.Entities;
using Shared.Events;
using System.Text.Json;
using Xunit;

namespace OrdersService.UnitTests;

public class OrdersControllerTests
{
    private readonly DbContextOptions<OrdersDbContext> _dbOptions;
    private readonly Mock<ILogger<OrdersController>> _loggerMock;

    public OrdersControllerTests()
    {
        _dbOptions = new DbContextOptionsBuilder<OrdersDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _loggerMock = new Mock<ILogger<OrdersController>>();
    }

    [Fact]
    public async Task CreateOrder_WithValidData_ReturnsCreatedAtAction()
    {
        await using var dbContext = new OrdersDbContext(_dbOptions);
        var controller = new OrdersController(dbContext, _loggerMock.Object);
        var createRequest = new OrdersController.CreateOrderRequest(
            UserId: Guid.NewGuid(),
            Amount: 1500.50m,
            Description: "ласт додеп"
        );
        
        var result = await controller.CreateOrder(createRequest);
        
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdOrder = Assert.IsType<Order>(createdAtActionResult.Value);

        Assert.Equal(createRequest.UserId, createdOrder.UserId);
        Assert.Equal(createRequest.Amount, createdOrder.Amount);
        Assert.Equal(createRequest.Description, createdOrder.Description);
        Assert.Equal(OrderStatus.New, createdOrder.Status);
    }

    [Fact]
    public async Task CreateOrder_WithValidData_CreatesOutboxMessage()
    {
        await using var dbContext = new OrdersDbContext(_dbOptions);
        var controller = new OrdersController(dbContext, _loggerMock.Object);
        var createRequest = new OrdersController.CreateOrderRequest(
            UserId: Guid.NewGuid(),
            Amount: 200,
            Description: "няшный заказ"
        );
        
        await controller.CreateOrder(createRequest);
        
        var outboxMessage = await dbContext.OutboxMessages.SingleOrDefaultAsync();
        Assert.NotNull(outboxMessage);

        var domainEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(outboxMessage.Data);
        Assert.NotNull(domainEvent);
        
        Assert.Equal(nameof(OrderCreatedEvent), outboxMessage.Type);
        Assert.Equal(createRequest.UserId, domainEvent.UserId);
        Assert.Equal(createRequest.Amount, domainEvent.Amount);
        Assert.Equal(createRequest.Description, domainEvent.Description);
    }
}