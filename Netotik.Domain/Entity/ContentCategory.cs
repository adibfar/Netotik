using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Netotik.Domain.Entity
{
    public partial class ContentCategory
    {
        public ContentCategory()
        {
            this.Contents = new List<Content>();
            this.SubCategories = new List<ContentCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ContentCategory Parent { get; set; }
        public int? ParentId { get; set; }
        public ICollection<ContentCategory> SubCategories { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}
