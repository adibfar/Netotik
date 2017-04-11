using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Shop.DeliveryDate;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IDeliveryDateService : IBaseService<DeliveryDate>
    {
        Task RemoveAsync(int id);
        IQueryable<TableDeliveryDateModel> GetDataTable(string search);
    }
}
