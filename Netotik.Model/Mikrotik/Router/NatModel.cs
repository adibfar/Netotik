namespace Netotik.ViewModels.Mikrotik
{
    public class Router_NatModel
    {
        
        public string id { get; set; }
        public string to_ipaddress { get; set; }
        public string dst_ipaddress { get; set; }
        public string to_ports { get; set; }
        public string dst_port { get; set; }
        public string input_interface { get; set; }
        public string protocol { get; set; }
        public bool disabled { get; set; }
        public string comment { get; set; }
        public string position { get; set; }
    }
}
