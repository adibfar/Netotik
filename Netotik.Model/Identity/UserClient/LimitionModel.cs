﻿using Netotik.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserClient
{
    public class LimitionModel
    {
        [AllowHtml]
        public string id { get; set; }
        [Display(Name = "نام")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9: .-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string name { get; set; }
        [Display(Name = "ایجاد کننده")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string owner { get; set; }
        [Display(Name = "محدودیت دانلود")]
        public string download_limit { get; set; }
        [Display(Name = "محدودیت آپلود")]
        public string upload_limit { get; set; }
        [Display(Name = "محدودیت آپلود و دانلود")]
        public string transfer_limit { get; set; }
        [Display(Name = "محدودیت زمان اتصال")]
        public string uptime_limit { get; set; }
        [Display(Name = "محدودیت سرعت آپلود")]
        public string rate_limit_rx { get; set; }
        [Display(Name = "محدودیت سرعت دانلود")]
        public string rate_limit_tx { get; set; }
        public string rate_limit_min_tx { get; set; }
        public string group_name { get; set; }
        public string ip_pool { get; set; }
        public string address_list { get; set; }
        
    }
}
