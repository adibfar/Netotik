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
using Netotik.ViewModels.Common.City;
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class CityService : BaseService<City>, ICityService
    {
        public CityService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public async Task<bool> ExistsByNameAsync(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(name) && x.Id != id.Value);
            return await dbSet.AnyAsync(x => x.Name.Equals(name));
        }

        public IList<CityItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<City> all = dbSet.Include(x => x.State).AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all
                      .Where(x => x.Name.Contains(model.sSearch) || x.State.Name.Contains(model.sSearch))
                      .AsQueryable();
            }

            // Apply Sorting
            Func<City, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.State.Name : x.Name);

            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new CityItem
                {
                    City = x.Name,
                    State = x.State.Name,
                    Id = x.Id,
                    IsDefault = x.IsDefault,
                    IsActive = x.IsActive,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();
        }

        public async Task Remove(int id)
        {
            var _Role = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_Role != null)
                Remove(_Role);
        }





        public IList<CityModel> GetByStateId(int stateId)
        {
            return dbSet.Where(x => x.IsActive && x.AddressStateId == stateId).OrderByDescending(x => x.IsDefault).Select(x => new CityModel { Id = x.Id, Name = x.Name }).ToList();
        }
    }
}
