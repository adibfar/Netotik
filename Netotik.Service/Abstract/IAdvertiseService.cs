using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.ViewModels.CMS.Advertise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IAdvertiseService : IBaseService<Advertise>
    {
        IList<AdvertiseItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);
    }
}
