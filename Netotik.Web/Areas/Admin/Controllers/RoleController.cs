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
using WebGrease.Css.Extensions;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Identity.Role;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using System.Net;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessRole)]
    [BreadCrumb(Title = "لیست گروههای کاربری", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class RoleController : BaseController
    {

        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationRoleManager _roleManager;

        #endregion

        #region Const

        public RoleController(IUnitOfWork unitOfWork, IApplicationRoleManager roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }

        #endregion

        #region Index
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _roleManager.GetList(model, out totalCount, out showCount);

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

        [HttpGet]
        public virtual ActionResult Create()
        {
            PopulatePermissions();
            return View(MVC.Admin.Role.Views._Create);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(RoleModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail,Messages.InvalidDataError);
                PopulatePermissions(viewModel.PermissionNames);
                return View(viewModel);
            }
            if (!await _roleManager.AddRole(viewModel))
            {
                this.MessageError(Messages.MissionFail,"لطفا برای گروه کاربری مورد نظر ، دسترسی تعیین کنید");
                PopulatePermissions();
                return View(viewModel);
            }

            await _unitOfWork.SaveChangesAsync();
            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.Role.Index());
        }

        #endregion

        #region Edit
        [HttpGet]
        public virtual async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _roleManager.GetRoleByIdAsync(id.Value);
            if (viewModel == null)
                return HttpNotFound();

            PopulatePermissions(viewModel.PermissionNames);
            return PartialView(MVC.Admin.Role.Views._Edit,viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(RoleModel viewModel)
        {
            if (_roleManager.ChechForExisByName(viewModel.Name, viewModel.Id))
                ModelState.AddModelError("Name", "این گروه  قبلا در سیستم ثبت شده است");

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                PopulatePermissions(viewModel.PermissionNames);
                return RedirectToAction(MVC.Admin.Role.Index());
            }

            var dbRole = await _roleManager.FindByIdAsync(viewModel.Id.Value);
            if (dbRole == null)
                return HttpNotFound();


            if (!await _roleManager.EditRole(viewModel))
            {
                this.MessageError(Messages.MissionFail, "لطفا برای گروه کاربری مورد نظر ، دسترسی تعیین کنید");
                PopulatePermissions();
                return RedirectToAction(MVC.Admin.Role.Index());
            }

            await _unitOfWork.SaveChangesAsync();
            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Role.Index());
        }

        #endregion

        #region Delete

        [HttpPost]
        public virtual async Task<ActionResult> Remove(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (await _roleManager.CheckRoleIsSystemRoleAsync(id.Value))
            {
                this.MessageError(Messages.MissionFail,"این گروه کاربری سیستمی است و حذف آن باعث اختلال در سیستم خواهد شد");
                return RedirectToAction(MVC.Admin.Role.Index());
            }
            await _roleManager.RemoveById(id.Value);
            this.MessageSuccess(Messages.MissionSuccess, Messages.RemoveSuccess);
            return RedirectToAction(MVC.Admin.Role.Index());
        }

        #endregion

        #region RemoteValidation

        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsRoleNameAvailable(string name, int? id)
        {
            return _roleManager.ChechForExisByName(name, id) ? Json(false) : Json(true);
        }

        #endregion

        #region Private
        [NonAction]
        private void PopulatePermissions(params string[] selectedpermissions)
        {
            var permissions = AssignableToRolePermissions.GetAsSelectListItems();

            if (selectedpermissions != null)
            {
                permissions.ForEach(
                    a => a.Selected = selectedpermissions.Any(s => s == a.Value));
            }

            ViewBag.Permissions = permissions;
        }

        #endregion
    }
}