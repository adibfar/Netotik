using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ContentComment
    {
        public ContentComment()
        {
            Comments = new List<ContentComment>();
        }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public long? UserId { get; set; }
        public string WebSite { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public int ContentId { get; set; }
        public int? CommentId { get; set; }
        public CommentStatus Status { get; set; }
        public virtual Content Content { get; set; }
        public virtual User User { get; set; }
        public virtual ContentComment Comment { get; set; }
        public virtual ICollection<ContentComment> Comments { get; set; }
    }
    public enum CommentStatus : short
    {
        Accepted = 0,
        WaitForAccept = 1,
        NotAccept = 2,
        Deleted = 3
    }
}
