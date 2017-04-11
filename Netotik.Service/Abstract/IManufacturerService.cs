using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.Manufacturer;

namespace Netotik.Services.Abstract
{
    public interface IManufacturerService : IBaseService<Manufacturer>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        Task<Manufacturer> GetByNameAsync(string name);
        IQueryable<TableManufacturerModel> GetDataTable(string search);

        Task Remove(int id);
    }
}
