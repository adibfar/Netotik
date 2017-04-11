using System.ComponentModel.DataAnnotations;

namespace Netotik.ViewModels.Identity.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "وارد کردن ایمیل ضروریست")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}