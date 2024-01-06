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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Equals(builder.Entity<ReleaseProducts>()
                                .HasKey(rp => new { rp.ReleaseId, rp.ProductId }));

            builder.Equals(builder.Entity<DeliveryProducts>()
                                .HasKey(rp => new { rp.DeliveryId, rp.ProductId }));

            base.OnModelCreating(builder);
        }
    }
}
