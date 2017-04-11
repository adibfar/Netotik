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

namespace Netotik.Services.Abstract
{
    public interface IMenuService : IBaseService<Menu>
    {
        IQueryable<TableAdminMenu> GetDataTableMenu(string search);
    }
}
