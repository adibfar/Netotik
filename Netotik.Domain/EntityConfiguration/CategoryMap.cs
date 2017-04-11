using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
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
            this.ToTable("Category");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParentCategoryId).HasColumnName("ParentCategoryId");
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

            // Relationships
            this.HasMany(t => t.Discounts)
                .WithMany(t => t.Categories)
                .Map(m =>
                    {
                        m.ToTable("DiscountAppliedToCategories");
                        m.MapLeftKey("CategoryId");
                        m.MapRightKey("DiscountId");
                    });

            this.HasOptional(t => t.Parent)
                .WithMany(t => t.SubCategories)
                .HasForeignKey(d => d.ParentCategoryId);


            this.HasOptional(t => t.Picture)
                .WithMany(t => t.categories)
                .HasForeignKey(t => t.PictureId);

        }
    }
}
