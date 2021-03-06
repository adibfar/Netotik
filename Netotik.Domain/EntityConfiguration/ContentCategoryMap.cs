using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ContentCategoryMap : EntityTypeConfiguration<ContentCategory>
    {
        public ContentCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.HasOptional(t => t.Parent)
                .WithMany(t => t.SubCategories)
                .HasForeignKey(d => d.ParentId);

            this.HasRequired(t => t.Language)
             .WithMany(t => t.ContentCategories)
             .HasForeignKey(d => d.LanguageId)
             .WillCascadeOnDelete(false);


        }
    }
}
