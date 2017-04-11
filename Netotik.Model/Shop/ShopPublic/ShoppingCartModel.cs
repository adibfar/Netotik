using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.ShopPublic
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OffPrice { get; set; }
        public decimal TotalPtice { get; set; }

    }
}
