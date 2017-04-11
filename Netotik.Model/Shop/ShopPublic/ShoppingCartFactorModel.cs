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
    public class ShoppingCartFactorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Manfacturer { get; set; }
        public string DeliveryDate { get; set; }
        public bool IsFreeShipping { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public int weight { get; set; }
        public decimal UnitOffPrice { get; set; }
        public decimal UnitTaxPrice { get; set; }
        public string imgName { get; set; }

    }
}
