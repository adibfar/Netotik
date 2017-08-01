using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class CustomerModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string login { get; set; }
        
    }
}
