using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Usermanager_CustomerModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string login { get; set; }
        
    }
}
