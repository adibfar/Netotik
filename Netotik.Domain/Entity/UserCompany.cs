using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public partial class UserCompany
    {
        public UserCompany()
        {
            UserCompanyClients = new List<UserCompanyClient>();
            UserCompanyLogClients = new List<UserCompanyLogClient>();
        }
        public long Id { get; set; }

        public string Expire_Date { get; set; }

        public string CompanyCode { get; set; }
        public string NationalCode { get; set; }
        public string Address { get; set; }
        public string R_Host { get; set; }
        public string R_User { get; set; }
        public string R_Password { get; set; }
        public int R_Port { get; set; }
        public string PostalCode { get; set; }

        public bool cloud { get; set; }
        public bool WebsitesLogs { get; set; }


        public long SmsCharge { get; set; }
        public bool RegisterWithSms { get; set; }
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

        public string RegisterWithSmsRouterProfile { get; set; }
        public string RegisterWithSmsMessage { get; set; }
        public bool SmsIfErrorInSms { get; set; }


        public long UserResellerId { get; set; }
        public virtual UserReseller UserReseller { get; set; }

        [Display(Name = "Userman Customer")]
        public string Userman_Customer { get; set; }
        public virtual User User { get; set; }
        public virtual UserCompanyTelegram UserCompanyTelegram { get; set; }
        public virtual UserCompanyRegisterSetting UserCompanyRegisterSetting { get; set; }
        public virtual ICollection<UserCompanyClient> UserCompanyClients { get; set; }
        public virtual ICollection<UserCompanyLogClient> UserCompanyLogClients { get; set; }

        public string ClientPermissions { get; set; }
        public string CompanyPermissions { get; set; }
    }
}
