using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class PaymentModel
    { 
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string user { get; set; }

        public string customer { get; set; }

        public string currency { get; set; }
        public string method { get; set; }
        public string price { get; set; }
        public string trans_start { get; set; }
        public string trans_status { get; set; }
        public string result_code { get; set; }
        public string result_msg { get; set; }
        public string trans_end { get; set; }

    }
}
