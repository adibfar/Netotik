using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ManufacturerMap : EntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.MetaTitle)
                .HasMaxLength(400);

            this.Property(t => t.MetaKeywords)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("Manufacturer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.MetaTitle).HasColumnName("MetaTitle");
            this.Property(t => t.MetaKeywords).HasColumnName("MetaKeywords");
            this.Property(t => t.MetaDescription).HasColumnName("MetaDescription");
            this.Property(t => t.PictureId).HasColumnName("PictureId");
            this.Property(t => t.ShowOnHomePage).HasColumnName("ShowOnHomePage");
            this.Property(t => t.IsPublished).HasColumnName("IsPublished");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.DisplayOrder).HasColumnName("DisplayOrder");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.EditDate).HasColumnName("EditDate");

            this.HasOptional(t => t.Picture)
                .WithMany(t => t.manufacturers)
                .HasForeignKey(t => t.PictureId);

        }
    }
}
