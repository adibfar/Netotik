using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.ShippingMethod;

namespace Netotik.Services.Abstract
{
    public interface IShippingMethodService : IBaseService<ShippingMethod>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        Task<ShippingMethod> GetByNameAsync(string name);
        IQueryable<TableShippingMethodModel> GetDataTable(string search);

        Task Remove(int id);
    }
}
