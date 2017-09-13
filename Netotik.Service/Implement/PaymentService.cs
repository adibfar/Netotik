﻿using Netotik.Common.Security;
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
using Netotik.ViewModels.Shop.PaymentType;
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class PaymentTypeService : BaseService<PaymentType>, IPaymentTypeService
    {
        public PaymentTypeService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public async Task<bool> ExistsByNameAsync(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(name) && x.Id != id.Value);
            return await dbSet.AnyAsync(x => x.Name.Equals(name));
        }



        public async Task<PaymentType> GetByNameAsync(string name)
        {
            return await dbSet.SingleAsync(x => x.Name.Equals(name));
        }


        IList<PaymentTypeItem> IPaymentTypeService.GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<PaymentType> all = dbSet.AsQueryable();

            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.Where(x => x.Name.Contains(model.sSearch)).AsQueryable();
            }


            // Apply Sorting
            Func<PaymentType, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.Name : x.Name);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new PaymentTypeItem
                {
                    Id = x.Id,
                    RowNumber = model.iDisplayStart + index + 1,
                    Name = x.Name,
                    imgName = x.PictureId.HasValue ? x.Picture.FileName : "Default.png",
                    Description = x.Description,
                    GateWayUrl = x.GateWayUrl,
                    IsActive = x.IsActive,
                    TerminalId = x.TerminalId
                }).ToList();
        }
        public async Task Remove(int id)
        {
            var _data = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_data != null)
                Remove(_data);
        }

    }
}
