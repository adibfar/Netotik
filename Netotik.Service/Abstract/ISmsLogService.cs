using Netotik.Domain.Entity;
using Netotik.ViewModels.SmsPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ISmsLogService : IBaseService<SmsLog>
    {
        IList<SmsLog> GetAll();
        IList<SmsLog> GetLastDays(int Days);
    }
}
