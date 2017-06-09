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

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "ShowName")]
        public string ShowName { get; set; }

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

        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [Remote("IsAdminPhoneNumberAvailable", "Remote", AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [Remote("IsAdminEmailAvailable", "Remote", AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        [StringLength(50, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "LengthError", MinimumLength = 6)]
        [System.Web.Mvc.Remote("IsUserNameAvailable", "Remote", AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "JustEnglishNumeric")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Facebook")]
        [Url(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Facebook { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Linkedin")]
        [Url(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string LinkedIn { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Twitter")]
        [Url(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Twitter { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Instagram")]
        [Url(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Instagram { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Website")]
        [Url(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Website { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ShortBio")]
        public string ShortBio { get; set; }

        public IList<ProfileLanguageItem> Items { get; set; }

        public int[] LanguageIds { get; set; }

        public string[] ShowNames { get; set; }

        public string[] ShortBios { get; set; }

    }
}
