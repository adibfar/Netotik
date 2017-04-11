using Netotik.Domain.Entity;
using Netotik.ViewModels.Shop.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IWareHouseService : IBaseService<Warehouse>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        IQueryable<TableWareHouseModel> GetDataTable(string search);

        Task Remove(int id);
    }
}
