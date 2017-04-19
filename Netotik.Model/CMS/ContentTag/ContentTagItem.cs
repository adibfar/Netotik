using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Netotik.ViewModels.CMS.ContentTag
{
    public class ContentTagItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RowNumber { get; set; }
    }
}
