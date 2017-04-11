using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class FileMap : EntityTypeConfiguration<File>
    {
        public FileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.MimeType)
                .HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("File");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.MimeType).HasColumnName("MimeType");
            this.Property(t => t.OrginalName).HasColumnName("OrginalName");

            // Relationships
            this.HasMany(t => t.Contents)
                .WithMany(t => t.FilesAttach)
                .Map(m =>
                    {
                        m.ToTable("ContentFileMap");
                        m.MapLeftKey("FileId");
                        m.MapRightKey("ContentId");
                    });


            // Relationships
            this.HasMany(t => t.Issues)
                .WithMany(t => t.FilesAttach)
                .Map(m =>
                {
                    m.ToTable("IssueFileMap");
                    m.MapLeftKey("FileId");
                    m.MapRightKey("IssueId");
                });

            // Relationships
            this.HasMany(t => t.IssueTracks)
                .WithMany(t => t.FilesAttach)
                .Map(m =>
                {
                    m.ToTable("IssueTrackFileMap");
                    m.MapLeftKey("FileId");
                    m.MapRightKey("IssueTrackId");
                });


        }
    }
}
