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


        public DbSet<StoragewithComputerParts.Models.Contractor> Contractors { get; set; } = default!;
        public DbSet<StoragewithComputerParts.Models.Product> Products { get; set; } = default!;
        public DbSet<StoragewithComputerParts.Models.Delivery> Deliveries { get; set; } = default!;
        public DbSet<StoragewithComputerParts.Models.Protocol> Protocols { get; set; } = default!;
        public DbSet<StoragewithComputerParts.Models.Release> Releases { get; set; } = default!;
        public DbSet<StoragewithComputerParts.Models.Stock> Stocks { get; set; } = default!;
        public DbSet<StoragewithComputerParts.Models.DeliveryProducts> DeliveryProducts { get; set; } = default!;
        public DbSet<StoragewithComputerParts.Models.ReleaseProducts> ReleaseProducts { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Equals(builder.Entity<ReleaseProducts>()
                                .HasKey(rp => new { rp.ReleaseId, rp.ProductId }));

            builder.Equals(builder.Entity<DeliveryProducts>()
                                .HasKey(rp => new { rp.DeliveryId, rp.ProductId }));

            //builder.Entity<Product>()
            //    .HasOne(p => p.Stock)
            //    .WithOne(s => s.Product)
            //    .HasForeignKey<Stock>(s => s.ProductId);

            //builder.Entity<Product>()
            //    .HasMany(p => p.ReleaseProducts)

            //builder.Entity<ReleaseProducts>()
            //    .HasOne(rp => rp.Release)
            //    .WithMany(r => r.Product)
            //    .HasForeignKey(rp => rp.ReleaseId);

            //builder.Entity<ReleaseProducts>()
            //    .HasOne(rp => rp.Product)
            //    .WithMany

            //builder.Entity<DeliveryProducts>()
            //    .HasOne(rp => rp.Delivery)
            //    .WithMany(r => r.ReleaseProducts)
            //    .HasForeignKey(rp => rp.ReleaseId);

            builder.Equals(builder.Entity<Stock>()
                                .HasKey(s => s.ProductId));

            base.OnModelCreating(builder);
        }

    }
}
