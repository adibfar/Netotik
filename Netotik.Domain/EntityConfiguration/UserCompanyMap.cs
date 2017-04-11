using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class UserCompanyMap : EntityTypeConfiguration<UserCompany>
    {
        public UserCompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Relationships
            this.HasRequired(t => t.User)
                .WithOptional(t => t.UserCompany);

            this.HasRequired(t => t.UserReseller)
                .WithMany(t => t.UserCompanies)
                .HasForeignKey(t => t.UserResellerId)
                .WillCascadeOnDelete(false);


        }
    }
}
