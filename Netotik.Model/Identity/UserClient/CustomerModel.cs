using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class CustomerModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string login { get; set; }
        public string password { get; set; }
        public string access { get; set; }
        public string backup_allowed { get; set; }
        public string time_zone { get; set; }
        public string permissions { get; set; }
        public string signup_allowed { get; set; }
        public bool disabled { get; set; }
        

    }
}
