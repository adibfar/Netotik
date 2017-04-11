using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class SliderMap : EntityTypeConfiguration<Slider>
    {
        public SliderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.ToTable("Slider");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BigText).HasColumnName("BigText");
            this.Property(t => t.SmallText).HasColumnName("SmallText");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Order).HasColumnName("Order");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.PictureId).HasColumnName("PictureId");

            // Relationships
            this.HasRequired(t => t.Picture)
                .WithMany(t => t.Sliders)
                .HasForeignKey(d => d.PictureId)
                .WillCascadeOnDelete(false);

        }
    }
}
