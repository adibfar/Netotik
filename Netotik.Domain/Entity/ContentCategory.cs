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
        [DisplayName("نام")]
        public string Name { get; set; }
        [DisplayName("توضیح")]
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        [DisplayName("نمایش در منو")]
        public bool showInMenu { get; set; }
        public ContentCategory Parent { get; set; }
        public int? ParentId { get; set; }
        public ICollection<ContentCategory> SubCategories { get; set; }
        public Nullable<int> PictureId { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public Picture Picture { get; set; }
    }
}
