using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class SmsLogMap : EntityTypeConfiguration<SmsLog>
    {
        public SmsLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.HasOptional(t => t.User)
               .WithMany(t => t.SmsLogs)
               .HasForeignKey(d => d.UserId)
               .WillCascadeOnDelete(false);

        }
    }
}
