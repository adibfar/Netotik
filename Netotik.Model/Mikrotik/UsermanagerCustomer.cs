using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Mikrotik
{
    public class UsermanagerCustomer
    {
        public string id { get; set; }
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string login { get; set; }
        
    }
}
