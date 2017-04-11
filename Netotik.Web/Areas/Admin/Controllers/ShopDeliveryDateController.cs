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
using Netotik.ViewModels.Shop.DeliveryDate;
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست زمانهای تحویل", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]

    public partial class ShopDeliveryDateController : BaseController
    {

        #region ctor
        private readonly IDeliveryDateService _deliveryDateService;
        private readonly IUnitOfWork _uow;

        public ShopDeliveryDateController(
            IDeliveryDateService deliveryDateService,
            IUnitOfWork uow)
        {
            _deliveryDateService = deliveryDateService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessDeliveryDate)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _deliveryDateService.GetDataTable(Search)
                .OrderBy(x => x.Order)
                .ToPagedList<TableDeliveryDateModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopDeliveryDate.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopDeliveryDate.ActionNames.Index, pageList);

        }
        #endregion


        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateDeliveryDate)]
        [BreadCrumb(Title = "زمان تحویل جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            return View(new DeliveryDateModel { DisplayOrder = 0 });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateDeliveryDate)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(DeliveryDateModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            #region Initial Content
            var delivery = new DeliveryDate()
            {
                IsDelete = false,
                Name = model.Name,
                DisplayOrder = model.DisplayOrder,
            };

            #endregion


            _deliveryDateService.Add(delivery);

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
            return RedirectToAction(MVC.Admin.ShopDeliveryDate.Index());
        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessDeliveryDate)]
        public virtual ActionResult Detail(int id)
        {
            var deli = _deliveryDateService.SingleOrDefault(id);
            if (deli == null) return HttpNotFound();
            return View(deli);
        }

        #endregion



        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteDeliveryDate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var deli = _deliveryDateService.SingleOrDefault(id);
            if (deli != null)
            {
                deli.IsDelete = true;
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ShopDeliveryDate.ActionNames.Index);
        }


        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditDeliveryDate)]
        [BreadCrumb(Title = "ویرایش زمان تحویل", Order = 1)]
        public virtual ActionResult Edit(int id)
        {

            var model = _deliveryDateService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();


            var editModel = new DeliveryDateModel
            {
                Id = model.Id,
                Name = model.Name,
                DisplayOrder = model.DisplayOrder,
            };

            return View(editModel);

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditDeliveryDate)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(DeliveryDateModel model, ActionType actionType)
        {
            var deli = _deliveryDateService.SingleOrDefault(model.Id);
            if (deli == null) return HttpNotFound();

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            deli.Name = model.Name;
            deli.DisplayOrder = model.DisplayOrder;

            _deliveryDateService.Update(deli);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return View();
            }
            this.MessageSuccess(Messages.MissionFail, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.ShopDeliveryDate.Index());
        }

        #endregion



    }
}