using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Router_EthernetModel
    {
        [AllowHtml]
        public string id { get; set; }
        public string name { get; set; }
        public string default_name { get; set; }
        public string mtu { get; set; }
        public string mac_address { get; set; }
        public string orig_mac_address { get; set; }
        public string arp { get; set; }
        public string arp_timeout { get; set; }
        public string loop_protect { get; set; }
        public string loop_protect_status { get; set; }
        public string loop_protect_send_interval { get; set; }
        public string loop_protect_disable_time { get; set; }
        public string disable_running_check { get; set; }
        public string auto_negotiation { get; set; }
        public string advertise { get; set; }
        public string full_duplex { get; set; }
        public string tx_flow_control { get; set; }
        public string rx_flow_control { get; set; }
        public string cable_settings { get; set; }
        public string speed { get; set; }
        public string running { get; set; }
        public string disabled { get; set; }
       

    }
}
