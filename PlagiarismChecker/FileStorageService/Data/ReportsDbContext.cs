using FileStorageService.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageService.Data
{
    public class ReportsDbContext : DbContext
    {
        public ReportsDbContext(DbContextOptions<ReportsDbContext> options) : base(options) { }

        public DbSet<Report> Reports { get; set; }
    }
}
