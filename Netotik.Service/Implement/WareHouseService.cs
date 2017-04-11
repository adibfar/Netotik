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
using Netotik.ViewModels.Shop.WareHouse;

namespace Netotik.Services.Implement
{
    public class WareHouseService : BaseService<Warehouse>, IWareHouseService
    {
        public WareHouseService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public async Task<bool> ExistsByNameAsync(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(name) && x.Id != id.Value);
            return await dbSet.AnyAsync(x => x.Name.Equals(name));
        }



        public IQueryable<TableWareHouseModel> GetDataTable(string search)
        {
            IQueryable<TableWareHouseModel> selected = dbSet
                .Where(x => !x.IsDelete).Include(x => x.AddressState).Include(x => x.AddressCity)
                                            .OrderBy(x => x.Name)
                                            .Select(x => new TableWareHouseModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                Address = x.Address,
                                                CityName = x.AddressCity.Name,
                                                StateName = x.AddressState.Name,
                                                IsActive = x.IsActive
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selected = selected.Where(x => x.Name.Contains(search)).AsQueryable();

            return selected;
        }
        public async Task Remove(int id)
        {
            var _Role = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_Role != null)
                Remove(_Role);
        }



    }
}
