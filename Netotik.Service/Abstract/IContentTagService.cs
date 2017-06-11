using Netotik.Common;
using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.Services.Enums;
using Netotik.ViewModels.CMS.ContentTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IContentTagService : IBaseService<ContentTag>
    {
        Task<ContentTag> SingleOrDefaultAsync(int primaryKey);

        IList<ContentTagItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);
        IList<ContentTag> GetAll(int languageId);

        Task<bool> IsExistByName(string name,int? id);

        IList<ContentTag> GetTagesbyIdsAsync(int[] ids);

        IList<ContentTag> GetByLanguageId(int id);
    }
}
