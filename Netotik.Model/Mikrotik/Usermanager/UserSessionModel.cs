using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Usermanager_UserSessionModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "ایجاد کننده")]
        public string customer { get; set; }
        [Display(Name = "نام کاربری")]
        public string user { get; set; }
        [Display(Name = "nas port")]
        public string nas_port { get; set; }
        public string nas_port_type { get; set; }
        public string nas_port_id { get; set; }
        public string calling_station_id { get; set; }
        public string acct_session_id { get; set; }
        [Display(Name = "آدرس IP")]
        public string user_ip { get; set; }
        public string host_ip { get; set; }
        public string status { get; set; }
        [Display(Name = "از(زمان)")]
        public string from_time { get; set; }
        [Display(Name = "تا (زمان)")]
        public string till_timeT { get; set; }
        [Display(Name = "از(زمان)")]
        public string from_timeT { get; set; }
        [Display(Name = "تا (زمان)")]
        public string till_time { get; set; }
        [Display(Name = "نحوه قطع شدن")]
        public string terminate_cause { get; set; }
        [Display(Name = "زمان اتصال")]
        public string uptime { get; set; }
        [Display(Name = "دانلود")]
        public string download { get; set; }
        [Display(Name = "آپلود")]
        public string upload { get; set; }
        [Display(Name = "فعال؟")]
        public string active { get; set; }
               
    }
}
