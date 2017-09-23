using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class UserCompanyLogClientMap : EntityTypeConfiguration<UserCompanyLogClient>
    {
        public UserCompanyLogClientMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Relationships
            this.HasRequired(t => t.UserCompany)
                .WithMany(t => t.UserCompanyLogClients)
                .HasForeignKey(t => t.UserCompanyId)
                .WillCascadeOnDelete(false);

        }
    }
}
