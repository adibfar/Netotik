using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Shop.ProductPanel
{
    public class ProductGridModel
    {
        public IEnumerable<BoxProductModel> Products;

        public int Page { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public int? ManufacturerId { get; set; }
        public string Manufacturer { get; set; }

    }
}
