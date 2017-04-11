using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;

namespace Netotik.ViewModels.Shop.ShippingMethod
{
    public class ShippingMethodModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        [Remote("IsNameExist", "ShopShippingMethod", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "BasePrice")]
        public decimal BasePrice { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        [UIHint("Multiline")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Active")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsDefault")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public bool IsDefault { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsPasKeraye")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public bool PriceAfterRecive { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Order")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public int DisplayOrder { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}
