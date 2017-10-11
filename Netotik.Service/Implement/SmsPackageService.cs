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
    public class SmsPackageService : BaseService<SmsPackage>, ISmsPackageService
    {
        public SmsPackageService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public IList<SmsPackageItem> GetAll()
        {
            return dbSet.Where(x => x.IsActive).OrderBy(x => x.Order).ToList()
                .Select((x, index) => new SmsPackageItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    Order = x.Order,
                    Price = x.Price,
                    UnitPrice = x.UnitPrice,
                    SmsCount = x.SmsCount,
                    RowNumber = ++index
                }).ToList();
        }
    }
}
