using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class ProfileLimitionModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "ProfileName")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string profile { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "LimitationName")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string limitation { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "FromTime")]
        public string from_time { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "TillTime")]
        public string till_time { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "WeekDays")]
        public string weekdays { get; set; }
        

    }
}
