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
using Netotik.ViewModels.Shop.Discount;
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست تخفیف ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopDiscountController : BaseController
    {

        #region ctor
        private readonly IDiscountService _discountService;
        private readonly IUnitOfWork _uow;

        public ShopDiscountController(
            IDiscountService discountService,
            IUnitOfWork uow)
        {
            _discountService = discountService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessDiscount)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _discountService.GetDataTable(Search)
                .ToPagedList<TableDiscountModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopDiscount.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopDiscount.ActionNames.Index, pageList);

        }
        #endregion


        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateDiscount)]
        [BreadCrumb(Title = "تخفیف جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            return View(new DiscountModel()
            {
                DiscountType = DiscountType.ManufacturerDiscount
            });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateDiscount)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(DiscountModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            if (model.UsePercentage && model.DiscountPercentage == null)
            {
                ModelState.AddModelError("DiscountPercentage", "درصد تخفیف را وارد کنید");
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }
            else if (!model.UsePercentage && model.DiscountAmount == null)
            {
                ModelState.AddModelError("DiscountAmount", "میزان تخفیف را وارد کنید");
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }

            if (model.RequiersCouponCode && string.IsNullOrWhiteSpace(model.CouponCode))
            {
                ModelState.AddModelError("CouponCode", "کد کوپن را وارد کنید");
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }

            #region Initial Content
            var discount = new Discount()
            {
                Name = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                DiscountType = model.DiscountType,
                Description = model.Description,
                UsePercentage = model.UsePercentage,
                RequiersCouponCode = model.RequiersCouponCode,
                CouponCode = (model.RequiersCouponCode) ? model.CouponCode : null,
                DiscountPercentage = (model.UsePercentage) ? model.DiscountPercentage : null,
                DiscountAmount = (model.UsePercentage) ? null : model.DiscountAmount,
                DiscountLimitationType = 0,
                MaximumDiscountAmount = model.MaximumDiscountAmount,
                MaximumDiscountQuantity = model.MaximumDiscountQuantity
            };

            #endregion

            _discountService.Add(discount);

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
            return RedirectToAction(MVC.Admin.ShopDiscount.Index());

        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessDiscount)]
        public virtual ActionResult Detail(int id)
        {
            var discount = _discountService.SingleOrDefault(id);
            if (discount == null) return HttpNotFound();
            return View(discount);
        }

        #endregion



        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteDiscount)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var type = _discountService.SingleOrDefault(id);
            if (type != null)
            {
                _discountService.Remove(type);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ShopDiscount.ActionNames.Index);
        }


        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditDiscount)]
        [BreadCrumb(Title = "ویرایش تخفیف", Order = 1)]
        public virtual ActionResult Edit(int id)
        {

            var model = _discountService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();


            var editModel = new DiscountModel
            {
                Id = model.Id,
                Name = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                UsePercentage = model.UsePercentage,
                RequiersCouponCode = model.RequiersCouponCode,
                DiscountType = model.DiscountType,
                Description = model.Description,
                CouponCode = (model.RequiersCouponCode) ? model.CouponCode : null,
                DiscountPercentage = (model.UsePercentage) ? model.DiscountPercentage : null,
                DiscountAmount = (model.UsePercentage) ? null : model.DiscountAmount,
                MaximumDiscountAmount = model.MaximumDiscountAmount,
                MaximumDiscountQuantity = model.MaximumDiscountQuantity
            };



            return View(editModel);

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditDiscount)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(DiscountModel model, ActionType actionType)
        {
            var discount = _discountService.SingleOrDefault(model.Id);
            if (discount == null) return HttpNotFound();



            if (model.UsePercentage && model.DiscountPercentage == null)
            {
                ModelState.AddModelError("DiscountPercentage", "درصد تخفیف را وارد کنید");
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }
            else if (!model.UsePercentage && model.DiscountAmount == null)
            {
                ModelState.AddModelError("DiscountAmount", "میزان تخفیف را وارد کنید");
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }
            if (model.RequiersCouponCode && string.IsNullOrWhiteSpace(model.CouponCode))
            {
                ModelState.AddModelError("CouponCode", "کد کوپن را وارد کنید");
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            #region Update

            discount.Name = model.Name;
            discount.StartDate = model.StartDate;
            discount.EndDate = model.EndDate;
            discount.UsePercentage = model.UsePercentage;
            discount.RequiersCouponCode = model.RequiersCouponCode;
            discount.DiscountType = model.DiscountType;
            discount.Description = model.Description;
            discount.CouponCode = (model.RequiersCouponCode) ? model.CouponCode : null;
            discount.DiscountPercentage = (model.UsePercentage) ? model.DiscountPercentage : null;
            discount.DiscountAmount = (model.UsePercentage) ? null : model.DiscountAmount;
            discount.MaximumDiscountAmount = model.MaximumDiscountAmount;
            discount.MaximumDiscountQuantity = model.MaximumDiscountQuantity;

            #endregion

            _discountService.Update(discount);

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
            return RedirectToAction(MVC.Admin.ShopDiscount.Index());
        }

        #endregion

    }
}