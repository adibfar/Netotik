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
        IList<Content> GetLastContents(int size,int languageId);
        IList<Content> GetLastPopular(int size, int languageId);
        IList<Content> GetRelatedPost(int size, int[] categoryIds);

        IList<Content> GetRss(int size, int languageId);
        IEnumerable<PublicItemContentModel> GetForPublicView(out int total, int page, int count, int languageId, int? categoryId, int? tagId);
        Task Publish(int id);
        Task UnPublish(int id);

        Task<Content> SingleOrDefaultAsync(int primaryKey);
    }
}
