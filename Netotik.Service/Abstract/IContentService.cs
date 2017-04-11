using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.ViewModels.CMS.Content;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IContentService : IBaseService<Content>
    {
        IQueryable<TableContentModel> GetContentTable(string search, long userId, string[] Roles);

        IList<Content> GetLastContents(int size);
        IList<Content> GetLastPopular(int size);
        IEnumerable<PublicItemContentModel> GetForPublicView(out int total, int page, int count, int? categoryId, int? tagId);
        Task Publish(int id);
        Task UnPublish(int id);

        Task<Content> SingleOrDefaultAsync(int primaryKey);
    }
}
