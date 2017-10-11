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
using Netotik.ViewModels.Shop.Factor;
using Netotik.Common.DataTables;
using Netotik.Services.Identity;

namespace Netotik.Services.Implement
{
    public class FactorService : BaseService<Factor>, IFactorService
    {
        public readonly IApplicationUserManager _applicationUserManager;
        public FactorService(IUnitOfWork unit, IApplicationUserManager applicationUserManager)
            : base(unit)
        {
            _applicationUserManager = applicationUserManager;
        }


        IList<FactorUserItem> IFactorService.GetUserFactorList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<Factor> all = dbSet.Where(x => x.UserId == _applicationUserManager.GetCurrentUserId()).AsQueryable();

            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.Where(x => x.Id.ToString().Contains(model.sSearch)).AsQueryable();
            }

            // Apply Sorting
            Func<Factor, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.Id.ToString() : x.Id.ToString());
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new FactorUserItem
                {
                    Id = x.Id,
                    RowNumber = model.iDisplayStart + index + 1,
                    RegisterPay = x.PaymentDate,
                    RegisterDate = x.RegisterDate,
                    FactorStatus = x.FactorStatus,
                    FactorType = x.FactorType,
                    GetId = x.GetId,
                    IpAddress = x.IpAddress,
                    PaymentPrice = x.PaymentPrice,
                    TransactionId = x.TransactionId
                }).ToList();
        }


        public async Task Remove(int id)
        {
            var _data = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_data != null)
                Remove(_data);
        }

    }
}
