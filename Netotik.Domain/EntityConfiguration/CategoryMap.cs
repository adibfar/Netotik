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
