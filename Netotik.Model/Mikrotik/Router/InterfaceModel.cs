namespace Netotik.ViewModels.Mikrotik
{
    public class Router_InterfaceModel
    {
        public string id { get; set; }

        public string name { get; set; }

        public string default_name { get; set; }

        public string type { get; set; }

        public string mtu { get; set; }

        public string actual_mtu { get; set; }
        public string mac_address { get; set; }
        public string last_link_up_time { get; set; }

        public string link_downs { get; set; }
        public string rx_byte { get; set; }
        public string tx_byte { get; set; }
        public string rx_packet { get; set; }
        public string tx_packet { get; set; }
        public string rx_drop { get; set; }
        public string tx_drop { get; set; }
        public string rx_error { get; set; }
        public string tx_error { get; set; }
        public string running { get; set; }
        public string disabled { get; set; }
    }
}
