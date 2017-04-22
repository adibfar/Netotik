using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class TicketTrackMap : EntityTypeConfiguration<TicketTrack>
    {
        public TicketTrackMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id);

            this.Property(t => t.Description)
                .IsRequired();


            this.HasRequired(t => t.UserCreated)
                .WithMany(t => t.IssuesTracksResponsed)
                .HasForeignKey(d => d.CreatedUserId)
                .WillCascadeOnDelete(false);

        }
    }
}
