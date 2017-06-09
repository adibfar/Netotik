using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class IndexSectionMap : EntityTypeConfiguration<IndexSection>
    {
        public IndexSectionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

           this.HasRequired(t => t.Language)
          .WithMany(t => t.Sections)
          .HasForeignKey(d => d.LanguageId)
          .WillCascadeOnDelete(false);
        }
    }
}
