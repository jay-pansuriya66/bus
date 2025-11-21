using Microsoft.EntityFrameworkCore;

namespace bus.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<customer> Customers { get; set; }
        public DbSet<vehicle> Vehicles { get; set; }
        public DbSet<sales> Sales { get; set; }
        public DbSet<salesDto> salesDtos { get; set; }
    }
}
