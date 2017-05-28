using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Hotspot_IPBindingsModel
    {
        [AllowHtml] 
        public string id { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string address { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string comment { get; set; }
        [RegularExpression(@"(^$)|(^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$)", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotValidError")]
        public string mac_address { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: ]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string server { get; set; }
        [RegularExpression(@"(^$)|(^[0-9.]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string to_address { get; set; }

        public string type { get; set; }
        public string disabled { get; set; }
        public string hits { get; set; }
        

    }
}
