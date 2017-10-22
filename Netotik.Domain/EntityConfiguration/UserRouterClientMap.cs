using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class UserRouterClientMap : EntityTypeConfiguration<UserRouterClient>
    {
        public UserRouterClientMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Relationships
            this.HasRequired(t => t.UserRouter)
                .WithMany(t => t.UserRouterClients)
                .HasForeignKey(t => t.UserRouterId)
                .WillCascadeOnDelete(false);

        }
    }
}
