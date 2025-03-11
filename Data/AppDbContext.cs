using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace YourNamespace.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Reading> Readings { get; set; }
    }
}