using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Common.Rss
{
    public class RssItemViewModel
    {
        public int ContentId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
