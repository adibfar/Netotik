using Netotik.Domain.Entity;
using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.UserAdmin
{
    public class AdminAddModel 
    {
        public int? Id { get; set; }

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

        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotValidError")]
        [System.Web.Mvc.Remote("IsAdminPhoneNumberAvailable", "Remote",System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotValidError")]
        [System.Web.Mvc.Remote("IsAdminEmailAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string Email { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(6, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        [System.Web.Mvc.Remote("IsUserNameAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(6, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Password")]
        [System.Web.Mvc.Remote("CheckPassword", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "گذرواژه سخت تری را انتخاب کنید.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Compare("Password", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ConfirmPasswordNotValid")]
        [Display(ResourceType = typeof(Captions), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        public long[] RoleIds { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ImageProfile")]
        public System.Web.HttpPostedFileBase ImageAvatar { get; set; }

    }
}
