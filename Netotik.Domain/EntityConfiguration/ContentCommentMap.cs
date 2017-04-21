using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class ContentCommentMap : EntityTypeConfiguration<ContentComment>
    {
        public ContentCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .HasMaxLength(500)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Email)
                .IsRequired();

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.ContentComments)
                .HasForeignKey(d => d.ContentId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.User)
                .WithMany(t => t.ContentComments)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.Comment)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.CommentId);

        }
    }
}
