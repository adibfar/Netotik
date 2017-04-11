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
using Netotik.ViewModels.Shop.PaymentType;
using System.IO;
using Netotik.ViewModels;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست درگاه پرداخت", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopPaymentTypeController : BaseController
    {

        #region ctor
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IUnitOfWork _uow;

        public ShopPaymentTypeController(
            IPaymentTypeService paymentTypeService,
            IUnitOfWork uow)
        {
            _paymentTypeService = paymentTypeService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessPaymentType)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _paymentTypeService.GetDataTable(Search)
                .ToPagedList<TablePaymentTypeModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopPaymentType.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopPaymentType.ActionNames.Index, pageList);

        }
        #endregion


        #region Create
        [BreadCrumb(Title = "درگاه جدید", Order = 1)]
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreatePaymentType)]
        public virtual ActionResult Create()
        {
            return View(new PaymentTypeModel() { IsActive = true });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreatePaymentType)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(PaymentTypeModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            #region Initial Content
            var now = DateTime.Now;
            var type = new PaymentType()
            {
                Name = model.Name,
                CreateDate = now,
                EditDate = now,
                Description = model.Description,
                IsActive = model.IsActive,
                IsDefault = model.IsDefault,
                TerminalId = model.TerminalId,
                UserName = model.UserName,
                Password = model.Password,
                GateWayUrl = model.GateWayUrl,
            };

            #endregion

            #region Add Content Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopPaymentTypePath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                type.Picture = picture;
            }
            #endregion

            _paymentTypeService.Add(type);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }
            this.MessageError(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.ShopPaymentType.Index());

        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessPaymentType)]
        public virtual ActionResult Detail(int id)
        {
            var man = _paymentTypeService.SingleOrDefault(id);
            if (man == null) return HttpNotFound();
            return View(man);
        }

        #endregion



        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeletePaymentType)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var type = _paymentTypeService.SingleOrDefault(id);
            if (type != null)
            {
                _paymentTypeService.Remove(type);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ShopPaymentType.Index());
        }


        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditPaymentType)]
        [BreadCrumb(Title = "ویرایش درگاه", Order = 1)]
        public virtual ActionResult Edit(int id)
        {

            var model = _paymentTypeService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();


            if (model.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopPaymentTypePath, model.Picture.FileName);

            var editModel = new PaymentTypeModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive,
                IsDefault = model.IsDefault,
                TerminalId = model.TerminalId,
                Password = model.Password,
                UserName = model.UserName,
                GateWayUrl = model.GateWayUrl
            };



            return View(editModel);

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditPaymentType)]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(PaymentTypeModel model, ActionType actionType)
        {
            var type = _paymentTypeService.SingleOrDefault(model.Id);
            if (type == null) return HttpNotFound();



            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            #region Update

            type.Name = model.Name;
            type.EditDate = DateTime.Now;
            type.Description = model.Description;
            type.GateWayUrl = model.GateWayUrl;
            type.IsActive = model.IsActive;
            type.IsDefault = model.IsDefault;
            type.TerminalId = model.TerminalId;
            type.UserName = model.UserName;
            type.Password = model.Password;
            #endregion


            #region Add Avatar Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopPaymentTypePath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                type.Picture = picture;
            }

            #endregion


            _paymentTypeService.Update(type);

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
            return RedirectToAction(MVC.Admin.ShopPaymentType.Index());
        }

        #endregion


        #region RemoteValidations



        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsNameExist(string name, int? id)
        {
            return await _paymentTypeService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
        }

        #endregion

    }
}