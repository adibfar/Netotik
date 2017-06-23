using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class UserModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "ایجاد کننده")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string customer { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        [Display(Name = "نام کاربری")]
        public string username { get; set; }
        [Display(Name = "گذرواژه")]
        public string password { get; set; }
        public string shared_users { get; set; }
        public string wireless_psk { get; set; }
        public string wireless_enc_key { get; set; }
        public string wireless_enc_algo { get; set; }
        [Display(Name = "آخرین اتصال")]
        public string last_seen { get; set; }
        [Display(Name = "آخرین اتصال")]
        public string last_seenT { get; set; }
        [Display(Name = "فعال")]
        public string active { get; set; }
        public string incomplete { get; set; }
        [Display(Name = "فعال")]
        public string disabled { get; set; }
        [Display(Name = "نام تعرفه")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string actual_profile { get; set; }
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
        [Display(Name = "مدت زمان اتصال")]
        public string uptime_used { get; set; }
        [Display(Name = "دانلود استفاده شده")]
        public string download_used { get; set; }
        [Display(Name = "آپلود استفاده شده")]
        public string upload_used { get; set; }
        [Display(Name = "توضیحات")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        public string comment { get; set; }

        
    }
}
