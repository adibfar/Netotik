using Netotik.Resources;
using System.ComponentModel.DataAnnotations;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class ProfileLimitionCreateModel
    {
        public string Profile_id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "ProfileName")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string profilelimition_profile { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "LimitationName")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string profilelimition_limitation { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "FromTime")]
        public string profilelimition_from_time { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "TillTime")]
        public string profilelimition_till_time { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "WeekDays")]
        public string profilelimition_weekdays { get; set; }



        //---------------------------------------------


        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string profile_name { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Creator")]
        public string profile_owner { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Group")]
        public string profile_name_for_users { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "TimeValidity")]
        public string profile_validity { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "StartAt")]
        public string profile_starts_at { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Price")]
        public string profile_price { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SharedUsers")]
        public string profile_override_shared_users { get; set; }


        //------------------------------------------------
        public string limitation_id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string limition_name { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Creator")]
        public string limition_owner { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "DownloadTrafficLimit")]
        public string limition_download_limit { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "UploadTrafficLimit")]
        public string limition_upload_limit { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "DownloadUploadTrafficLimit")]
        public string limition_transfer_limit { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "ConnectionOnlineTime")]
        public string limition_uptime_limit { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "UploadSpeedLimit")]
        public string limition_rate_limit_rx { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "DownloadSpeedLimit")]
        public string limition_rate_limit_tx { get; set; }
        public string limition_rate_limit_min_tx { get; set; }
        public string limition_group_name { get; set; }
        public string limition_ip_pool { get; set; }
        public string limition_address_list { get; set; }
    }
}
