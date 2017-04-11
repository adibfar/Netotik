using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ShippingMethodMap : EntityTypeConfiguration<ShippingMethod>
    {
        public ShippingMethodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.BasePrice).HasPrecision(18, 0);


            this.HasOptional(t => t.Picture)
                .WithMany(t => t.ShippingMethodes)
                .HasForeignKey(t => t.PictureId);
        }
    }
}
