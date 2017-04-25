using Netotik.Common;
using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.Services.Enums;
using Netotik.ViewModels.CMS.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IContentCommentService : IBaseService<ContentComment>
    {
        Task<ContentComment> SingleOrDefaultAsync(int primaryKey);
        IList<CommentItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);
    }
}
