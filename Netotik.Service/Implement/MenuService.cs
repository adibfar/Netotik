using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Netotik.ViewModels.CMS.Menu;

namespace Netotik.Services.Implement
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        public MenuService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public IQueryable<TableAdminMenu> GetDataTableMenu(string search)
        {
            IQueryable<TableAdminMenu> contents = dbSet
                                  .OrderByDescending(x => x.Text)
                                  .Select(x => new TableAdminMenu
                                  {
                                      Id = x.Id,
                                      Name = x.Text,
                                      Url = x.Url,
                                      Parent = "",
                                  }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                contents = contents.Where(x => x.Name.Contains(search)).AsQueryable();

            return contents;
        }
    }
}
