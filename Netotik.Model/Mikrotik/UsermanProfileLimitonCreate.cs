using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Mikrotik
{
    public class UsermanProfileLimitionCreate
    {
        public string Profile_id { get; set; }
        [Display(Name = "نام پروفایل")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "فقط انگلیسی وارد کنید")]
        public string profilelimition_profile { get; set; }
        [Display(Name = "محدودیت")]
        public string profilelimition_limitation { get; set; }
        [Display(Name = "از ساعت")]
        public string profilelimition_from_time { get; set; }
        [Display(Name = "تا ساعت")]
        public string profilelimition_till_time { get; set; }
        [Display(Name = "روزهای هفته")]
        public string profilelimition_weekdays { get; set; }



        //---------------------------------------------


        [Display(Name = "نام")]
        public string profile_name { get; set; }
        [Display(Name = "ایجاد کننده")]
        public string profile_owner { get; set; }
        [Display(Name = "نام کاربران این تعرفه")]
        public string profile_name_for_users { get; set; }
        [Display(Name = "اعتبار زمانی")]
        public string profile_validity { get; set; }
        [Display(Name = "زمان شروع اعتبار زمانی")]
        public string profile_starts_at { get; set; }
        [Display(Name = "قیمت")]
        public string profile_price { get; set; }
        public string profile_override_shared_users { get; set; }


        //------------------------------------------------
        public string limitation_id { get; set; }
        [Display(Name = "نام")]
        public string limition_name { get; set; }
        [Display(Name = "ایجاد کننده")]
        public string limition_owner { get; set; }
        [Display(Name = "محدودیت دانلود")]
        public string limition_download_limit { get; set; }
        [Display(Name = "محدودیت آپلود")]
        public string limition_upload_limit { get; set; }
        [Display(Name = "محدودیت آپلود و دانلود")]
        public string limition_transfer_limit { get; set; }
        [Display(Name = "محدودیت زمان اتصال")]
        public string limition_uptime_limit { get; set; }
        [Display(Name = "محدودیت سرعت دانلود")]
        public string limition_rate_limit_rx { get; set; }
        [Display(Name = "محدودیت سرعت آپلود")]
        public string limition_rate_limit_tx { get; set; }
        public string limition_rate_limit_min_tx { get; set; }
        public string limition_group_name { get; set; }
        public string limition_ip_pool { get; set; }
        public string limition_address_list { get; set; }
    }
}
