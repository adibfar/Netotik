using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.CMS.IndexSection
{
    public class IndexSectionItem
    {
        public int? Id { get; set; }
        public long RowNumber { get; set; }
        public string Title { get; set; }
        public string FlagLanguage { get; set; }
        public int Order { get; set; }
    }
}
