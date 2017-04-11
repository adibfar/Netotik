using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.CMS.Content
{
    public class TableContentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool AllowComments { get; set; }
        public DateTime LastEdited { get; set; }
        public string LastUserEdit { get; set; }
        public ContentStatus status { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
    }

  

}
