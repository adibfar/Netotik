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
using Netotik.ViewModels.Shop.ShippingMethod;
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessShopShippingByMethod)]
    [BreadCrumb(Title = "لیست روش های ارسال", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopShippingMethodController : BaseController
    {

        #region ctor
        private readonly IShippingMethodService _shippingMethodService;
        private readonly IUnitOfWork _uow;

        public ShopShippingMethodController(
            IShippingMethodService shippingMethodService,
            IUnitOfWork uow)
        {
            _shippingMethodService = shippingMethodService;
            _uow = uow;
        }
        #endregion


        #region Index
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _shippingMethodService.GetDataTable(Search)
                .ToPagedList<TableShippingMethodModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopShippingMethod.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopShippingMethod.ActionNames.Index, pageList);

        }
        #endregion


        #region Create

        [BreadCrumb(Title = "روش جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            return View(new ShippingMethodModel() { BasePrice = 0, DisplayOrder = 0, IsActive = true });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ShippingMethodModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            #region Initial Content
            var metod = new ShippingMethod()
            {
                Name = model.Name,
                DisplayOrder = model.DisplayOrder,
                Description = model.Description,
                IsActive = model.IsActive,
                IsDelete = false,
                IsDefault = model.IsDefault,
                PriceAfterRecive = model.PriceAfterRecive,
                BasePrice = model.BasePrice
            };

            #endregion

            #region Add Content Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopShippingMethodPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                metod.Picture = picture;
            }
            #endregion

            _shippingMethodService.Add(metod);

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
            return RedirectToAction(MVC.Admin.ShopShippingMethod.Index());

        }
        #endregion


        #region Detail

        public virtual ActionResult Detail(int id)
        {
            var man = _shippingMethodService.SingleOrDefault(id);
            if (man == null) return HttpNotFound();
            return View(man);
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var shipping = _shippingMethodService.SingleOrDefault(id);
            if (shipping != null)
            {
                _shippingMethodService.Remove(shipping);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ShopShippingMethod.ActionNames.Index);
        }


        #region Edit

        [BreadCrumb(Title = "ویرایش روش", Order = 1)]
        public virtual ActionResult Edit(int id)
        {

            var model = _shippingMethodService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();


            if (model.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopShippingMethodPath, model.Picture.FileName);

            var editModel = new ShippingMethodModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                DisplayOrder = model.DisplayOrder,
                BasePrice = model.BasePrice,
                IsDefault = model.IsDefault,
                PriceAfterRecive = model.PriceAfterRecive,
                IsActive = model.IsActive
            };

            return View(editModel);

        }

        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ShippingMethodModel model, ActionType actionType)
        {
            var method = _shippingMethodService.SingleOrDefault(model.Id);
            if (method == null) return HttpNotFound();



            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            #region Update

            method.Name = model.Name;
            method.Description = model.Description;
            method.IsActive = model.IsActive;
            method.IsDefault = model.IsDefault;
            method.DisplayOrder = model.DisplayOrder;
            method.BasePrice = model.BasePrice;
            method.PriceAfterRecive = model.PriceAfterRecive;
            #endregion


            #region Add Avatar Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopShippingMethodPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                method.Picture = picture;
            }


            if (method.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopShippingMethodPath, method.Picture.FileName);


            #endregion


            _shippingMethodService.Update(method);
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
            return RedirectToAction(MVC.Admin.ShopShippingMethod.Index());

        }

        #endregion


        #region RemoteValidations



        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsNameExist(string name, int? id)
        {
            return await _shippingMethodService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
        }

        #endregion

    }
}