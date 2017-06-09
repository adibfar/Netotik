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

using System.IO;
using Microsoft.AspNet.Identity;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.CMS.Content;
using Netotik.ViewModels.CMS.ContentCategory;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "ContentsList", UseDefaultRouteUrl = true,Order = 0, GlyphIcon = "icon-files-o")]
    public partial class ContentController : BasePanelController
    {

        #region ctor
        private readonly ILanguageService _languageService;
        private readonly IContentTagService _contentTagService;
        private readonly IContentService _contentService;
        private readonly IContentCategoryService _categoryService;
        private readonly IApplicationUserManager _applicationUserManagerService;
        private readonly IUnitOfWork _uow;

        public ContentController(
            ILanguageService languageService,
            IContentCategoryService categoryService,
            IContentTagService contentTagService,
            IApplicationUserManager applicationUserManagerService,
            IContentService contentService,
            IUnitOfWork uow)
        {
            _languageService = languageService;
            _categoryService = categoryService;
            _contentTagService = contentTagService;
            _applicationUserManagerService = applicationUserManagerService;
            _contentService = contentService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContent)]
        public virtual ActionResult Index()
        {
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContent)]
        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _contentService.GetList(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateContent)]
        [BreadCrumb(Title = "NewContent", GlyphIcon = "icon-plus", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            PopulateLangauges();
            return View(await _contentService.GetForCreateAsync());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateContent)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ContentModel model, ActionType actionType)
        {

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Content.Create());
            }

            #region Initial Content
            var now = DateTime.Now;

            var content = new Netotik.Domain.Entity.Content()
            {
                AllowComments = model.AllowComments,
                AllowViewComments = model.AllowViewComments,
                Body = model.Body,
                BodyOverview = model.BodyOverview,
                HasSideBar = model.HasSideBar,
                DontShowBlog = model.DontShowBlog,
                DontShowImageDetail = model.DontShowImageDetail,
                LanguageId = model.LanguageId,
                CreateDate = now,
                CreatedUserId = User.Identity.GetUserId<long>(),
                EditDate = now,
                EditedUserId = User.Identity.GetUserId<long>(),
                status = ContentStatus.WaitForAccept,
                Title = model.Title,
                Url = model.Url,
                StartDate = now,
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
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.Content.Create());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.Content.Index());
        }
        #endregion


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteContent)]
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id)
        {
            var news = _contentService.SingleOrDefault(id);
            if (news == null) return HttpNotFound();
            var canDelete = (news.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent);
            if (canDelete)
            {
                _contentService.Remove(news);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.Content.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAcceptContent)]
        [HttpPost]
        public virtual async Task<ActionResult> Accept(int id)
        {
            var news = _contentService.SingleOrDefault(id);
            if (news == null) return HttpNotFound();
            var canAccept = (news.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllContent);
            if (canAccept)
            {
                news.status = ContentStatus.Accepted;
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.Content.Index());
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDontAcceptContent)]
        [HttpPost]
        public virtual async Task<ActionResult> NotAccept(int id)
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
        [BreadCrumb(Title = "EditContent", GlyphIcon = "icon-edit2", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = await _contentService.GetForEditAsync(id);
            PopulateLangauges(model.LanguageId);
            return View(model);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditContent)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ContentModel model, ActionType actionType)
        {
            var entity = _contentService.SingleOrDefault(model.Id);
            if (entity == null) return HttpNotFound();

            var canEdit = (entity.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanEditContent);
            if (canEdit == false) return HttpNotFound();

            PopulateLangauges(model.LanguageId);
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View();
            }

            #region Update
            var now = DateTime.Now;

            entity.StartDate = now;
            entity.Title = model.Title;
            entity.Url = model.Url;
            entity.BodyOverview = model.BodyOverview;
            entity.Body = model.Body;
            entity.HasSideBar = model.HasSideBar;
            entity.DontShowBlog = model.DontShowBlog;
            entity.DontShowImageDetail = model.DontShowImageDetail;
            entity.AllowComments = model.AllowComments;
            entity.LanguageId = model.LanguageId;
            entity.AllowViewComments = model.AllowViewComments;
            entity.EditedUserId = User.Identity.GetUserId<long>();
            entity.MetaTitle = model.MetaTitle;
            entity.MetaKeywords = model.MetaKeywords;
            entity.MetaDescription = model.MetaDescription;
            entity.EditDate = now;

            entity.ContentTages.Clear();
            entity.ContentTages = _contentTagService.GetTagesbyIdsAsync(model.TagIds);

            entity.ContentCategories.Clear();
            entity.ContentCategories = _categoryService.GetbyIds(model.CategoryIds);

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
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return View();
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Content.Index());
        }
        #endregion

        private void PopulateLangauges(int? selectedId = null)
        {
            var list = _languageService.All().Where(x => x.Published).ToList();
            ViewBag.Languages = new SelectList(list, "Id", "Name", selectedId);
        }

        [HttpPost]
        [AjaxOnly]
        public virtual ActionResult FillTag(int? languageId)
        {
            return languageId.HasValue ? Json(_contentTagService.GetByLanguageId(languageId.Value)) : Json(null);
        }


    }
}