using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class AddressCityMap : EntityTypeConfiguration<City>
    {
        public AddressCityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("AddressCity");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StateId).HasColumnName("AddressStateId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");

            // Relationships
            this.HasRequired(t => t.State)
                .WithMany(t => t.Cities)
                .HasForeignKey(d => d.StateId)
                .WillCascadeOnDelete(false);

        }
    }
}
