using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Shop.ProductAdmin
{
    public class TableProductModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string imgName { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public int PictureCount { get; set; }
    }
}
