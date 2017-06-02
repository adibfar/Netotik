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
using Netotik.Common.Filters;

namespace Netotik.ViewModels.Identity.UserAdmin
{
    public class ProfileModel
    {

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotValidError")]
        [Remote("IsAdminPhoneNumberAvailable", "Remote", AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotValidError")]
        [Remote("IsAdminEmailAvailable", "Remote", AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string Email { get; set; }

        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [DisplayName("نام کاربری")]
        [StringLength(256, ErrorMessage = "نام کاربری نباید کمتر از 5 حرف و بیتشر از 256 حرف باشد", MinimumLength = 5)]
        [Remote("IsUserNameAvailable", "Remote", AreaReference.UseRoot, AdditionalFields = "Id", ErrorMessage = "این نام کاربری قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "فقط از حروف انگلیسی و اعداد استفاده کنید")]
        public string UserName { get; set; }

        [DisplayName("فیس بوک")]
        [DataType(DataType.Url,ErrorMessage ="لینک وارد شده نامعتبر است.")]
        public string Facebook { get; set; }

        [DisplayName("لینکداین")]
        [DataType(DataType.Url, ErrorMessage = "لینک وارد شده نامعتبر است.")]
        public string LinkedIn { get; set; }

        [DisplayName("توییتر")]
        [DataType(DataType.Url, ErrorMessage = "لینک وارد شده نامعتبر است.")]
        public string Twitter { get; set; }

        [DisplayName("اینستاگرام")]
        [DataType(DataType.Url, ErrorMessage = "لینک وارد شده نامعتبر است.")]
        public string Instagram { get; set; }

        [DisplayName("وب سایت")]
        [DataType(DataType.Url, ErrorMessage = "لینک وارد شده نامعتبر است.")]
        public string Website { get; set; }


        public string ShortBio { get; set; }
    }
}
