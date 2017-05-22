using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Hotspot_WalledGardenModel
    {
        [AllowHtml]
        public string id { get; set; }
        public string action { get; set; }
        public string dst_host { get; set; }
        public string dst_port { get; set; }
        public string method { get; set; }
        public string src_address { get; set; }
        public string server { get; set; }
        public string path { get; set; }
        public string comment { get; set; }
        public string disabled { get; set; }
        public string hits { get; set; }

    }
}
