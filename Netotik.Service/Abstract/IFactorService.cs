using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.Factor;
using Netotik.Common.DataTables;

namespace Netotik.Services.Abstract
{
    public interface IFactorService : IBaseService<Factor>
    {
        IList<FactorUserItem> GetUserFactorList(RequestListModel model, out long TotalCount, out long ShowCount);

        Task Remove(int id);
    }
}
