using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.CMS.ContentCategory
{
    public class ContentCategoryItem
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string Name { get; set; }
        public string FlagLanguage { get; set; }
        public string Parent { get; set; }

    }
}
