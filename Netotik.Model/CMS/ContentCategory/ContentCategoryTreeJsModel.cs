using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;

namespace Netotik.ViewModels.CMS.ContentCategory
{
    public class ContentCategoryTreeJsModel
    {
        public ContentCategoryTreeJsModel()
        {
            Childs = new HashSet<ContentCategoryTreeJsModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }

        public ICollection<ContentCategoryTreeJsModel> Childs { get; set; }

    }
}
