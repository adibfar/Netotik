using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ReturnRequestMap : EntityTypeConfiguration<ReturnRequest>
    {
        public ReturnRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ReasonForReturn)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ReturnRequest");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderItemId).HasColumnName("OrderItemId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ReasonForReturn).HasColumnName("ReasonForReturn");
            this.Property(t => t.UserComment).HasColumnName("UserComment");
            this.Property(t => t.StaffComment).HasColumnName("StaffComment");
            this.Property(t => t.ReturnRequestStatus).HasColumnName("ReturnRequestStatus");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.EditDate).HasColumnName("EditDate");

            // Relationships
            this.HasRequired(t => t.OrderItem)
                .WithMany(t => t.ReturnRequests)
                .HasForeignKey(d => d.OrderItemId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ReturnRequests)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);

        }
    }
}
