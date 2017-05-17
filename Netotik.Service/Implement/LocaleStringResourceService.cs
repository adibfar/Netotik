using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Netotik.Common.DataTables;
using Netotik.ViewModels.Common.Language;
using AutoMapper;

namespace Netotik.Services.Implement
{
    public class LocaleStringResourceService : BaseService<Netotik.Domain.Entity.LocaleStringResource>, ILocaleStringResourceService
    {

        private readonly IMappingEngine _mappingEngine;
        public LocaleStringResourceService(IMappingEngine mappingEngine, IUnitOfWork unit)
            : base(unit)
        {
            _mappingEngine = mappingEngine;
        }

        public IList<ResourceItem> GetList(RequestListModel model, int LanguageId, out long TotalCount, out long ShowCount)
        {
            IQueryable<LocaleStringResource> all = dbSet.Where(x => x.LanguageId == LanguageId)
                .Include(x => x.Language).AsQueryable();

            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.
                    Where(x => x.Name.Contains(model.sSearch) || x.Value.Contains(model.sSearch))
                    .AsQueryable();
            }


            // Apply Sorting
            Func<LocaleStringResource, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.Name : x.Value);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength)
                .Select((x, index) => new ResourceItem
                {
                    Key = x.Name,
                    Value = x.Value,
                    Id = x.Id,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();

        }

    }
}
