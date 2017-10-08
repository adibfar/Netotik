using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Common.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ISmsService : IBaseService<SmsLog>
    {
        ResultSms SendSms(string To, string Text, long? UserSenderId = null);
    }
}
