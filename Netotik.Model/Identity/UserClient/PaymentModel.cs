using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class PaymentModel
    { 
        [AllowHtml]
        public string id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string user { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Creator")]
        public string customer { get; set; }

        public string currency { get; set; }
        public string method { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Price")]
        public string price { get; set; }
        public string trans_start { get; set; }
        public string trans_status { get; set; }
        public string result_code { get; set; }
        public string result_msg { get; set; }
        public string trans_end { get; set; }

    }
}
