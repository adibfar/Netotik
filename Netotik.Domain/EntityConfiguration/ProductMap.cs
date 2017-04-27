using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.MetaKeywords)
                .HasMaxLength(400);

            this.Property(t => t.Price)
                .HasPrecision(18, 0);


            this.HasOptional(t => t.Manufacturer)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ManufacturerId);

            this.HasOptional(t => t.Picture)
                .WithMany(t => t.BgProducts)
                .HasForeignKey(d => d.PictureId);


            this.HasMany(t => t.Pictures)
                .WithMany(t => t.Products)
                .Map(m =>
                {
                    m.ToTable("ProductPictureMap");
                    m.MapLeftKey("ProductId");
                    m.MapRightKey("PictureId");

                });

        }
    }
}
