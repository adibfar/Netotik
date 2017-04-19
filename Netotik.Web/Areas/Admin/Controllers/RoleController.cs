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
using Netotik.ViewModels.Identity.Role;
using Netotik.Services.Identity;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessRole)]
    [BreadCrumb(Title = "لیست گروههای کاربری", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class RoleController : BaseController
    {

        #region ctor
        private readonly IApplicationRoleManager _applicationRoleManagerService;
        //private readonly IPermissonService _PermissonService;
        private readonly IUnitOfWork _uow;

        public RoleController(
            IApplicationRoleManager applicationRoleManagerService,
            //  IPermissonService PermissonService,
            IUnitOfWork uow)
        {
            //_PermissonService = PermissonService;
            _applicationRoleManagerService = applicationRoleManagerService;
            _uow = uow;
        }
        #endregion


        #region Index
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = new List<RoleViewModel>(); //_applicationRoleManagerService.GetDataTableRole(Search).ToPagedList<RoleViewModel>(Page, PageSize);
            if (Request.IsAjaxRequest())
                return View(MVC.Admin.Role.Views._Table, pageList);
            else
                return View(MVC.Admin.Role.ActionNames.Index, pageList);
        }
        #endregion


        #region Create

        [BreadCrumb(Title = "گروه جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await LoadRoles();
            PopulatePermissones();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(AddEditRoleViewModel model, ActionType actionType)
        {
            //if (model.PermissonIds != null && model.PermissonIds.Count() > 0)
            //    PopulatePermissones(model.PermissonIds);
            //else PopulatePermissones();

            //await LoadRoles(model.ParentId);

            //if (!ModelState.IsValid)
            //{

            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
            //    return View(model);
            //}
            //else
            //{
            //    var now = DateTime.Now;

            //    var role = new Role()
            //    {
            //        IsSystemRole = false,
            //        ParentRoleId = model.ParentId,
            //        IsDefaultRoleRegisteredUser = model.IsDefaultRoleRegisteredUser,
            //        TaxExemt = model.TaxExemt,
            //        FreeShipping = model.FreeShipping,
            //        Active = model.Active,
            //        Name = model.Name,
            //        SystemName = model.SystemName
            //    };

            //    role.Permissons = await _PermissonService.GetPermissonesbyIdsAsync(model.PermissonIds);

            //    if (model.IsDefaultRoleRegisteredUser)
            //        await _applicationRoleManagerService.DisableAllDefaultRoleRegister();

            //    _applicationRoleManagerService.Add(role);


            //    if (result.Status)
            //    {
            //        try
            //        {
            //            await _uow.SaveChangesAsync();
            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //        catch (Exception ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //    }
            //    SetResultMessage(result);
            //    if (!result.Status) return View(model);

            //    if (actionType == ActionType.SaveContinue) return RedirectToAction(MVC.Admin.Role.Edit(role.Id));
            //    return RedirectToAction(MVC.Admin.Role.Index());
            //}

            return View();


        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessRole)]
        public virtual ActionResult Detail(int id)
        {
            //var user = _applicationRoleManagerService.SingleOrDefault(id);
            //if (user == null)
            //    return RedirectToAction(MVC.Admin.Role.ActionNames.Index);

            //ViewBag.logins = user.LoginHistories.OrderByDescending(x => x.RegisterDate).Take(10).ToList();
            //return View(user);
            return View();
        }

        #endregion


        #region Edit
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {

            //await _applicationRoleManagerService.Remove(id);
            //await _uow.SaveChangesAsync();
            //return RedirectToAction(MVC.Admin.Role.ActionNames.Index);

            return View();
        }

        [BreadCrumb(Title = "ویرایش گروه", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            //var model = _applicationRoleManagerService.SingleOrDefault(id);
            //if (model == null)
            //    return RedirectToAction(MVC.Admin.Role.ActionNames.Index);

            //await LoadRoles(model.ParentRoleId);

            //PopulatePermissones(model.Permissons.Select(x => x.Id).ToArray());
            //return View(new AddEditRoleViewModel
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    SystemName = model.SystemName,
            //    FreeShipping = model.FreeShipping,
            //    TaxExemt = model.TaxExemt,
            //    Active = model.Active,
            //    IsDefaultRoleRegisteredUser = model.IsDefaultRoleRegisteredUser
            //});
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(AddEditRoleViewModel model, ActionType actionType)
        {
            //var user = _applicationRoleManagerService.SingleOrDefault(model.Id.Value);
            //if (user == null)
            //    return RedirectToAction(MVC.Admin.Role.ActionNames.Index);

            //await LoadRoles(model.ParentId);
            //PopulatePermissones(model.PermissonIds);

            //if (ModelState.IsValid)
            //{
            //    user.Name = model.Name;
            //    user.SystemName = model.SystemName;
            //    user.FreeShipping = model.FreeShipping;
            //    user.TaxExemt = model.TaxExemt;
            //    user.Active = model.Active;
            //    user.IsDefaultRoleRegisteredUser = model.IsDefaultRoleRegisteredUser;

            //    user.Permissons.Clear();
            //    user.Permissons = await _PermissonService.GetPermissonesbyIdsAsync(model.PermissonIds);

            //    if (model.IsDefaultRoleRegisteredUser)
            //        await _applicationRoleManagerService.DisableAllDefaultRoleRegister();

            //    _applicationRoleManagerService.Update(user);



            //    if (result.Status)
            //    {
            //        try
            //        {
            //            await _uow.SaveChangesAsync();
            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //        catch (Exception ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //    }
            //    SetResultMessage(result);
            //    if (!result.Status) return View();
            //}

            //if (actionType == ActionType.SaveContinue) return RedirectToAction(MVC.Admin.Role.Edit(model.Id.Value));
            //return RedirectToAction(MVC.Admin.Role.Index());

            return View();
        }

        #endregion



        #region RemoteValidations



        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsRoleNameExist(string name, int? id)
        {
            //return await _applicationRoleManagerService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
            return Json(false);
        }

        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsRoleSystemNameExist(string systemName, int? id)
        {
            //return await _applicationRoleManagerService.ExistsBySystemNameAsync(systemName, id) ? Json(false) : Json(true);
            return Json(false);
        }

        #endregion



        #region Private
        [NonAction]
        private void PopulatePermissones(params int[] selectedIds)
        {
            //var permitssons = _PermissonService.All().ToList().Select(x => new
            //SelectListItem
            //{
            //    Text = x.Description,
            //    Value = x.Id.ToString(),
            //    Group = new SelectListGroup() { Name = ((short)x.Section).ToString() },
            //    Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            //}).ToList();
            //ViewBag.Permissones = permitssons;
        }


        private async Task LoadRoles(int? selectedId = null)
        {
            //var list = await _applicationRoleManagerService.All().ToListAsync();
            //ViewBag.Roles = new SelectList(list, "Id", "Name", selectedId);
        }


        #endregion
    }
}