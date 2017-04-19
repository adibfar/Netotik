using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ContentTagMap : EntityTypeConfiguration<ContentTag>
    {
        public ContentTagMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);


            // Table & Column Mappings
            this.ToTable("ContentTag");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Text");

            // Relationships
            this.HasMany(t => t.Contents)
                .WithMany(t => t.ContentTages)
                .Map(m =>
                    {
                        m.ToTable("ContentTagMap");
                        m.MapLeftKey("ContentTagId");
                        m.MapRightKey("ContentId");
                    });


        }
    }
}
