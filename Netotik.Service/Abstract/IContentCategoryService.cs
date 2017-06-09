using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.CMS.ContentCategory;
using Netotik.Common.DataTables;

namespace Netotik.Services.Abstract
{
    public interface IContentCategoryService : IBaseService<ContentCategory>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);

        Task<ContentCategory> GetSubjectByNameAsync(string name);
        IList<ContentCategoryItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);
        IList<ContentCategory> GetAll(int languageId);

        IList<ContentCategory> GetbyIds(int[] ids);

        Task<ContentCategory> SingleOrDefaultAsync(int primaryKey);
        
        Task Remove(int id);
    }
}
