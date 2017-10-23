using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class ProfileModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string name { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Creator")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string owner { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Group")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string name_for_users { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "TimeValidity")]
        public string validity { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "StartAt")]
        public string starts_at { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Price")]
        public string price { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SharedUsers")]
        public string override_shared_users { get; set; } 
    }
}
