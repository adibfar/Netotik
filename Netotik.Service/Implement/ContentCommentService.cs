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
using Netotik.ViewModels.CMS.Comment;
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class ContentCommentService : BaseService<ContentComment>, IContentCommentService
    {
        public ContentCommentService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public async Task<ContentComment> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey);
        }


        public IList<CommentItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<ContentComment> all = dbSet.Where(x => x.Status != CommentStatus.Deleted).AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all
                      .Where(x => x.Name.Contains(model.sSearch)
                      || x.Text.Contains(model.sSearch) ||
                        x.Email.Contains(model.sSearch))
                      .AsQueryable();
            }

            // Apply Sorting
            Func<ContentComment, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.Name : x.Email);

            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new CommentItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Text = x.Text,
                    Status = x.Status,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();
        }
    }
}
