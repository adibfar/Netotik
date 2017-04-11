using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.Shop.Discount
{
    public class DiscountModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        [UIHint("Multiline")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DiscountType")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public DiscountType DiscountType { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "UsePercentage")]
        public bool UsePercentage { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DiscountPercentage")]
        [Range(1, 100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RangeError")]
        public int? DiscountPercentage { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DiscountAmount")]
        public decimal? DiscountAmount { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "MaximumDiscountAmount")]
        public decimal? MaximumDiscountAmount { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(ResourceType = typeof(Captions), Name = "StartDatePublish")]
        public DateTime? StartDate { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(ResourceType = typeof(Captions), Name = "EndDatePublish")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "CouponRequire")]
        public bool RequiersCouponCode { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "CouponCode")]
        public string CouponCode { get; set; }

           [Display(ResourceType = typeof(Captions), Name = "MaximumDiscountQuantity")]
        public int? MaximumDiscountQuantity { get; set; }

    }
}
