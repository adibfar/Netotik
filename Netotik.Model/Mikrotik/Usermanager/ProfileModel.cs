﻿using System.ComponentModel.DataAnnotations;

namespace Netotik.ViewModels.Mikrotik
{
    public class Usermanager_ProfileModel
    {
        public string id { get; set; }
        [Display(Name = "نام")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string name { get; set; }
        [Display(Name = "ایجاد کننده")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string owner { get; set; }
        [Display(Name = "نام کاربران این تعرفه")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string name_for_users { get; set; }
        [Display(Name = "اعتبار زمانی")]
        public string validity { get; set; }
        [Display(Name = "زمان شروع اعتبار زمانی")]
        public string starts_at { get; set; }
        [Display(Name = "قیمت")]
        public string price { get; set; }
        public string override_shared_users { get; set; } 
    }
}