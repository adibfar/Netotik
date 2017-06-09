using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel;

namespace Netotik.ViewModels.Shop.ProductAdmin
{
    public class ProductModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Brand")]
        public int? ManufacturerId { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Tax")]
        public int? TaxId { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DeliveryDate")]
        public int? DeliveryDateId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "FreeShipping")]
        public bool IsFreeShipping { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "QuantityWareHouse")]
        public int Quantity { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "MinNotifyQuantityWareHouse")]
        public int MinQuantityNotify { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "IsMinNotifyQuantityWareHouse")]
        public bool IsMinQuantityNotify { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "DisplayQuantityForUser")]
        public bool DisplayQuantityForUser { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "OrderMaximumQuantity")]
        public int OrderMaximumQuantity { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "BodyOverview")]
        [UIHint("Multiline")]
        public string ShortDescription { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        [UIHint("SummerNoteEditor")]
        [AllowHtml]
        public string FullDescription { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AdminComment")]
        [UIHint("Multiline")]
        public string AdminComment { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ShowOnHomePage")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool ShowOnHomePage { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaKeywords")]
        [MaxLength(400, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [UIHint("Multiline")]
        public string MetaKeywords { get; set; }

        [UIHint("Multiline")]
        [Display(ResourceType = typeof(Captions), Name = "MetaDescription")]
        public string MetaDescription { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "MetaTitle")]
        [MaxLength(400, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [UIHint("Multiline")]
        public string MetaTitle { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AllowComment")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool AllowComment { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AllowViewComments")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool AllowViewComments { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DisableBuy")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool DisableBuyButton { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "CallForPrice")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool CallForPrice { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "CanBuyIfNotInStock")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool CanBuyIfNotInStock { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "PriceProduct")]
        public decimal? Price { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MaxOffPrice")]
        public decimal? MaxOffPrice { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "WeightProduct")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public int Weight { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(ResourceType = typeof(Captions), Name = "StartDatePublish")]
        public DateTime? AvailableStartDate { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(ResourceType = typeof(Captions), Name = "EndDatePublish")]
        public DateTime? AvailableEndDate { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsPublish")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool IsPublished { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }

        public int[] CategoryIds { get; set; }
        public int[] DiscountIds { get; set; }
        public int[] ColorIds { get; set; }

        public int[] SizeIds { get; set; }
    }
}
