using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.ComponentModel;
using System.Web;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class SmsModel
    {
        public long Id { get; set; }

        public long SmsCharge { get; set; }
        public bool RegisterWithSms { get; set; }
        [System.Web.Mvc.Remote("SmsCodeIsValid", "Remote", System.Web.Mvc.AreaReference.UseRoot, HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string RegisterWithSmsCode { get; set; }
        public bool SmsAdminChangeUserPassword { get; set; }
        public bool SmsUserhangeUserPassword { get; set; }
        public bool SmsAdminChangeAdminPassword { get; set; }
        public bool SmsAdminLogins { get; set; }
        public bool RegisterFormSms { get; set; }
        public bool SmsUserAfterCreateWithAdmin { get; set; }
        public bool SmsActive { get; set; }
        public bool SmsUserAfterResetCounter { get; set; }
        public bool SmsUserAfterDelete { get; set; }
        public bool SmsUserAfterChangePackage { get; set; }
    }
}
