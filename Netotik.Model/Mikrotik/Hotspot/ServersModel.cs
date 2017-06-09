using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Hotspot_ServerModel
    {
        [AllowHtml]
        public string id { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string name { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string Hinterface { get; set; }
        public string address_pool { get; set; }
        public string profile { get; set; }
        public string idle_timeout { get; set; }
        public string keepalive_timeout { get; set; }
        public string login_timeout { get; set; }
        public string addresses_per_mac { get; set; }
        public string disabled { get; set; }
        public string proxy_status { get; set; }

    }
}
