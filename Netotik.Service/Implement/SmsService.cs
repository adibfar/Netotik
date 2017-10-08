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

namespace Netotik.Services.Implement
{
    public class SmsService : BaseService<SmsLog>, ISmsService
    {

        private readonly ISettingService _settingService;

        public SmsService(IUnitOfWork unit, ISettingService settingService)
            : base(unit)
        {
            _settingService = settingService;
        }

        public ResultSms SendSms(string To, string Text, long? UserSenderId = null)
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
                    MobileNumber = To,
                    Result = (ResultSms)retval,
                    UserId = UserSenderId
                };
                dbSet.Add(smsLog);

                return smsLog.Result;
            }
        }

    }
}

