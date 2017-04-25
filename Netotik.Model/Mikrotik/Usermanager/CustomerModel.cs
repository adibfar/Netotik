using System.ComponentModel.DataAnnotations;

namespace Netotik.ViewModels.Mikrotik
{
    public class Usermanager_CustomerModel
    {
        public string id { get; set; }
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string login { get; set; }
        
    }
}
