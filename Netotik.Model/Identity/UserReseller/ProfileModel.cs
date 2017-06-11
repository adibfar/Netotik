using Netotik.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserReseller
{
    public class ProfileModel
    {
        public bool? EmailConfirmed { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(6, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "CompanyName")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "JustEnglishNumeric")]
        [System.Web.Mvc.Remote("IsResellerCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string ResellerCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        //[System.Web.Mvc.Remote("IsPhoneNumberAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        //[System.Web.Mvc.Remote("IsNationalCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "کد ملی اشتباه است یا قبلا در سیستم ثبت شده است.")]
        public string NationalCode { get; set; }


        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        //[Remote("IsAdminEmailAvailable", "Remote", AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string Email { get; set; }
    }
}