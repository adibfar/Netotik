using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.CMS.Content
{
    public class ContentItem
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string Title { get; set; }
        public bool AllowComments { get; set; }
        public String LastEdited { get; set; }
        public string UserCreator { get; set; }
        public string PublishDate { get; set; }
        public ContentStatus status { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public string ImageFileName { get; set; }
        public string FlagLanguage { get; set; }
    }
}
