using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class ProfileLimitionModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "نام پروفایل")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string profile { get; set; }
        [Display(Name = "محدودیت")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string limitation { get; set; }
        [Display(Name = "از ساعت")]
        public string from_time { get; set; }
        [Display(Name = "تا ساعت")]
        public string till_time { get; set; }
        [Display(Name = "روزهای هفته")]
        public string weekdays { get; set; }
        

    }
}
