using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class UserModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Creator")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string customer { get; set; }
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        public string username { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Password")]
        public string password { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SharedUsers")]
        public string shared_users { get; set; }
        public string wireless_psk { get; set; }
        public string wireless_enc_key { get; set; }
        public string wireless_enc_algo { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "LastConnection")]
        public string last_seen { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "LastConnection")]
        public string last_seenT { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Active")]
        public string active { get; set; }
        public string incomplete { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Active")]
        public string disabled { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "PlanName")]
        [RegularExpression(@"(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string actual_profile { get; set; }
        public string caller_id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "FirstName")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string first_name { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "LastName")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string last_name { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Tel")]
        public string phone { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Address")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string location { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        public string email { get; set; }
        public string ip_address { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "OnlineTime")]
        public string uptime_used { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "DownloadUsed")]
        public string download_used { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "UploadUsed")]
        public string upload_used { get; set; }
        public string comment { get; set; }
        public string registration_date { get; set; }
        public string reg_key { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "BirthDate")]
        public string Birthday { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        [System.Web.Mvc.Remote("IsNationalCodeValid", "Remote", System.Web.Mvc.AreaReference.UseRoot, HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string NationalCode { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "IsMale")]
        public string IsMale { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "CreateDate")]
        public string CreateDate { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "MarriageDate")]
        public string MarriageDate { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Age")]
        public string Age { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "LastEdit")]
        public string EditDate { get; set; }

    }
}
