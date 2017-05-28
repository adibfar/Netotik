using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Hotspot_IPWalledGardenModel
    {
        [AllowHtml]
        public string id { get; set; }
        public string action { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string dst_host { get; set; }
        [RegularExpression(@"(^$)|(^[0-9]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string dst_port { get; set; }
        public string src_address { get; set; }
        public string server { get; set; }
        public string protocol { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string comment { get; set; }
        public string dst_address { get; set; }
        public string disabled { get; set; }
        public string hits { get; set; }
    }
}
