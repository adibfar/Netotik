using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MvcPaging;
using Netotik.Common.Filters;
using Netotik.Web.Infrastructure;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.Common;
using Netotik.Resources;
using Netotik.Domain.Entity;
using Netotik.Common.Security;
using Netotik.Web.Infrastructure.Filters;
using System.Web.UI;
using System.Threading.Tasks;
using Netotik.Web;
using System.Data.Entity.Validation;
using Netotik.Web.Extension;
using System.IO;
using Microsoft.AspNet.Identity;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.CMS.Content;
using Netotik.ViewModels.CMS.ContentCategory;
using Netotik.Services.Identity;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست مطالب", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ContentController : BasePanelController
    {

        #region ctor
        private readonly IContentTagService _contentTagService;
        private readonly IContentService _contentService;
        private readonly IContentCategoryService _categoryService;
        private readonly IApplicationUserManager _applicationUserManagerService;
        private readonly IUnitOfWork _uow;

        public ContentController(
            IContentCategoryService categoryService,
            IContentTagService contentTagService,
            IApplicationUserManager applicationUserManagerService,
            IContentService contentService,
            IUnitOfWork uow)
        {
            _categoryService = categoryService;
            _contentTagService = contentTagService;
            _applicationUserManagerService = applicationUserManagerService;
            _contentService = contentService;
            _uow = uow;
        }
        #endregion

        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContent)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _contentService.GetContentTable(Search, 1, new string[] { "1" })
                .ToPagedList<TableContentModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.Content.Views._Table, pageList);
            else
                return View(MVC.Admin.Content.ActionNames.Index, pageList);
        }
        #endregion

        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateContent)]
        [BreadCrumb(Title = "مطلب جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await PopulateCategories();
            PopulateTags();
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateContent)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ContentModel model, ActionType actionType)
        {
            if (model.TagIds != null && model.TagIds.Count() > 0)
                PopulateTags(model.TagIds);
            else PopulateTags();

            if (model.CategoryIds == null) await PopulateCategories();
            else await PopulateCategories(model.CategoryIds);


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            #region Initial Content
            var now = DateTime.Now;
            var startDatePublish = now;
            DateTime? endDatePublish = now;
            if (model.StartDate.HasValue)
            {
                if (model.StartDateTime.HasValue)
                {
                    startDatePublish = new DateTime(
                        model.StartDate.Value.Year,
                        model.StartDate.Value.Month,
                        model.StartDate.Value.Day,
                        model.StartDateTime.Value.Hours,
                        model.StartDateTime.Value.Minutes, 0);
                }
                else
                {
                    startDatePublish = new DateTime(
                    model.StartDate.Value.Year,
                    model.StartDate.Value.Month,
                    model.StartDate.Value.Day,
                    now.Hour,
                    now.Minute, 0);
                }
            }
            if (model.EndDate.HasValue)
            {
                if (model.EndDateTime.HasValue)
                {
                    endDatePublish = new DateTime(
                        model.EndDate.Value.Year,
                        model.EndDate.Value.Month,
                        model.EndDate.Value.Day,
                        model.EndDateTime.Value.Hours,
                        model.EndDateTime.Value.Minutes, 0);
                }
                else
                {
                    startDatePublish = new DateTime(
                    model.EndDate.Value.Year,
                    model.EndDate.Value.Month,
                    model.EndDate.Value.Day,
                    now.Hour,
                    now.Minute, 0);
                }
            }


            var content = new Netotik.Domain.Entity.Content()
            {
                AdminComment = model.AdminComment,
                AllowComments = model.AllowComments,
                AllowViewComments = model.AllowViewComments,
                Body = model.Body,
                BodyOverview = model.BodyOverview,
                CreateDate = now,
                CreatedUserId = User.Identity.GetUserId<long>(),
                EditDate = now,
                EditedUserId = User.Identity.GetUserId<long>(),
                status = ContentStatus.WaitForAccept,
                Title = model.Title,
                StartDate = model.StartDate.HasValue ? startDatePublish : now,
                EndDate = model.EndDate.HasValue ? endDatePublish : null,
                MetaDescription = model.MetaDescription,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle
            };
            content.ContentTages = _contentTagService.GetTagesbyIdsAsync(model.TagIds);
            content.ContentCategories = _categoryService.GetbyIds(model.CategoryIds);


            #endregion

            #region Add Content Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesContentPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                content.Picture = picture;
            }
            #endregion

            _contentService.Add(content);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.Content.Index());
        }
        #endregion

        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContent)]
        public virtual ActionResult Detail(int id)
        {
            var content = _contentService.SingleOrDefault(id);
            if (content == null) return HttpNotFound();

            var canView = (content.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent);
            if (canView)
            {
                if (content.PictureId.HasValue)
                    ViewBag.Avatar = Path.Combine(FilePathes._imagesContentPath, content.Picture.FileName);
                return View(content);
            }
            return HttpNotFound();
        }

        #endregion

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteContent)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var news = _contentService.SingleOrDefault(id);
            if (news == null) return HttpNotFound();
            var canDelete = (news.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent);
            if (canDelete)
            {
                _contentService.Remove(news);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.Content.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAcceptContent)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Accept(int id = 0)
        {
            var news = _contentService.SingleOrDefault(id);
            if (news == null) return HttpNotFound();
            var canAccept = (news.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent);
            if (canAccept)
            {
                news.status = ContentStatus.Accepted;
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.Content.ActionNames.Index);
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDontAcceptContent)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> NotAccept(int id = 0)
        {
            var news = _contentService.SingleOrDefault(id);
            if (news == null) return HttpNotFound();
            var canDontAccept = (news.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent);
            if (news != null && canDontAccept)
            {
                news.status = ContentStatus.NotAccepted;
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.Content.Index());
        }


        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditContent)]
        [BreadCrumb(Title = "ویرایش مطلب", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = _contentService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();

            var canEdit = (model.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent);
            if (canEdit == false) return HttpNotFound();

            await PopulateCategories(model.ContentCategories.Select(x => x.Id).ToArray());
            PopulateTags(model.ContentTages.Select(x => x.Id).ToArray());


            if (model.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesContentPath, model.Picture.FileName);

            var editModel = new ContentModel
            {
                Id = model.Id,
                AdminComment = model.AdminComment,
                AllowComments = model.AllowComments,
                AllowViewComments = model.AllowViewComments,
                Body = model.Body,
                BodyOverview = model.BodyOverview,
                StartDate = model.StartDate,
                StartDateTime = model.StartDate.Value.TimeOfDay,
                EndDate = model.EndDate,
                EndDateTime = model.EndDate.HasValue ? (TimeSpan?)model.EndDate.Value.TimeOfDay : null,
                Title = model.Title,
                MetaDescription = model.MetaDescription,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle
            };

            return View(editModel);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditContent)]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ContentModel model, ActionType actionType)
        {
            var entity = _contentService.SingleOrDefault(model.Id);
            if (entity == null) return HttpNotFound();

            var canEdit = (entity.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent);
            if (canEdit == false) return HttpNotFound();

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            #region Update
            var now = DateTime.Now;
            var startDatePublish = now;
            DateTime? endDatePublish = now;
            if (model.StartDate.HasValue)
            {
                if (model.StartDateTime.HasValue)
                {
                    startDatePublish = new DateTime(
                        model.StartDate.Value.Year,
                        model.StartDate.Value.Month,
                        model.StartDate.Value.Day,
                        model.StartDateTime.Value.Hours,
                        model.StartDateTime.Value.Minutes, 0);
                }
                else
                {
                    startDatePublish = new DateTime(
                    model.StartDate.Value.Year,
                    model.StartDate.Value.Month,
                    model.StartDate.Value.Day,
                    now.Hour,
                    now.Minute, 0);
                }
            }
            if (model.EndDate.HasValue)
            {
                if (model.EndDateTime.HasValue)
                {
                    endDatePublish = new DateTime(
                        model.EndDate.Value.Year,
                        model.EndDate.Value.Month,
                        model.EndDate.Value.Day,
                        model.EndDateTime.Value.Hours,
                        model.EndDateTime.Value.Minutes, 0);
                }
                else
                {
                    startDatePublish = new DateTime(
                    model.EndDate.Value.Year,
                    model.EndDate.Value.Month,
                    model.EndDate.Value.Day,
                    now.Hour,
                    now.Minute, 0);
                }
            }


            entity.StartDate = startDatePublish;
            entity.EndDate = endDatePublish;
            entity.Title = model.Title;
            entity.BodyOverview = model.BodyOverview;
            entity.Body = model.Body;
            entity.AllowComments = model.AllowComments;
            entity.AllowViewComments = model.AllowViewComments;
            entity.EditedUserId = 1;
            entity.MetaTitle = model.MetaTitle;
            entity.MetaKeywords = model.MetaKeywords;
            entity.MetaDescription = model.MetaDescription;
            entity.EditDate = now;

            entity.ContentTages.Clear();
            entity.ContentTages = _contentTagService.GetTagesbyIdsAsync(model.TagIds);

            entity.ContentCategories.Clear();
            entity.ContentCategories = _categoryService.GetbyIds(model.CategoryIds);


            if (model.TagIds == null) PopulateTags();
            else PopulateTags(model.TagIds.ToArray());

            if (model.CategoryIds == null) await PopulateCategories();
            else await PopulateCategories(model.CategoryIds.ToArray());



            #endregion

            #region Add Avatar Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesContentPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                entity.Picture = picture;
            }

            #endregion

            if (entity.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesContentPath, entity.Picture.FileName);


            _contentService.Update(entity);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Content.Index());
        }


        #endregion


        #region Private

        [NonAction]
        private void PopulateTags(params int[] selectedIds)
        {
            var tages = _contentTagService.All().ToList().Select(x => new
            SelectListItem
            {
                Text = x.Text,
                Value = x.Id.ToString(),
                Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            }).ToList();
            ViewBag.Tages = tages;
        }



        private async Task PopulateCategories(params int[] selectedIds)
        {
            var list = (await _categoryService.All().Where(x => !x.IsDelete && x.ParentId == null).Include(x => x.SubCategories).ToListAsync()).Select(x => new
              ContentCategoryTreeJsModel()
            {
                Name = x.Name,
                Id = x.Id,
                Childs = x.SubCategories.Select(z => new ContentCategoryTreeJsModel
                {
                    Id = z.Id,
                    Name = z.Name,
                    Selected = (selectedIds != null) ? selectedIds.Any(y => y == z.Id) : false,
                    Childs = z.SubCategories.Select(d => new ContentCategoryTreeJsModel
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Selected = (selectedIds != null) ? selectedIds.Any(y => y == d.Id) : false,
                        Childs = d.SubCategories.Select(s => new ContentCategoryTreeJsModel
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Selected = (selectedIds != null) ? selectedIds.Any(y => y == s.Id) : false
                        }).ToList()
                    }).ToList()
                }).ToList(),
                Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            }).ToList(); ;
            ViewBag.Categories = list;
        }
        #endregion
    }
}