using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Relationships
            this.HasOptional(t => t.AddressCity)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.AddressCityId);


            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);


        }
    }
}
