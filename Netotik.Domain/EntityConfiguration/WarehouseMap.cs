using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class WarehouseMap : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Warehouse");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("AdminComment");
            this.Property(t => t.AddressStateId).HasColumnName("AddressStateId");
            this.Property(t => t.AddressCityId).HasColumnName("AddressCityId");

            this.HasRequired(t => t.AddressState)
                .WithMany(t => t.WareHoses)
                .HasForeignKey(t => t.AddressStateId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.AddressCity)
                .WithMany(t => t.WareHoses)
                .HasForeignKey(t => t.AddressCityId)
                .WillCascadeOnDelete(false);
        }
    }
}
