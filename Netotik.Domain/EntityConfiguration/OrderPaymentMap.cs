using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class OrderPaymentMap : EntityTypeConfiguration<OrderPayment>
    {
        public OrderPaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);


            // Relationships
            this.HasRequired(t => t.Order)
                .WithMany(t => t.OrderPayments)
                .HasForeignKey(d => d.OrderId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.PaymentType)
                .WithMany(t => t.OrderPayments)
                .HasForeignKey(d => d.PaymentTypeId)
                .WillCascadeOnDelete(false);

        }
    }
}
