using Netotik.Resources;
using System.ComponentModel.DataAnnotations;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class UserRegisterModel
    {
        [Display(Name = "ایجاد کننده")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string customer { get; set; }
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Password")]
        public string password { get; set; }
        [DataType(DataType.Password)]
        //[Compare("password", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ConfirmPasswordNotValid")]
        [Display(ResourceType = typeof(Captions), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        public string shared_users { get; set; }
        public string wireless_psk { get; set; }
        public string wireless_enc_key { get; set; }
        public string wireless_enc_algo { get; set; }

        [Display(Name = "فعال")]
        public string disabled { get; set; }

        public string caller_id { get; set; }
        [Display(Name = "نام")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string first_name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string last_name { get; set; }
        [Display(Name = "شماره تماس")]
        public string phone { get; set; }
        [Display(Name = "آدرس")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string location { get; set; }
        [Display(Name = "ایمیل")]
        public string email { get; set; }
        public string ip_address { get; set; }
        [Display(Name = "توضیحات")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string comment { get; set; }
        [Display(Name = "نام تعرفه")]
        public string profile { get; set; }
        public string NationalCode { get; set; }
        public string registration_date { get; set; }
        public string reg_key { get; set; }
        public bool SendSmsNow { get; set; }
    }
}
