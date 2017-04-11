using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ProductAttributeValueMap : EntityTypeConfiguration<ProductAttributeValue>
    {
        public ProductAttributeValueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(500);


            // Relationships
            this.HasRequired(t => t.ProductAttribute)
                .WithMany(t => t.ProductAttributeValues)
                .HasForeignKey(d => d.ProductAttributeId)
                .WillCascadeOnDelete(false);

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductAttributeValues)
                .HasForeignKey(d => d.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
}
