using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ContentMap : EntityTypeConfiguration<Content>
    {
        public ContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id);

            this.Property(t => t.Title)
                .IsRequired();

            this.Property(t => t.Body)
                .IsRequired();

            this.Property(t => t.BodyOverview)
                .IsRequired();

            this.Property(t => t.MetaKeywords)
                .HasMaxLength(400);

            this.Property(t => t.MetaTitle)
                .HasMaxLength(400);

            // Relationships
            this.HasOptional(t => t.Picture)
                .WithMany(t => t.Contents)
                .HasForeignKey(d => d.PictureId);

            this.HasRequired(t => t.UserCreated)
                .WithMany(t => t.ContentsCreated)
                .HasForeignKey(d => d.CreatedUserId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.UserEdited)
               .WithMany(t => t.ContentsEdited)
               .HasForeignKey(d => d.EditedUserId)
               .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Language)
             .WithMany(t => t.Contents)
             .HasForeignKey(d => d.LanguageId)
             .WillCascadeOnDelete(false);


        }
    }
}
