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
using Netotik.ViewModels.Shop.WareHouse;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست انبار", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopWareHouseController : BaseController
    {

        #region ctor
        private readonly IStateService _addressStateService;
        private readonly ICityService _addressCityService;
        private readonly IWareHouseService _wareHouseService;
        private readonly IUnitOfWork _uow;

        public ShopWareHouseController(
            IWareHouseService wareHouseService,
            IStateService addressStateService,
            ICityService addressCityService,
            IUnitOfWork uow)
        {
            _wareHouseService = wareHouseService;
            _addressCityService = addressCityService;
            _addressStateService = addressStateService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessWareHouse)]
        public virtual ActionResult Index(string Search = "", int Page = 1, int PageSize = 10)
        {

            var pageList = _wareHouseService.GetDataTable(Search)
                .ToPagedList(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopWareHouse.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopWareHouse.ActionNames.Index, pageList);

        }
        #endregion


        #region Create
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateWareHouse)]
        [BreadCrumb(Title = "انبار جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await LoadState();
            return View(new WareHouseModel { IsActive = true });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateWareHouse)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(WareHouseModel model, ActionType actionType)
        {
            await LoadState(model.StateId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }

            var city = _addressCityService.SingleOrDefault(model.CityId);
            if (city == null)
            {
                this.MessageError(Messages.MissionFail, "شهر انتخاب شده نامعتبر است.");
                return View(model);
            }


            var wareHouse = new Warehouse()
            {
                Name = model.Name,
                IsActive = model.IsActive,
                AddressCityId = city.Id,
                AddressStateId = city.StateId,
                Description = model.Description,
                IsDelete = false,
                Address = model.Address
            };

            _wareHouseService.Add(wareHouse);


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
            return RedirectToAction(MVC.Admin.ShopWareHouse.Index());

        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessWareHouse)]
        public virtual ActionResult Detail(int id)
        {
            var wareHouse = _wareHouseService.SingleOrDefault(id);
            if (wareHouse == null) return HttpNotFound();
            return View(wareHouse);
        }

        #endregion


        #region Edit
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteWareHouse)]
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var ware = _wareHouseService.SingleOrDefault(id);
            if (ware == null) return HttpNotFound();
            ware.IsDelete = true;
            await _uow.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.ShopWareHouse.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditWareHouse)]
        [BreadCrumb(Title = "ویرایش انبار", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = _wareHouseService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.ShopWareHouse.ActionNames.Index);
            await LoadState(model.AddressStateId);

            return View(new WareHouseModel
            {
                Id = model.Id,
                Name = model.Name,
                IsActive = model.IsActive,
                StateId = model.AddressStateId,
                CityId = model.AddressCityId,
                Description = model.Description,
                Address = model.Address
            });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditWareHouse)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(WareHouseModel model, ActionType actionType)
        {

            var wareHouse = _wareHouseService.SingleOrDefault(model.Id);
            if (wareHouse == null)
                return RedirectToAction(MVC.Admin.ShopWareHouse.ActionNames.Index);

            await LoadState(model.StateId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            wareHouse.Name = model.Name;
            wareHouse.IsActive = model.IsActive;
            wareHouse.AddressStateId = model.StateId;
            wareHouse.AddressCityId = model.CityId;
            wareHouse.Description = model.Description;
            wareHouse.Address = model.Address;

            _wareHouseService.Update(wareHouse);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.ShopWareHouse.Index());
        }

        #endregion



        #region Private

        private async Task LoadState(int? selectedId = null)
        {
            var list = await _addressStateService.All().ToListAsync();
            ViewBag.States = new SelectList(list, "Id", "Name", selectedId);


            if (selectedId.HasValue)
            {
                ViewBag.Cities = new SelectList(_addressCityService.Where(x => x.StateId == selectedId).ToList(), "Id", "Name");
            }


        }


        #endregion


        public virtual ActionResult FillCity(int state)
        {
            var cities = _addressCityService.Where(c => c.StateId == state).Select(x => new { Id = x.Id, Name = x.Name }).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        #region RemoteValidations



        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsNameExist(string name, int? id)
        {
            return await _wareHouseService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
        }

        #endregion

    }
}