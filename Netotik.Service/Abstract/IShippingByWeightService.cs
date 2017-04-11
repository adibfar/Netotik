using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.ShippingByWeight;

namespace Netotik.Services.Abstract
{
    public interface IShippingByWeightService : IBaseService<ShippingByWeight>
    {
        IQueryable<TableShippingByWeightModel> GetDataTable(int shippingId);

        Task Remove(int id);
    }
}
