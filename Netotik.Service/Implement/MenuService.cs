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
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        public MenuService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public IList<Menu> GetAllFooterMenu(int languageId)
        {
            return dbSet
                   .Where(x => x.IsActive && x.MenuLocation != MenuLocation.Header && x.LanguageId == languageId)
                   .Include(x => x.SubMenues)
                   .OrderBy(x => x.Order)
                   .ToList();
        }

        public IList<Menu> GetAllHeaderMenu(int languageId)
        {
            return dbSet
                   .Where(x => x.IsActive && x.MenuLocation == MenuLocation.Header && x.LanguageId == languageId)
                   .Include(x => x.SubMenues)
                   .OrderBy(x => x.Order)
                   .ToList();
        }

        public IList<MenuItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<Menu> all = dbSet.Include(x => x.Language).AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.
                    Where(x => x.Text.Contains(model.sSearch))
                    .AsQueryable();
            }


            // Apply Sorting
            Func<Menu, string> orderingFunction = (x => x.Text);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new MenuItem
                {
                    Text = x.Text,
                    Id = x.Id,
                    Icon = x.Icon,
                    IsActive = x.IsActive,
                    Order = x.Order,
                    Parent = x.ParentId.HasValue ? x.Parent.Text : "",
                    FlagLanguage = x.Language.FlagImageFileName,
                    Url = x.Url,
                    RowNumber = model.iDisplayStart + index + 1
                }).ToList();
        }
    }
}
