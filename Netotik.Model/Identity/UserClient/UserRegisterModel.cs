using Netotik.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class UserRegisterModel
    {
        [Display(ResourceType = typeof(Captions), Name = "Creator")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string customer { get; set; }
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        public string username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Password")]
        public string password { get; set; }
        [DataType(DataType.Password)]
        //[Compare("password", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ConfirmPasswordNotValid")]
        [Display(ResourceType = typeof(Captions), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SharedUsers")]
        public string shared_users { get; set; }
        public string wireless_psk { get; set; }
        public string wireless_enc_key { get; set; }
        public string wireless_enc_algo { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Active")]
        public string disabled { get; set; }

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
        public string comment { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "PlanName")]
        public string profile { get; set; }
        public string registration_date { get; set; }
        public string reg_key { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SendSmsNow")]
        public bool SendSmsNow { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "BirthDate")]
        public DateTime Birthday { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        [System.Web.Mvc.Remote("IsNationalCodeValid", "Remote", System.Web.Mvc.AreaReference.UseRoot, HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string NationalCode { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "IsMale")]
        public bool IsMale { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "CreateDate")]
        public DateTime CreateDate { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "MarriageDate")]
        public DateTime MarriageDate { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Age")]
        public int? Age { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "LastEdit")]
        public DateTime EditDate { get; set; }
    }
}
