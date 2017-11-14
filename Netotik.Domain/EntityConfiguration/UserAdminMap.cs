using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class UserAdminMap : EntityTypeConfiguration<UserAdmin>
    {
        public UserAdminMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.HasRequired(t => t.User)
                .WithOptional(t => t.UserAdmin)
                .WillCascadeOnDelete(true);

        }
    }
}
