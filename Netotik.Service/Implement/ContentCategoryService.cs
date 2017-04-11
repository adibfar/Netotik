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
using Netotik.ViewModels.CMS.ContentCategory;

namespace Netotik.Services.Implement
{
    public class ContentCategoryService : BaseService<ContentCategory>, IContentCategoryService
    {
        public ContentCategoryService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public async Task<bool> ExistsByNameAsync(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(name) && x.Id != id.Value && !x.IsDelete);
            return await dbSet.AnyAsync(x => x.Name.Equals(name) && !x.IsDelete);
        }



        public async Task<ContentCategory> GetSubjectByNameAsync(string name)
        {
            return await dbSet.SingleAsync(x => x.Name.Equals(name) && !x.IsDelete);
        }


        public IQueryable<TableContentCategoryModel> GetDataTableSubject(string search)
        {
            IQueryable<TableContentCategoryModel> selected = dbSet.Where(x=>!x.IsDelete)
                                            .OrderByDescending(x=>x.Contents.Count())
                                            .Select(x => new TableContentCategoryModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selected = selected.Where(x => x.Name.Contains(search)).AsQueryable();

            return selected;
        }
        public async Task Remove(int id)
        {
            var _Role = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_Role != null)
                Remove(_Role);
        }

        public IList<ContentCategory> GetbyIds(int[] ids)
        {
            var categories = dbSet.ToList();
            var list = new List<ContentCategory>();
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    var t = categories.FirstOrDefault(x => x.Id == item);
                    if (t != null)
                        list.Add(t);
                }
            }
            return list;
        }



        public async Task<ContentCategory> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey && !x.IsDelete);
        }


    }
}
