using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.ViewModels;
using Netotik.ViewModels.CMS.Menu;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Common.DataTables;

namespace Netotik.Services.Abstract
{
    public interface IMenuService : IBaseService<Menu>
    {
        IList<MenuItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);

        IList<Menu> GetAllHeaderMenu(int languageId);
        IList<Menu> GetAllFooterMenu(int languageId);

    }
}
