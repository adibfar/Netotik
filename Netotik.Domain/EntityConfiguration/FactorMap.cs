using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class FactorMap : EntityTypeConfiguration<Factor>
    {
        public FactorMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.HasRequired(x => x.PaymentType)
                .WithMany(x => x.Factores)
                .HasForeignKey(x => x.PaymentTypeId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.User)
                .WithMany(x => x.Factores)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
