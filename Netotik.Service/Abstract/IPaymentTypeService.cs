using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.PaymentType;
using Netotik.Common.DataTables;

namespace Netotik.Services.Abstract
{
    public interface IPaymentTypeService : IBaseService<PaymentType>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        Task<PaymentType> GetByNameAsync(string name);
        IList<PaymentTypeItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);

        Task Remove(int id);
    }
}
