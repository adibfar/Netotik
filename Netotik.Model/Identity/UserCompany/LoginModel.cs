using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Netotik.Resources.Messages))]
        [Display(Name = "UserName", ResourceType = typeof(Netotik.Resources.Captions))]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Netotik.Resources.Messages))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Netotik.Resources.Captions))]
        public string Password { get; set; }
        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }

    }
}
