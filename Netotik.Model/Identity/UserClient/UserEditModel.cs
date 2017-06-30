using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class UserEditModel
    {
        [AllowHtml]
        public string id { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        [Display(Name = "ایجاد کننده")]
        public string customer { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        [Display(Name = "نام کاربری")]
        public string username { get; set; }
        [Display(Name = "گذرواژه")]
        public string password { get; set; }
        public string shared_users { get; set; }
        public string wireless_psk { get; set; }
        public string wireless_enc_key { get; set; }
        public string wireless_enc_algo { get; set; }

        [Display(Name = "فعال")]
        public string disabled { get; set; }

        public string caller_id { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        [Display(Name = "نام")]
        public string first_name { get; set; }
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessage = "مقدار وارد شده معتبر نمی باشد")]
        [Display(Name = "نام خانوادگی")]
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
        public string registration_date { get; set; }
        public string reg_key { get; set; }

    }
}
