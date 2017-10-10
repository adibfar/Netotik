using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PersianDate;
using Netotik.Common;
using Netotik.Common.DataTables;
using Netotik.Service.FarapayamakService;
using Netotik.Services.Identity;

namespace Netotik.Services.Implement
{
    public class SmsService : BaseService<SmsLog>, ISmsService
    {

        private readonly ISettingService _settingService;
        private readonly IApplicationUserManager _applicationUserManager;

        public SmsService(IUnitOfWork unit, ISettingService settingService, IApplicationUserManager applicationUserManager)
            : base(unit)
        {
            _applicationUserManager = applicationUserManager;
            _settingService = settingService;
        }

        public long GetCredit()
        {
            throw new NotImplementedException();
        }

        public int GetMessageSize(string text)
        {
            if (text.Any(x => !char.IsSymbol(x) && !char.IsLower(x) && !char.IsUpper(x) && !char.IsDigit(x) && !char.IsWhiteSpace(x)))
                return (text.Length / 71) + 1;
            return (text.Length / 161) + 1;

        }

        public ResultSms SendSms(string To, string Text, long UserSenderId)
        {
            using (var sms = new Send())
            {
                var user = _applicationUserManager.FindUserById(UserSenderId);
                var setting = _settingService.GetAll();

                long[] rec = null;
                byte[] status = null;

                if (GetMessageSize(Text) > user.UserCompany.SmsCharge)
                    return ResultSms.CreditNotValid;

                int retval = sms.SendSms(setting.SmsUsername, setting.SmsPassword, new string[] { To }, setting.SmsNumber, Text, false, "", ref rec, ref status);

                var smsLog = new SmsLog()
                {
                    CreateDate = DateTime.Now,
                    From = setting.SmsNumber,
                    MessageText = Text,
                    MessageSize = GetMessageSize(Text),
                    MobileNumber = To,
                    Result = (ResultSms)retval,
                    UserId = UserSenderId
                };
                dbSet.Add(smsLog);

                if (smsLog.Result == ResultSms.Success)
                    user.UserCompany.SmsCharge -= GetMessageSize(Text);

                return smsLog.Result;
            }
        }


        public ResultSms SendSms(string To, string Text)
        {
            using (var sms = new Send())
            {
                var setting = _settingService.GetAll();

                long[] rec = null;
                byte[] status = null;

                int retval = sms.SendSms(setting.SmsUsername, setting.SmsPassword, new string[] { To }, setting.SmsNumber, Text, false, "", ref rec, ref status);


                var smsLog = new SmsLog()
                {
                    CreateDate = DateTime.Now,
                    From = setting.SmsNumber,
                    MessageText = Text,
                    MessageSize = GetMessageSize(Text),
                    MobileNumber = To,
                    Result = (ResultSms)retval
                };
                dbSet.Add(smsLog);

                return smsLog.Result;
            }
        }

    }
}

