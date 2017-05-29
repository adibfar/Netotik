using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Common.Meta
{
    public class MetaViewModel
    {
        public MetaViewModel()
        {
            OgTags = new List<string>();
        }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Conanical { get; set; }

        public string OgSiteName { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string OgType { get; set; }
        public string OgUrl { get; set; }
        public string OgImage { get; set; }
        public string OgImageType { get; set; }
        public string OgPublishTime { get; set; }
        public string OgModifiedTime { get; set; }
        public string OgArticleSection { get; set; }
        public IList<string> OgTags { get; set; }

    }
}
