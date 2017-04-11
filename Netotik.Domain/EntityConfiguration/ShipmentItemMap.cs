using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ShipmentItemMap : EntityTypeConfiguration<ShipmentItem>
    {
        public ShipmentItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ShipmentItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShipmentId).HasColumnName("ShipmentId");
            this.Property(t => t.OrderItemId).HasColumnName("OrderItemId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.WarehouseId).HasColumnName("WarehouseId");

            // Relationships
            this.HasRequired(t => t.OrderItem)
                .WithMany(t => t.ShipmentItems)
                .HasForeignKey(d => d.OrderItemId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Shipment)
                .WithMany(t => t.ShipmentItems)
                .HasForeignKey(d => d.ShipmentId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Warehouse)
                .WithMany(t => t.ShipmentItems)
                .HasForeignKey(d => d.WarehouseId)
                .WillCascadeOnDelete(false);

        }
    }
}
