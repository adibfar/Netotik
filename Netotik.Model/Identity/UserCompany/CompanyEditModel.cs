using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class CompanyEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "نام شرکت")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        [System.Web.Mvc.Remote("IsResellerCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string CompanyName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotValidError")]
        [System.Web.Mvc.Remote("IsPhoneNumberAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string MobileNumber { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotValidError")]
        [System.Web.Mvc.Remote("IsEmailAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string Email { get; set; }

        [MaxLength(10, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }

        [MaxLength(10, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "کد ملی")]
        [System.Web.Mvc.Remote("CheckMeliCode", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "کد ملی اشتباه می باشد.")]
        public string PersonCode { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "آدرس روتر")]
        public string R_Host { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "کاربر روتر")]
        public string R_User { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "گذرواژه روتر")]
        public string R_Password { get; set; }

        
        [Display(Name = "API Port")]
        public int R_Port { get; set; }
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "Userman Customer")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string Userman_Customer { get; set; }
    }
}
