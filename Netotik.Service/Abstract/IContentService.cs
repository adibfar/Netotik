using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.ViewModels.CMS.Content;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Common.DataTables;

namespace Netotik.Services.Abstract
{
    public interface IContentService : IBaseService<Content>
    {
        IList<ContentItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);

        Task<ContentModel> GetForCreateAsync();
        Task<ContentModel> GetForEditAsync(int Id);
        IList<Content> GetLastContents(int size);
        IList<Content> GetLastPopular(int size);
        IEnumerable<PublicItemContentModel> GetForPublicView(out int total, int page, int count, int? categoryId, int? tagId);
        Task Publish(int id);
        Task UnPublish(int id);

        Task<Content> SingleOrDefaultAsync(int primaryKey);
    }
}
