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
    public class FactorModel
    {

        [Required(ErrorMessage = "نام خود را وارد کنید")]
        [MaxLength(100, ErrorMessage = "حدااکثر 100 کاراکتر")]
        [MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        public string coupon { get; set; }
        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید")]
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "کد پستی خود را وارد کنید")]
        [Display(ResourceType = typeof(Captions), Name = "PostalCode")]
        public long PostalCode { get; set; }
        
        [Required(ErrorMessage = "استان را نتخاب کنید.")]
        [Display(ResourceType = typeof(Captions), Name = "State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "شهر خود را انتخاب کنید.")]
        [Display(ResourceType = typeof(Captions), Name = "City")]
        public int CityId { get; set; }


        [Required(ErrorMessage = "آدرس خود را وارد کنید.")]
        [MaxLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Address")]
        public string Address { get; set; }


        [MaxLength(500, ErrorMessage = "حداکثر 500 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "شیوه ارسال را مشخص کنید.")]
        public int? ShipingMethodId { get; set; }

        [Required(ErrorMessage = "درگاه بانکی را مشخص کنید.")]
        public int? PaymentTypeId { get; set; }

        public string Email { get; set; }
    }
}
