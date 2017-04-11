using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ProductComment
    {
        public ProductComment()
        {
            Comments = new List<ProductComment>();
        }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CommentText { get; set; }
        public int ProductId { get; set; }
        public int? CommentId { get; set; }
        public CommentStatus Status { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual ProductComment Comment { get; set; }
        public virtual ICollection<ProductComment> Comments { get; set; }
    }


    public enum CommentStatus : short
    {
        Accepted = 0,
        WaitForAccept = 1,
        NotAccept = 2,
        Delete = 3
    }

}
