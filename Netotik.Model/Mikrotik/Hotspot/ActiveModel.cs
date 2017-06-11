using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Hotspot_ActiveModel
    {
        [AllowHtml]
        public string id { get; set; }
        public string Flags { get; set; }
        public string mac_address { get; set; }
        public string address { get; set; }
        public string to_address { get; set; }
        public string server { get; set; }
        public string uptime { get; set; }
        public string keepalive_timeout { get; set; }
        public string session_time_left { get; set; }
        public string disabled { get; set; }
        public string login_by { get; set; }
        public string limit_bytes_in { get; set; }
        public string limit_bytes_out { get; set; }
        public string limit_bytes_total { get; set; }
        public string radius { get; set; }
        public string user { get; set; }

    }
}
