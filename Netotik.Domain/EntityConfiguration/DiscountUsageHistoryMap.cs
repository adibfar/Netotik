using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class DiscountUsageHistoryMap : EntityTypeConfiguration<DiscountUsageHistory>
    {
        public DiscountUsageHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("DiscountUsageHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DiscountId).HasColumnName("DiscountId");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasRequired(t => t.Discount)
                .WithMany(t => t.DiscountUsageHistories)
                .HasForeignKey(d => d.DiscountId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Order)
                .WithMany(t => t.DiscountUsageHistories)
                .HasForeignKey(d => d.OrderId)
                .WillCascadeOnDelete(false);

        }
    }
}
