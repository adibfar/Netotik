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
using Netotik.ViewModels.Shop.Category;

namespace Netotik.Services.Implement
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public async Task<bool> ExistsByNameAsync(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(name) && x.Id != id.Value && !x.IsDeleted);
            return await dbSet.AnyAsync(x => x.Name.Equals(name));
        }



        public async Task<Category> GetByNameAsync(string name)
        {
            return await dbSet.SingleAsync(x => x.Name.Equals(name) && !x.IsDeleted);
        }


        public IQueryable<TableCategoryModel> GetDataTable(string search)
        {
            IQueryable<TableCategoryModel> selected = dbSet.Where(x => !x.IsDeleted).Include(x => x.Products).Include(x => x.Picture)
                .OrderByDescending(x => x.Products.Count)
                                            .Select(x => new TableCategoryModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                Description = x.Description,
                                                IsPublish = x.IsPublished,
                                                imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
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



        public IList<Category> GetbyIds(int[] ids)
        {
            var categories = dbSet.ToList();
            var list = new List<Category>();
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

    }
}
