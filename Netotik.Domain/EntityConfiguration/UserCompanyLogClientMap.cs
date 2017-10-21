using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class UserRouterLogClientMap : EntityTypeConfiguration<UserRouterLogClient>
    {
        public UserRouterLogClientMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Relationships
            this.HasRequired(t => t.UserRouter)
                .WithMany(t => t.UserRouterLogClients)
                .HasForeignKey(t => t.UserRouterId)
                .WillCascadeOnDelete(false);

        }
    }
}
