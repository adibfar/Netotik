using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Usermanager_CustomerModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string login { get; set; }
        
    }
}
