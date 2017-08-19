using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class UserCompanyTelegramMap : EntityTypeConfiguration<UserCompanyTelegram>
    {
        public UserCompanyTelegramMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Relationships
            this.HasRequired(t => t.UserCompany)
                .WithOptional(t => t.UserCompanyTelegram);

        }
    }
}
