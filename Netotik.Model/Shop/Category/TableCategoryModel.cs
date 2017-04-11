using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Shop.Category
{
    public class TableCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublish { get; set; }
        public string Description { get; set; }
        public string imgName { get; set; }
    }
}
