using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            this.HasMany(t => t.Issues)
                .WithMany(t => t.IssueRoles)
               .Map(m =>
               {
                   m.ToTable("IssueRoleMap");
                   m.MapLeftKey("RoleId");
                   m.MapRightKey("IssueId");
               });


        }
    }
}
