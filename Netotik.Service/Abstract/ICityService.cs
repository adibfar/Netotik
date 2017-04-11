using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Common.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ICityService : IBaseService<City>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        IList<CityItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);

        IList<CityModel> GetByStateId(int stateId);
        Task Remove(int id);
    }
}
