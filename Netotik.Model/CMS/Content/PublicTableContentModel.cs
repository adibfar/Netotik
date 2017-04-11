using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.CMS.Content
{
    public class PublicTableContentModel
    {
        public IEnumerable<PublicItemContentModel> Contents;

        public int Page { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public int? TagId { get; set; }
        public string Tag { get; set; }
    }
}

