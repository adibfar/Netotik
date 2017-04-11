using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;

namespace Netotik.ViewModels.Shop.ShippingByWeight
{
    public class ShippingByWeightModel
    {
        public int? Id { get; set; }
        public int ShippingMethodId { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "FromWeight")]
        public int? FromWeight { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ToWeight")]
        public int? ToWeight { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "AddPrice")]
        public decimal AdditionalFixedPrice { get; set; }

    }
}
