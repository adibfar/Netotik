using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class InboxMessageMap : EntityTypeConfiguration<InboxMessage>
    {
        public InboxMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.Message)
                .HasMaxLength(1500);

            // Table & Column Mappings
            this.ToTable("InboxMessage");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
