using Microsoft.EntityFrameworkCore;
using OrdersService.Persistence.Entities;

namespace OrdersService.Persistence;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options)
{
    private const string Schema = "orders";
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();
        
        base.OnModelCreating(modelBuilder);
    }
}