using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public partial class UserCompany
    {
        public long Id { get; set; }

        public string Expire_Date { get; set; }
        
        [Display(Name = "نام شرکت")]
        public string CompanyCode { get; set; }
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "آدرس روتر")]
        public string R_Host { get; set; }
        [Display(Name = "کاربر روتر")]
        public string R_User { get; set; }
        [Display(Name = "گذرواژه روتر")]
        public string R_Password { get; set; }
        [Display(Name = "API Port")]
        public int R_Port { get; set; }
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }

        [Display(Name = "فعال سازی CLoud ")]
        public bool cloud { get; set; }

        public long UserResellerId { get; set; }
        public virtual UserReseller UserReseller { get; set; }

        [Display(Name = "Userman Customer")]
        public string Userman_Customer { get; set; }
        public virtual User User { get; set; }
    }
}
