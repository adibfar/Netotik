using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class PictureMap : EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.MimeType)
                .HasMaxLength(50);

            this.Property(t => t.OrginalName);

            // Table & Column Mappings
            this.ToTable("Picture");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.MimeType).HasColumnName("MimeType");
            this.Property(t => t.OrginalName).HasColumnName("OrginalName");

            //// Relationships
            //this.HasMany(t => t.Products)
            //    .WithMany(t => t.Pictures)
            //    .Map(m =>
            //        {
            //            m.ToTable("ProductPictureMap");
            //            m.MapLeftKey("PictureId");
            //            m.MapRightKey("ProductId");
            //        });


            //// Relationships
            //this.HasMany(t => t.ProductGalleries)
            //    .WithMany(t => t.Pictures)
            //    .Map(m =>
            //    {
            //        m.ToTable("ProductGalleryPictureMap");
            //        m.MapLeftKey("PictureId");
            //        m.MapRightKey("ProductId");
            //    });


        }
    }
}
