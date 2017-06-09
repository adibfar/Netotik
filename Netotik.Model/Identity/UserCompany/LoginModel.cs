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
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Captions))]
        [Display(Name = "UserName", ResourceType = typeof(Captions))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Captions))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Captions))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Captions))]
        public bool RememberMe { get; set; }

    }
}
