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
        [Display(ResourceType = typeof(Captions), Name = "SmsCharge")]
        public long SmsCharge { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "RegisterWithSms")]
        public bool RegisterWithSms { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "RegisterWithSmsCode")]
        [System.Web.Mvc.Remote("SmsCodeIsValid", "Remote", System.Web.Mvc.AreaReference.UseRoot, HttpMethod = "POST", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string RegisterWithSmsCode { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsAdminChangeUserPassword")]
        public bool SmsAdminChangeUserPassword { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsUserhangeUserPassword")]
        public bool SmsUserhangeUserPassword { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsAdminChangeAdminPassword")]
        public bool SmsAdminChangeAdminPassword { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsAdminLogins")]
        public bool SmsAdminLogins { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "RegisterFormSms")]
        public bool RegisterFormSms { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsUserAfterCreateWithAdmin")]
        public bool SmsUserAfterCreateWithAdmin { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsActive")]
        public bool SmsActive { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsUserAfterResetCounter")]
        public bool SmsUserAfterResetCounter { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsUserAfterDelete")]
        public bool SmsUserAfterDelete { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsUserAfterChangePlan")]
        public bool SmsUserAfterChangePackage { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "RegisterWithSmsRouterProfile")]
        public string RegisterWithSmsRouterProfile { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "RegisterWithSmsMessage")]
        public string RegisterWithSmsMessage { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "SmsIfErrorInSms")]
        public bool SmsIfErrorInSms { get; set; }
    }
}
