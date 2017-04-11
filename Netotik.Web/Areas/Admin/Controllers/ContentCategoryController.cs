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
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.CMS.ContentCategory;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست موضوعات مطالب", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ContentCategoryController : BaseController
    {

        #region ctor
        private readonly IContentCategoryService _contentCategoryService;
        private readonly IUnitOfWork _uow;

        public ContentCategoryController(
            IContentCategoryService contentCAtegoryService,
            IUnitOfWork uow)
        {
            _contentCategoryService = contentCAtegoryService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContentCategory)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _contentCategoryService.GetDataTableSubject(Search)
                .ToPagedList<TableContentCategoryModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ContentCategory.Views._Table, pageList);
            else
                return View(MVC.Admin.ContentCategory.ActionNames.Index, pageList);

        }
        #endregion


        #region Create
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateContentCategory)]
        [BreadCrumb(Title = "موضوع جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await LoadCategories();
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateContentCategory)]
        [ValidateAntiForgeryToken]
        [HttpPost,]
        public virtual async Task<ActionResult> Create(ContentCategoryModel model, ActionType actionType)
        {
            await LoadCategories(model.ParentId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View(model);
            }

            var subject = new ContentCategory()
            {
                showInMenu = model.showInMenu,
                Name = model.Name,
                Description = model.Description,
                ParentId = model.ParentId
            };

            _contentCategoryService.Add(subject);


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
            return RedirectToAction(MVC.Admin.ContentCategory.Index());
        }
        #endregion


        #region Detail
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContentCategory)]
        public virtual ActionResult Detail(int id)
        {
            var sub = _contentCategoryService.SingleOrDefault(id);
            if (sub == null)
                return RedirectToAction(MVC.Admin.ContentCategory.ActionNames.Index);

            return View(sub);
        }

        #endregion


        #region Edit
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteContentCategory)]
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {

            var subject = _contentCategoryService.SingleOrDefault(id);
            if (subject != null)
            {
                subject.IsDelete = true;
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ContentCategory.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditContentCategory)]
        [BreadCrumb(Title = "ویرایش موضوع", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = _contentCategoryService.SingleOrDefault(id);
            if (model == null) return RedirectToAction(MVC.Admin.ContentCategory.ActionNames.Index);
            await LoadCategories(model.ParentId, model.Id);

            return View(new ContentCategoryModel
            {
                Id = model.Id,
                showInMenu = model.showInMenu,
                Name = model.Name,
                Description = model.Description,
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditContentCategory)]
        public virtual async Task<ActionResult> Edit(ContentCategoryModel model, ActionType actionType)
        {
            var cat = _contentCategoryService.SingleOrDefault(model.Id);
            if (cat == null) return RedirectToAction(MVC.Admin.ContentCategory.ActionNames.Index);

            await LoadCategories(model.Id, model.ParentId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            cat.Name = model.Name;
            cat.Description = model.Description;
            cat.showInMenu = model.showInMenu;
            cat.ParentId = model.ParentId;
            _contentCategoryService.Update(cat);
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateSuccess);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.ContentCategory.Index());
        }

        #endregion



        #region Private

        private async Task LoadCategories(int? selectedId = null, int? catId = null)
        {
            var list = await _contentCategoryService.All().Where(x => !x.IsDelete).ToListAsync();
            if (catId.HasValue) list = list.Where(x => x.Id != catId.Value).ToList();
            ViewBag.Categories = new SelectList(list, "Id", "Name", selectedId);
        }

        #endregion

        #region RemoteValidations



        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsSubjectNameExist(string name, int? id)
        {
            return await _contentCategoryService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
        }

        #endregion



    }
}