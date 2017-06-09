using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Hotspot_IPBindingsModel
    {
        [AllowHtml] 
        public string id { get; set; }
        public string address { get; set; }
        public string comment { get; set; }
        [RegularExpression(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string mac_address { get; set; }
        public string server { get; set; }
        public string to_address { get; set; }
        public string type { get; set; }
        public string disabled { get; set; }
        public string hits { get; set; }
        

    }
}
