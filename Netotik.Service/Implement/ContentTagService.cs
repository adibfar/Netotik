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
using Netotik.ViewModels.CMS.ContentTag;
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class ContentTagService : BaseService<ContentTag>, IContentTagService
    {
        public ContentTagService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public IList<ContentTagItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<ContentTag> all = dbSet.Include(x=>x.Language).AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.Where(x => x.Name.Contains(model.sSearch)).AsQueryable();
            }


            // Apply Sorting
            Func<ContentTag, string> orderingFunction = (x => x.Name);

            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new ContentTagItem
                {
                    Name = x.Name,
                    Id = x.Id,
                    FlagLanguage = x.Language.FlagImageFileName,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();
        }

        public async Task<ContentTag> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey);
        }


        public async Task<bool> IsExistByName(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name == name && x.Id != id);
            return await dbSet.AnyAsync(x => x.Name == name);
        }


        public IList<ContentTag> GetTagesbyIdsAsync(int[] ids)
        {
            var tag = dbSet.ToList();
            IList<ContentTag> list = new List<ContentTag>();
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    var t = tag.FirstOrDefault(x => x.Id == item);
                    if (t != null)
                        list.Add(t);
                }
            }
            return list;
        }

        public IList<ContentTag> GetByLanguageId(int id)
        {
            return dbSet.AsNoTracking().Where(x => x.LanguageId == id).ToList();
        }

        public IList<ContentTag> GetAll(int languageId)
        {
            return dbSet
                .Where(x => x.LanguageId == languageId)
                .ToList();
        }
    }
}
