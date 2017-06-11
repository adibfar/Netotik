using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Hotspot_UsersModel
    {
        [AllowHtml]
        public string id { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string name { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string profile { get; set; }
        public string routes { get; set; }
        public string email { get; set; }
        public string limit_uptime { get; set; }
        public string limit_bytes_in { get; set; }
        public string limit_bytes_out { get; set; }
        public string limit_bytes_total { get; set; }
        public string disabled { get; set; }
        public string uptime { get; set; }
        public string comment { get; set; }
        public string bytes_in { get; set; }
        public string bytes_out { get; set; }

        public string packets_in { get; set; }
        public string packets_out { get; set; }
    }
}
