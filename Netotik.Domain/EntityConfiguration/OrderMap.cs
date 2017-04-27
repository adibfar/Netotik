using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.TotalTaxPrice).HasPrecision(18, 0);
            this.Property(t => t.TotalPrice).HasPrecision(18, 0);
            this.Property(t => t.PaymentPrice).HasPrecision(18, 0);
            this.Property(t => t.TotalProductDiscountPrice).HasPrecision(18, 0);
            this.Property(t => t.TotalFactorDiscountPrice).HasPrecision(18, 0);
            this.Property(t => t.TotalFactorCouponDiscountPrice).HasPrecision(18, 0);

            // Relationships
            this.HasRequired(t => t.Address)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.AddressId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.User)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.UserId);

        }
    }
}
