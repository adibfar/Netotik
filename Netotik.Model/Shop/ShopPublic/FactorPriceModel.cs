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
    public class FactorPriceModel
    {
        public decimal TotalPrice { get; set; }
        public decimal TotalOffPrice { get; set; }
        public decimal TotalTaxPrice { get; set; }
        public decimal TotalShipmentPrice { get; set; }
        public decimal TotalPaymentPrice { get; set; }
    }
}
