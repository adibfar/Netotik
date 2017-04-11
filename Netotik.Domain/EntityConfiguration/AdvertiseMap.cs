using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class AdvertiseMap : EntityTypeConfiguration<Advertise>
    {
        public AdvertiseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Relationships
            this.HasRequired(t => t.Picture)
                .WithMany(t => t.Advertises)
                .HasForeignKey(d => d.PictureId)
                .WillCascadeOnDelete(false);

        }
    }
}
