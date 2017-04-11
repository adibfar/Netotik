using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;

namespace Netotik.ViewModels.ShopPublic
{
    public class ShippingListModel
    {
        public int Id { get; set; }
        public string imgName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool PriceAfterRecive { get; set; }
        public bool Selected { get; set; }
    }
}
