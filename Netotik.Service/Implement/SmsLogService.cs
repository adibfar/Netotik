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
using Netotik.ViewModels.SmsPackage;

namespace Netotik.Services.Implement
{
    public class SmsLogService : BaseService<SmsLog>, ISmsLogService
    {
        public SmsLogService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public IList<SmsLog> GetAll()
        {
            return dbSet.ToList()
                .Select((x, index) => new SmsLog
                {
                    Id = x.Id
                }).ToList();
        }

      
    }
}
