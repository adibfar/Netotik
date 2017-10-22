using Netotik.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Netotik.Domain.EntityConfiguration
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserName") { IsUnique = false }));

            this.Property(t => t.Email)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Email") { IsUnique = false }))
                .HasMaxLength(1000);

            this.Property(t => t.FirstName)
                .HasMaxLength(100);

            this.Property(t => t.LastName)
                .HasMaxLength(100);

            this.Property(t => t.LastLoginIpAddress)
                .HasMaxLength(1000);

            this.HasMany(t => t.IssuesCreated)
                .WithRequired(t => t.UserCreated)
                .HasForeignKey(t => t.CreatedUserId)
                .WillCascadeOnDelete(false);

            this.HasMany(t => t.IssuesResponsed)
                .WithRequired(t => t.LastResponseUser)
                .HasForeignKey(t => t.LastResponseUserId)
                .WillCascadeOnDelete(false);
            
            this.HasOptional(t => t.Picture)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.PictureId);
            
        }
    }
}
