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
using Netotik.ViewModels.Shop.ShippingMethod;

namespace Netotik.Services.Implement
{
    public class ShippingMethodService : BaseService<ShippingMethod>, IShippingMethodService
    {
        public ShippingMethodService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public async Task<bool> ExistsByNameAsync(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(name) && x.Id != id.Value);
            return await dbSet.AnyAsync(x => x.Name.Equals(name));
        }



        public async Task<ShippingMethod> GetByNameAsync(string name)
        {
            return await dbSet.SingleAsync(x => x.Name.Equals(name));
        }


        public IQueryable<TableShippingMethodModel> GetDataTable(string search)
        {
            IQueryable<TableShippingMethodModel> selected = dbSet.Where(x => !x.IsDelete).Include(x => x.Picture).Include(x => x.ShippingByWeights)
                .OrderBy(x => x.Name)
                                            .Select(x => new TableShippingMethodModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                Description = x.Description,
                                                BasePrice = x.BasePrice,
                                                CountCondition = x.ShippingByWeights.Count,
                                                IsActive = x.IsActive,
                                                imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selected = selected.Where(x => x.Name.Contains(search)).AsQueryable();

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
