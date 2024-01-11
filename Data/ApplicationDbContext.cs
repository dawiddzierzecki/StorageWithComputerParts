using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoragewithComputerParts.Models;

namespace StoragewithComputerParts.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Protocol> Protocols { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<DeliveryProducts> DeliveryProducts { get; set; }
        public DbSet<ReleaseProducts> ReleaseProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReleaseProducts>()
                .HasKey(rp => new { rp.ReleaseId, rp.ProductId });

            modelBuilder.Entity<DeliveryProducts>()
                .HasKey(dp => new { dp.DeliveryId, dp.ProductId });

            modelBuilder.Entity<ReleaseProducts>()
                .HasOne(rp => rp.Release)
                .WithMany(r => r.ReleaseProducts)
                .HasForeignKey(rp => rp.ReleaseId);

            modelBuilder.Entity<ReleaseProducts>()
                .HasOne(rp => rp.Product)
                .WithMany(p => p.ReleaseProducts)
                .HasForeignKey(rp => rp.ProductId);

            modelBuilder.Entity<DeliveryProducts>()
                .HasOne(dp => dp.Delivery)
                .WithMany(d => d.DeliveryProducts)
                .HasForeignKey(dp => dp.DeliveryId);

            modelBuilder.Entity<DeliveryProducts>()
                .HasOne(dp => dp.Product)
                .WithMany(p => p.DeliveryProducts)
                .HasForeignKey(dp => dp.ProductId);

            modelBuilder.Entity<Stock>()
                .HasKey(s => s.ProductId);
        }
    }
}
