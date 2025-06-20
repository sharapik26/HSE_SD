using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Persistence;
using OrdersService.Persistence.Entities;
using Shared.Events;
using System.Text.Json;

namespace OrdersService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(OrdersDbContext dbContext, ILogger<OrdersController> logger) : ControllerBase
{
    public record CreateOrderRequest(Guid UserId, decimal Amount, string Description);

    [HttpPost]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Amount = request.Amount,
            Description = request.Description,
            Status = OrderStatus.New,
            CreatedOn = DateTime.UtcNow
        };
        
        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            UserId = order.UserId,
            Amount = order.Amount,
            Description = order.Description
        };
        
        var outboxMessage = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            OccurredOn = DateTime.UtcNow,
            Type = nameof(OrderCreatedEvent),
            Data = JsonSerializer.Serialize(orderCreatedEvent)
        };
        
        await dbContext.Orders.AddAsync(order);
        await dbContext.OutboxMessages.AddAsync(outboxMessage);

        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Создан новый заказ {OrderId} для пользователя {UserId} в статусе '{Status}'",
            order.Id, order.UserId, order.Status);

        return CreatedAtAction(nameof(GetOrder), new { orderId = order.Id }, order);
    }

    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder(Guid orderId)
    {
        var order = await dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return NotFound($"Заказ с ID {orderId} не найден.");
        }

        return Ok(order);
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserOrders(Guid userId)
    {
        var orders = await dbContext.Orders
            .AsNoTracking()
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedOn)
            .ToListAsync();

        return Ok(orders);
    }
}