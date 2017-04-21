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
using Netotik.ViewModels.CMS.Advertise;
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class AdvertiseService : BaseService<Advertise>, IAdvertiseService
    {
        public AdvertiseService(IUnitOfWork unit)
            : base(unit)
        {

        }




        public IList<AdvertiseItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<Advertise> all = dbSet.AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            //if (!string.IsNullOrEmpty(model.sSearch))
            //{
            //    all = all
            //          .Where(x => x.Name.Contains(model.sSearch) || x.State.Name.Contains(model.sSearch))
            //          .AsQueryable();
            //}

            // Apply Sorting
            //Func<City, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.State.Name : x.Name);

            //// asc or desc
            //all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new AdvertiseItem
                {
                    Order = x.Order,
                    Url = x.Url,
                    Id = x.Id,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();
        }

    }
}
