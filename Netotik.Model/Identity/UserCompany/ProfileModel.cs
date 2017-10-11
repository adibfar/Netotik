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

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class ProfileModel
    {
        public long Id { get; set; }

        public bool IsBanned { get; set; }
        public long UserResellerId { get; set; }

        public bool? EmailConfirmed { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [System.Web.Mvc.Remote("IsCompanyPhoneNumberAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id,Reseller_Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        [System.Web.Mvc.Remote("IsCompanyNationalCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id,Reseller_Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string NationalCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [System.Web.Mvc.Remote("IsCompanyEmailAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id,Reseller_Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(6, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "CompanyName")]
        [RegularExpression(@"(^[a-zA-Z0-9]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "JustEnglishNumeric")]
        [System.Web.Mvc.Remote("IsCompanyCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id,Reseller_Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string CompanyCode { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ImageProfile")]
        public System.Web.HttpPostedFileBase ImageAvatar { get; set; }


        public string[] ClientPermissionNames { get; set; }
        public string[] CompanyPermissionNames { get; set; }

        public string ZarinPalMerchantId { get; set; }
    }
}
