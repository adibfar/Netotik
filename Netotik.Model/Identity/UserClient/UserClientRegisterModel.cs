using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class UserClientRegisterModel
    {
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        public string MobileNumber { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [EmailAddress(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Username")]
        public string Username { get; set; }

        [Range(minimum: 13, maximum: 120, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RangeError")]
        [Display(ResourceType = typeof(Captions), Name = "Age")]
        public int? Age { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "Password")]
        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(4, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Compare("Password", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ConfirmPasswordNotValid")]
        [Display(ResourceType = typeof(Captions), Name = "ConfirmPassword")]
        public string PasswordConfirm { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "BirthDate")]
        public DateTime BirthDate { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Sex")]
        public Sex Sex { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        [MaxLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [System.Web.Mvc.Remote("IsNationalCodeValid", "Remote", System.Web.Mvc.AreaReference.UseRoot, HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string NationalCode { get; set; }
    }


    public enum Sex : short
    {
        [Display(ResourceType = typeof(Captions), Name = "Male")]
        Male = 0,
        [Display(ResourceType = typeof(Captions), Name = "Female")]
        Female
    }

}
