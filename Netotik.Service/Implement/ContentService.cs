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
using Netotik.Common.DataTables;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Netotik.Services.Identity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFSecondLevelCache;
using System.Web.Mvc;

namespace Netotik.Services.Implement
{
    public class ContentService : BaseService<Content>, IContentService
    {

        #region Fields
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly IContentCategoryService _ContentCategoryService;
        private readonly IContentTagService _ContentTagService;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Constructor

        public ContentService(IUnitOfWork unit,
            IMappingEngine mappingEngine,
            ApplicationUserManager applicationUserManager,
            IContentCategoryService ContentCategoryService,
            IContentTagService ContentTagService)
            : base(unit)
        {
            _ContentCategoryService = ContentCategoryService;
            _ContentTagService = ContentTagService;
            _mappingEngine = mappingEngine;
            _applicationUserManager = applicationUserManager;
        }
        #endregion



        public async Task Publish(int id)
        {
            var contents = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            contents.status = ContentStatus.Accepted;
            Update(contents);
        }

        public async Task UnPublish(int id)
        {
            var contents = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            contents.status = ContentStatus.Deleted;
            Update(contents);
        }

        public async Task<ContentModel> GetForCreateAsync()
        {
            var model = new ContentModel();

            model.ContentTages = await _ContentTagService.All()
                .AsNoTracking()
                .Project(_mappingEngine).To<SelectListItem>()
                .Cacheable().ToListAsync();

            model.ContentCategories = await _ContentCategoryService.All()
                .Where(x => !x.IsDeleted && !x.ParentId.HasValue)
                .Include(x => x.SubCategories)
                .AsNoTracking().Cacheable().ToListAsync();

            model.IsPublished = true;
            model.AllowComments = true;
            model.AllowViewComments = true;

            return model;
        }

        public async Task<ContentModel> GetForEditAsync(int Id)
        {
            var content = await dbSet.FirstOrDefaultAsync(x => x.Id == Id);
            var model = _mappingEngine.Map<ContentModel>(content);

            model.ContentCategories = await _ContentCategoryService.All()
                .Where(x => !x.IsDeleted && !x.ParentId.HasValue)
                .Include(x => x.SubCategories)
                .AsNoTracking().Cacheable().ToListAsync();

            model.ContentTages = _ContentTagService.All().ToList().Select(x => new
            SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (content.ContentTages != null) ? content.ContentTages.Any(y => y.Id == x.Id) : false
            }).ToList();

            model.CategoryIds = content.ContentCategories.Select(x => x.Id).ToArray();

            model.Picture = content.Picture;

            return model;
        }


        public IList<ContentItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<Content> all = dbSet.Where(x => x.status != ContentStatus.Deleted).AsNoTracking().AsQueryable();
            var permissions = _applicationUserManager.GetRoles(_applicationUserManager.GetCurrentUserId());

            if (!permissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent))
                all = all.Where(x => x.CreatedUserId == _applicationUserManager.GetCurrentUserId());

            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.
                    Where(x => x.UserCreated.FirstName.Contains(model.sSearch) ||
                    x.UserCreated.LastName.Contains(model.sSearch) ||
                    x.Title.Contains(model.sSearch))
                    .AsQueryable();
            }


            // Apply Sorting
            Func<Content, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.Title : x.UserCreated.FirstName);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new ContentItem
                {
                    Id = x.Id,
                    RowNumber = model.iDisplayStart + index + 1,
                    AllowComments = x.AllowComments,
                    LastEdited = PersianDate.ConvertDate.ToFa(x.EditDate, "g"),
                    UserCreator = x.UserEdited.FirstName + " " + x.UserEdited.LastName,
                    status = x.status,
                    ViewCount = x.CountView,
                    CommentCount = x.ContentComments.Count,
                    Title = x.Title,
                    ImageFileName = x.PictureId.HasValue ? x.Picture.FileName : "Default.png",
                })
                .ToList();

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

            var selectQuery = contents.OrderBy(x => x.StartDate).Skip((page - 1) * count).Take(count)
               .Select(a => new PublicItemContentModel
               {
                   Id = a.Id,
                   CommentCount = a.CommentCount,
                   ViewCount = a.CountView,
                   Title = a.Title,
                   Image = a.PictureId.HasValue ? a.Picture.FileName : "",
                   Description = a.BodyOverview,
                   StartPublish = a.StartDate.Value,
                   User = a.UserCreated.FirstName + " " + a.UserCreated.LastName
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
