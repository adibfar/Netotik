using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ImageGalleryItemMap : EntityTypeConfiguration<ImageGalleryItem>
    {
        public ImageGalleryItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ImageGalleryItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContentId).HasColumnName("ContentId");
            this.Property(t => t.PictureId).HasColumnName("PictureId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.ImageGalleryItems)
                .HasForeignKey(d => d.ContentId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Picture)
                .WithMany(t => t.ImageGalleryItems)
                .HasForeignKey(d => d.PictureId)
                .WillCascadeOnDelete(false);

        }
    }
}
