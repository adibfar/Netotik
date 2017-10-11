using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class FactorSmsDetailMap : EntityTypeConfiguration<FactorSmsDetail>
    {
        public FactorSmsDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.HasRequired(x => x.Factor)
                .WithOptional(x => x.FactorSmsDetail);

        }
    }
}
