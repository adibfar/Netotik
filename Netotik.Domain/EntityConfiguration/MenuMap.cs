using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.Description)
                .HasMaxLength(500);
            
            // Relationships
            this.HasOptional(t => t.Parent)
                .WithMany(t => t.SubMenues)
                .HasForeignKey(d => d.ParentId);


            this.HasRequired(t => t.Language)
          .WithMany(t => t.Menus)
          .HasForeignKey(d => d.LanguageId)
          .WillCascadeOnDelete(false);

        }
    }
}
