using Netotik.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Captions), Name = "OldPassword")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [StringLength(50, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "LengthError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Captions), Name = "NewPassword")]
        [Remote("CheckPassword", "Remote", AreaReference.UseRoot, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "PasswordEasy", HttpMethod = "POST")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Captions), Name = "ConfirmPassword")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ConfirmPasswordNotValid")]
        public string ConfirmPassword { get; set; }
    }
}