using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class Router_LogModel
    {
        [AllowHtml]
        public string id { get; set; }

        public string time { get; set; }

        public string topics { get; set; }

        public string message { get; set; }
    }
}
