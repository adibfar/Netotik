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
using Netotik.ViewModels.CMS.ContentTag;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست برچسب ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ContentTagController : BaseController
    {


        #region ctor
        private readonly IContentTagService _contentTagService;
        private readonly IContentService _contentService;
        private readonly IUnitOfWork _uow;

        public ContentTagController(
            IContentTagService contentTagService,
            IContentService contentService,
            IUnitOfWork uow)
        {
            _contentService = contentService;
            _contentTagService = contentTagService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessTag)]
        public virtual ActionResult Index(string Search = "")
        {

            var pageList = _contentTagService.All()
                .Where(x => x.Text.Contains(Search))
                .ToList();

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ContentTag.Views._Table, pageList);
            else
                return View(MVC.Admin.ContentTag.ActionNames.Index, pageList);

        }
        #endregion


        #region Create
        [BreadCrumb(Title = "برچسب جدید", Order = 1)]
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateTag)]
        public virtual ActionResult Create()
        {
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateTag)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ContentTagModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }


            var tag = new ContentTag()
            {
                Text = model.Name
            };

            _contentTagService.Add(tag);


            try
            {
                await _uow.SaveChangesAsync();
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.ContentTag.Index());
        }

        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessTag)]
        public virtual ActionResult Detail(int id)
        {
            var tag = _contentTagService.SingleOrDefault(id);
            if (tag == null) return HttpNotFound();
            return View(tag);
        }

        #endregion


        #region Edit
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteTag)]
        [HttpPost]
        [BreadCrumb(Title = "ویرایش برچسب", Order = 1)]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var tag = new ContentTag { Id = id };
            _contentTagService.Remove(tag);
            await _uow.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.ContentTag.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditTag)]
        public virtual ActionResult Edit(int id)
        {
            var model = _contentTagService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.ContentTag.ActionNames.Index);

            return View(new ContentTagModel
            {
                Id = model.Id,
                Name = model.Text
            });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditTag)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ContentTagModel model, ActionType actionType)
        {

            var tag = _contentTagService.SingleOrDefault(model.Id);
            if (tag == null)
                return RedirectToAction(MVC.Admin.ContentTag.ActionNames.Index);


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            tag.Text = model.Name;

            _contentTagService.Update(tag);

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
            return RedirectToAction(MVC.Admin.ContentTag.Index());
        }

        #endregion



        #region private

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsTagNameExist(string name, int? id)
        {
            return await _contentTagService.IsExistByName(name, id) ? Json(false) : Json(true);
        }
        #endregion



    }
}