using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ShippingByWeightMap : EntityTypeConfiguration<ShippingByWeight>
    {
        public ShippingByWeightMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.AdditionalFixedPrice).HasPrecision(18, 0);


            // Relationships
            this.HasRequired(t => t.ShippingMethod)
                .WithMany(t => t.ShippingByWeights)
                .HasForeignKey(d => d.ShippingMethodId)
                .WillCascadeOnDelete(false);

        }
    }
}
