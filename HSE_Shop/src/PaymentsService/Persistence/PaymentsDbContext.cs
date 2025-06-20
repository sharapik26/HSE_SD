using Microsoft.EntityFrameworkCore;
using PaymentsService.Persistence.Entities;

namespace PaymentsService.Persistence;

public class PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : DbContext(options)
{
    private const string Schema = "payments";

    public DbSet<Account> Accounts { get; set; }
    public DbSet<InboxMessage> InboxMessages { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.Entity<Account>()
            .HasIndex(a => a.UserId)
            .IsUnique();
        
        modelBuilder.Entity<Account>()
            .Property(p => p.Version)
            .IsRowVersion();
            
        base.OnModelCreating(modelBuilder);
    }
}