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
using EntityFramework.Extensions;
using Netotik.ViewModels.CMS.Content;
using Netotik.ViewModels.Identity.Security;

namespace Netotik.Services.Implement
{
    public class ContentService : BaseService<Content>, IContentService
    {
        public ContentService(IUnitOfWork unit)
            : base(unit)
        {

        }



        public async Task Publish(int id)
        {
            var contents = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            contents.status = ContentStatus.Accepted;
            Update(contents);
        }

        public async Task UnPublish(int id)
        {
            var contents = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            contents.status = ContentStatus.Delete;
            Update(contents);
        }


        public IQueryable<TableContentModel> GetContentTable(string search, long userId, string[] Roles)
        {
            var contents = dbSet.AsNoTracking().Include(x => x.ContentComments)
                                  .OrderByDescending(x => x.CreateDate)
                                  .AsQueryable();



            if (Roles.Any(x => x == AssignableToRolePermissions.CanViewAllContent))
            {

            }
            else if (Roles.Any(x => x == AssignableToRolePermissions.CanAccessContent))
            {
                contents = contents.Where(x => x.CreatedUserId == userId);
            }
            else
            {
                contents = contents.Where(x => 1 == 0);
            }


            if (!string.IsNullOrWhiteSpace(search))
                contents = contents.Where(x => x.Title.Contains(search)).AsQueryable();

            return contents.Select(x => new TableContentModel
            {
                Id = x.Id,
                AllowComments = x.AllowComments,
                LastEdited = x.EditDate,
                LastUserEdit = x.UserEdited.FirstName + " " + x.UserEdited.LastName,
                status = x.status,
                ViewCount = x.CountView,
                CommentCount = x.ContentComments.Count,
                Title = x.Title
            }).AsQueryable();
        }

        public IEnumerable<PublicItemContentModel> GetForPublicView(out int total, int page, int count, int? categoryId, int? tagId)
        {
            var date = DateTime.Now;
            var date1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            var date2 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            var contents = dbSet.AsNoTracking()
                .Where(x => x.status == ContentStatus.Accepted)
                .Where(x => (x.StartDate.HasValue ? x.StartDate.Value <= date1 : true) &&
                            (x.EndDate.HasValue ? x.EndDate.Value >= date2 : true))
                .Include(x => x.Picture)
                .Include(x => x.ContentTages)
                .Include(x => x.UserCreated)
                .AsQueryable();

            if (categoryId.HasValue) contents = contents.Where(x => x.ContentCategories.Any(y => y.Id == categoryId.Value)).AsQueryable();
            if (tagId.HasValue) contents = contents.Where(x => x.ContentTages.Any(y => y.Id == tagId.Value)).AsQueryable();

            var totalQuery = contents.FutureCount();

            var selectQuery = contents.OrderBy(x=>x.StartDate).Skip((page - 1) * count).Take(count)
               .Select(a => new PublicItemContentModel
               {
                   Id = a.Id,
                   CommentCount = a.CommentCount,
                   ViewCount = a.CountView,
                   Title = a.Title,
                   Image = a.PictureId.HasValue ? a.Picture.FileName : "",
                   Description = a.BodyOverview,
                   StartPublish = a.StartDate.Value
               }).Future();

            total = totalQuery.Value;
            return selectQuery.ToList();
        }


        public IList<Content> GetLastContents(int size)
        {
            var date = DateTime.Now.Date;
            var date1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            var date2 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            return dbSet
                .Where(x => x.status == ContentStatus.Accepted &&
                      (x.StartDate.HasValue ? x.StartDate.Value <= date1 : true) &&
                      (x.EndDate.HasValue ? x.EndDate.Value >= date2 : true))
                .Include(x => x.Picture).Include(x => x.UserCreated)
                .OrderByDescending(x => x.StartDate)
                .Take(size).ToList();

        }

        public IList<Content> GetLastPopular(int size)
        {
            var date = DateTime.Now.Date;
            var date1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            var date2 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            return dbSet.Where(x => x.status == ContentStatus.Accepted &&
                            (x.StartDate.HasValue ? x.StartDate.Value <= date1 : true) &&
                            (x.EndDate.HasValue ? x.EndDate.Value >= date2 : true))
             .Include(x => x.Picture).Include(x => x.UserCreated)
             .OrderByDescending(x => x.CountView)
             .Take(size).ToList();
        }



        public async Task<Content> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey);
        }
    }
}
