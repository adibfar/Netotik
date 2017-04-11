using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.Category;

namespace Netotik.Services.Abstract
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        Task<Category> GetByNameAsync(string name);
        IQueryable<TableCategoryModel> GetDataTable(string search);

        IList<Category> GetbyIds(int[] ids);
        

        Task Remove(int id);
    }
}
