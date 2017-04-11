using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ProductGalleryMap : EntityTypeConfiguration<ProductGallery>
    {
        public ProductGalleryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.MetaKeywords)
                .HasMaxLength(400);

            this.HasOptional(t => t.Picture)
                .WithMany(t => t.BgProductGalleries)
                .HasForeignKey(d => d.PictureId);


            this.HasMany(t => t.Pictures)
                .WithMany(t => t.ProductGalleries)
                .Map(m =>
                {
                    m.ToTable("GalleryProductPictureMap");
                    m.MapLeftKey("GalleryProductId");
                    m.MapRightKey("PictureId");

                });

        }
    }
}
