using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class UserRouterTelegramMap : EntityTypeConfiguration<UserRouterTelegram>
    {
        public UserRouterTelegramMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Relationships
            this.HasRequired(t => t.UserRouter)
                .WithOptional(t => t.UserRouterTelegram);

        }
    }
}
