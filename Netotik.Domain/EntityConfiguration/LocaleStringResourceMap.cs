using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class LocaleStringResourceMap : EntityTypeConfiguration<LocaleStringResource>
    {
        public LocaleStringResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.HasRequired(x => x.Language)
                .WithMany(x => x.LocaleStringResources)
                .HasForeignKey(x => x.LanguageId)
                .WillCascadeOnDelete(false);
        }
    }
}
