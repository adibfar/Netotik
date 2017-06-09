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
using PersianDate;
using Netotik.Common;
using Netotik.Common.DataTables;
using Netotik.ViewModels.CMS.IndexSection;

namespace Netotik.Services.Implement
{
    public class IndexSectionService : BaseService<IndexSection>, IIndexSectionService
    {
        public IndexSectionService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public IList<IndexSection> GetAll(int languageId)
        {
            return dbSet
                .Where(x => x.LanguageId == languageId)
                .OrderBy(x => x.Order)
                .ToList();
        }

        public IList<IndexSectionItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<IndexSection> all = dbSet.Include(x => x.Language).AsQueryable();
            TotalCount = all.LongCount();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new IndexSectionItem
                {
                    Order = x.Order,
                    Title = x.Title,
                    Id = x.Id,
                    FlagLanguage = x.Language.FlagImageFileName,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();
        }

    }
}
