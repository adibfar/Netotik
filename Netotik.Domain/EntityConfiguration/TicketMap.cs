using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id);

            this.Property(t => t.Title)
                .HasMaxLength(300)
                .IsRequired();

            this.Property(t => t.Description);


            this.HasRequired(t => t.UserCreated)
                .WithMany(t => t.IssuesCreated)
                .HasForeignKey(d => d.CreatedUserId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.LastResponseUser)
               .WithMany(t => t.IssuesResponsed)
               .HasForeignKey(d => d.LastResponseUserId)
               .WillCascadeOnDelete(false);

        }
    }
}
