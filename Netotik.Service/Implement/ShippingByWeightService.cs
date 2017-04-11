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
using Netotik.ViewModels.Shop.ShippingByWeight;

namespace Netotik.Services.Implement
{
    public class ShippingByWeightService : BaseService<ShippingByWeight>, IShippingByWeightService
    {
        public ShippingByWeightService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public IQueryable<TableShippingByWeightModel> GetDataTable(int shippingId)
        {
            IQueryable<TableShippingByWeightModel> selected = dbSet.Where(x => x.ShippingMethodId == shippingId).OrderBy(x => x.AdditionalFixedPrice)
                                            .Select(x => new TableShippingByWeightModel
                                            {
                                                Id = x.Id,
                                                FromWeight = x.FromWeight,
                                                ToWeight = x.ToWeight,
                                                AdditionalFixedPrice = x.AdditionalFixedPrice
                                            }).AsQueryable();


            return selected;
        }
        public async Task Remove(int id)
        {
            var _data = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_data != null)
                Remove(_data);
        }


    }
}
