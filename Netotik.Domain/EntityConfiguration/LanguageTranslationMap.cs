using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class LanguageTranslationMap : EntityTypeConfiguration<LanguageTranslation>
    {
        public LanguageTranslationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.LanguageTranslationes)
                .HasForeignKey(d => d.LanguageId)
                .WillCascadeOnDelete(true);


        }
    }
}
