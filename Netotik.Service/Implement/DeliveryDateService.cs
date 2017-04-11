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
using Netotik.ViewModels.Shop.DeliveryDate;

namespace Netotik.Services.Implement
{
    public class DeliveryDateService : BaseService<DeliveryDate>, IDeliveryDateService
    {
        public DeliveryDateService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public async Task RemoveAsync(int id)
        {
            var pic = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (pic != null)
                Remove(pic);
        }


        public IQueryable<TableDeliveryDateModel> GetDataTable(string search)
        {
            IQueryable<TableDeliveryDateModel> selected = dbSet.Where(x => !x.IsDelete).Select(x => new TableDeliveryDateModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                Order = x.DisplayOrder
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selected = selected.Where(x => x.Name.Contains(search)).AsQueryable();

            return selected;
        }
    }
}
