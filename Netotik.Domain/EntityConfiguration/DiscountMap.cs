using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class DiscountMap : EntityTypeConfiguration<Discount>
    {
        public DiscountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CouponCode)
                .HasMaxLength(100);

            this.Property(t => t.DiscountAmount)
                .HasPrecision(18, 0);

            this.Property(t => t.MaximumDiscountAmount)
                .HasPrecision(18, 0);

            this.Property(t => t.DiscountAmount)
                .HasPrecision(18, 0);

            // Relationships
            this.HasMany(t => t.Manufacturers)
                .WithMany(t => t.Discounts)
                .Map(m =>
                    {
                        m.ToTable("DiscountAppliedToManufacturers");
                        m.MapLeftKey("DiscountId");
                        m.MapRightKey("ManufacturerId");
                    });

            this.HasMany(t => t.Products)
                .WithMany(t => t.Discounts)
                .Map(m =>
                    {
                        m.ToTable("DiscountAppliedToProduct");
                        m.MapLeftKey("DiscountId");
                        m.MapRightKey("ProductId");
                    });


        }
    }
}
