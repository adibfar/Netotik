using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class LimitionModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string name { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Creator")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string owner { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "DownloadTrafficLimit")]
        public string download_limit { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "UploadTrafficLimit")]
        public string upload_limit { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "DownloadUploadTrafficLimit")]
        public string transfer_limit { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "ConnectionOnlineTime")]
        public string uptime_limit { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "UploadSpeedLimit")]
        public string rate_limit_rx { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "DownloadSpeedLimit")]
        public string rate_limit_tx { get; set; }
        public string rate_limit_min_tx { get; set; }
        public string group_name { get; set; }
        public string ip_pool { get; set; }
        public string address_list { get; set; }
        
    }
}
