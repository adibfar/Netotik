using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netotik.Domain.Entity
{
    public class ContentTag
    {
        public ContentTag()
        {
            this.Contents = new List<Content>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}
