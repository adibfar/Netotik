using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Common.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IStateService : IBaseService<State>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        IList<StateItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);

        Task Remove(int id);
    }
}
