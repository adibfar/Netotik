using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Shop.ShippingByWeight
{
    public class TableShippingByWeightModel
    {
        public int Id { get; set; }
        public int ShippingMethodId { get; set; }
        public int? FromWeight { get; set; }
        public int? ToWeight { get; set; }
        public decimal AdditionalFixedPrice { get; set; }
    }
}
