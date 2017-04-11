using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class LinkMap : EntityTypeConfiguration<Link>
    {
        public LinkMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.ToTable("Link");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Order).HasColumnName("Order");

        }
    }
}
