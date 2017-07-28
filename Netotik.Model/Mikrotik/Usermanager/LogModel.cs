using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Usermanager_LogModel
    {
        [AllowHtml]
        public string id { get; set; }
        public string customer { get; set; }
        public string user_orig { get; set; }
        public string nas_port { get; set; }
        public string nas_port_type { get; set; }
        public string nas_port_id { get; set; }
        public string calling_station_id { get; set; }
        public string user_ip { get; set; }
        public string status { get; set; }
        public string host_ip { get; set; }
        public string time { get; set; }
        public string description { get; set; }
    }
}
