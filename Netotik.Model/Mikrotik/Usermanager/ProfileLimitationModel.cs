using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Usermanager_ProfileLimitionModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "نام پروفایل")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string profile { get; set; }
        [Display(Name = "محدودیت")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string limitation { get; set; }
        [Display(Name = "از ساعت")]
        public string from_time { get; set; }
        [Display(Name = "تا ساعت")]
        public string till_time { get; set; }
        [Display(Name = "روزهای هفته")]
        public string weekdays { get; set; }
        

    }
}
