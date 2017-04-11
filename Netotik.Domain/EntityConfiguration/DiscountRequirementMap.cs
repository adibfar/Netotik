using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class DiscountRequirementMap : EntityTypeConfiguration<DiscountRequirement>
    {
        public DiscountRequirementMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DiscountRequirementRuleSystemName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("DiscountRequirement");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DiscountId).HasColumnName("DiscountId");
            this.Property(t => t.DiscountRequirementRuleSystemName).HasColumnName("DiscountRequirementRuleSystemName");

            // Relationships
            this.HasRequired(t => t.Discount)
                .WithMany(t => t.DiscountRequirements)
                .HasForeignKey(d => d.DiscountId)
                .WillCascadeOnDelete(false);

        }
    }
}
