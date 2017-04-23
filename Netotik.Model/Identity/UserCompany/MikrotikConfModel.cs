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

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class MikrotikConfModel
    {
        public long Id { get; set; }

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
        [Display(Name = "فعال سازی CLoud ")]
        public bool cloud { get; set; }
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "Userman Customer")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string Userman_Customer { get; set; }
    }
}
