using CDRPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CDRPlatform.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<CallDetailRecord> CallDetailRecord { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CallDetailRecord>()
                .Property(c => c.Cost)
                .HasPrecision(20,10);
        }
    }
}
