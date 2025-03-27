using Microsoft.EntityFrameworkCore;
using VendSys.Models;

namespace VendSys.Data
{
    public class VdiDexDbContext : DbContext
    {
        public VdiDexDbContext(DbContextOptions<VdiDexDbContext> options)
            : base(options)
        {
        }

        public DbSet<DEXMeter> DEXMeters => Set<DEXMeter>();
        public DbSet<DEXLaneMeter> DEXLaneMeters => Set<DEXLaneMeter>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DEXMeter>()
                .HasMany(m => m.LaneMeters)
                .WithOne(l => l.DEXMeter!)
                .HasForeignKey(l => l.DEXMeterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}