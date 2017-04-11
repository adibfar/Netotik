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
using Netotik.ViewModels.Shop.ShippingByWeight;
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    public partial class ShopShippingByWeightController : BaseController
    {

        #region ctor
        private readonly IShippingMethodService _shippingMethodService;
        private readonly IShippingByWeightService _shippingByWeightService;
        private readonly IUnitOfWork _uow;

        public ShopShippingByWeightController(
            IShippingByWeightService shippingByWeightService,
            IShippingMethodService shippingMethodService,
            IUnitOfWork uow)
        {
            _shippingByWeightService = shippingByWeightService;
            _shippingMethodService = shippingMethodService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessShopShippingByWeight)]
        public virtual ActionResult Index(int? Id)
        {
            if (Id == null) return HttpNotFound();
            ViewBag.Id = Id;
            var pageList = _shippingByWeightService.GetDataTable(Id.Value).ToList();

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopShippingByWeight.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopShippingByWeight.ActionNames.Index, pageList);

        }
        #endregion


        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateShopShippingByWeight)]
        public virtual ActionResult Create(int? Id)
        {
            var model = _shippingMethodService.SingleOrDefault(Id);
            if (model == null) return HttpNotFound();

            return View(new ShippingByWeightModel() { AdditionalFixedPrice = 0, FromWeight = 0, ToWeight = 0, ShippingMethodId = Id.Value });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateShopShippingByWeight)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ShippingByWeightModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            if (_shippingMethodService.SingleOrDefault(model.ShippingMethodId) == null) return HttpNotFound();

            #region Initial Content
            var shipping = new ShippingByWeight()
            {
                AdditionalFixedPrice = model.AdditionalFixedPrice,
                FromWeight = model.FromWeight,
                ToWeight = model.ToWeight,
                ShippingMethodId = model.ShippingMethodId
            };

            #endregion

            _shippingByWeightService.Add(shipping);

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
            return RedirectToAction(MVC.Admin.ShopShippingByWeight.Index(model.ShippingMethodId));

        }
        #endregion




        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteShopShippingByWeight)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var shipping = _shippingByWeightService.SingleOrDefault(id);
            if (shipping != null)
            {
                _shippingByWeightService.Remove(shipping);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ShopShippingByWeight.ActionNames.Index, new { id = shipping.ShippingMethodId });
        }


        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditShopShippingByWeight)]
        public virtual ActionResult Edit(int id)
        {

            var model = _shippingByWeightService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();


            var editModel = new ShippingByWeightModel
            {
                Id = model.Id,
                ShippingMethodId = model.ShippingMethodId,
                FromWeight = model.FromWeight,
                ToWeight = model.ToWeight,
                AdditionalFixedPrice = model.AdditionalFixedPrice

            };

            return View(editModel);

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditShopShippingByWeight)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ShippingByWeightModel model, ActionType actionType)
        {
            var shipping = _shippingByWeightService.SingleOrDefault(model.Id);
            if (shipping == null) return HttpNotFound();

            if (ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            shipping.AdditionalFixedPrice = model.AdditionalFixedPrice;
            shipping.FromWeight = model.FromWeight;
            shipping.ToWeight = model.ToWeight;


            _shippingByWeightService.Update(shipping);

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
            return RedirectToAction(MVC.Admin.ShopShippingByWeight.Index(model.ShippingMethodId));

        }

        #endregion

    }
}