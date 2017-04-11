using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;

namespace Netotik.ViewModels.Shop.ProductPanel
{
    public class ViewProductModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "Manufacturer")]
        public int? ManufacturerId { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Tax")]
        public int? TaxId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "DeliveryDate")]
        public int DeliveryDateId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "FreeShipping")]
        public bool IsFreeShipping { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "QuantityWareHouse")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "MinNotifyQuantityWareHouse")]
        public int MinQuantityNotify { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "IsMinNotifyQuantityWareHouse")]
        public bool IsMinQuantityNotify { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "DisplayQuantityForUser")]
        public bool DisplayQuantityForUser { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "DisplayQuantityForUser")]
        public int OrderMaximumQuantity { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(200, ErrorMessage = "حدااکثر 100 کاراکتر")]
        [MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
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
        [Required(ErrorMessage = "*")]
        public bool ShowOnHomePage { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaKeywords")]
        [MaxLength(400, ErrorMessage = "حدااکثر 400 کاراکتر")]
        [UIHint("Multiline")]
        public string MetaKeywords { get; set; }

        [UIHint("Multiline")]
        [Display(ResourceType = typeof(Captions), Name = "MetaDescription")]
        public string MetaDescription { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "MetaTitle")]
        [MaxLength(400, ErrorMessage = "حدااکثر 400 کاراکتر")]
        [UIHint("Multiline")]
        public string MetaTitle { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AllowComment")]
        [Required(ErrorMessage = "*")]
        public bool AllowComment { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AllowViewComments")]
        [Required(ErrorMessage = "*")]
        public bool AllowViewComments { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DisableBuy")]
        [Required(ErrorMessage = "*")]
        public bool DisableBuyButton { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "CallForPrice")]
        [Required(ErrorMessage = "*")]
        public bool CallForPrice { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "PriceProduct")]
        public decimal? Price { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "weightProduct")]
        [Required(ErrorMessage = "*")]
        public int Weight { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(ResourceType = typeof(Captions), Name = "StartDatePublish")]
        public DateTime? AvailableStartDate { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(ResourceType = typeof(Captions), Name = "EndDatePublish")]
        public DateTime? AvailableEndDate { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsPublish")]
        [Required(ErrorMessage = "*")]
        public bool IsPublished { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }


        public int[] DiscountIds { get; set; }
    }
}
