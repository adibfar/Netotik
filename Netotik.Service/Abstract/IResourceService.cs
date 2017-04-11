using Netotik.Common;
using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.Services.Enums;
using Netotik.ViewModels.Common.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IResourceService : IBaseService<Netotik.Domain.Entity.Resource>
    {
        void SeedDataBase(string xmlRsourceString,string culture);

        IList<ResourceItem> GetList(RequestListModel model,out long TotalCount, out long ShowCount);
    }
}
