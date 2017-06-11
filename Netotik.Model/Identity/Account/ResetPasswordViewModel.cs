using Netotik.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [StringLength(50, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "LengthError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Captions), Name = "Password")]
        [Remote("CheckPassword", "Remote", AreaReference.UseRoot, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "PasswordEasy", HttpMethod = "POST")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Captions), Name = "ConfirmPassword")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ConfirmPasswordNotValid")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}