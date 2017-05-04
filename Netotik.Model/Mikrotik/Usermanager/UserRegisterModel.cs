﻿using System.ComponentModel.DataAnnotations;

namespace Netotik.ViewModels.Mikrotik
{
    public class Usermanager_UserRegisterModel
    {
        [Display(Name = "ایجاد کننده")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string customer { get; set; }
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
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
        [Display(Name = "نام")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string first_name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string last_name { get; set; }
        [Display(Name = "شماره تماس")]
        public string phone { get; set; }
        [Display(Name = "آدرس")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string location { get; set; }
        [Display(Name = "ایمیل")]
        public string email { get; set; }
        public string ip_address { get; set; }
        [Display(Name = "توضیحات")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string comment { get; set; }
        [Display(Name = "نام تعرفه")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string profile { get; set; }

    }
}