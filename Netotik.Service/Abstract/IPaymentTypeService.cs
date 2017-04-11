using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.PaymentType;

namespace Netotik.Services.Abstract
{
    public interface IPaymentTypeService : IBaseService<PaymentType>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        Task<PaymentType> GetByNameAsync(string name);
        IQueryable<TablePaymentTypeModel> GetDataTable(string search);

        Task Remove(int id);
    }
}
