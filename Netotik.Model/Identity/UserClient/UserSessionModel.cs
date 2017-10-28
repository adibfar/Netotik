using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class UserSessionModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Creator")]
        public string customer { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        public string user { get; set; }
        public string nas_port { get; set; }
        public string nas_port_type { get; set; }
        public string nas_port_id { get; set; }
        public string calling_station_id { get; set; }
        public string acct_session_id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "IpAddress")]
        public string user_ip { get; set; }
        public string host_ip { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Status")]
        public string status { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "FromTime")]
        public string from_time { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "TillTime")]
        public string till_timeT { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "FromTime")]
        public string from_timeT { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "TillTime")]
        public string till_time { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "TerminateCause")]
        public string terminate_cause { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "OnlineTime")]
        public string uptime { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Download")]
        public string download { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Upload")]
        public string upload { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Active")]
        public string active { get; set; }

               
    }
}
