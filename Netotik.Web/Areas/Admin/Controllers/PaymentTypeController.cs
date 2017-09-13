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

using Netotik.ViewModels.Shop.PaymentType;
using System.IO;
using Netotik.ViewModels;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessPaymentType)]
    [BreadCrumb(Title = "PaymentTypes", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon icon-table")]
    public partial class PaymentTypeController : BaseController
    {

        #region ctor
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IUnitOfWork _uow;

        public PaymentTypeController(
            IPaymentTypeService paymentTypeService,
            IUnitOfWork uow)
        {
            _paymentTypeService = paymentTypeService;
            _uow = uow;
        }
        #endregion


        #region Index
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _paymentTypeService.GetList(model, out totalCount, out showCount);

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
        public virtual ActionResult Create()
        {
            return PartialView(MVC.Admin.PaymentType.Views._Create, new PaymentTypeModel { IsActive = true });
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(PaymentTypeModel model, ActionType actionType = ActionType.Save)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.PaymentType.Index());
            }

            #region Initial Slider
            var paymentType = new Netotik.Domain.Entity.PaymentType()
            {
                Name = model.Name,
                Description = model.Description,
                GateWayUrl = model.GateWayUrl,
                IsDefault = model.IsDefault,
                Password = model.Password,
                TerminalId = model.TerminalId,
                UserName = model.UserName,
                CreateDate = DateTime.Now,
                IsActive = model.IsActive,
                IsDelete = false
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
                paymentType.Picture = picture;
            }
            #endregion

            _paymentTypeService.Add(paymentType);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.PaymentType.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.PaymentType.Index());
        }
        #endregion

        #region Edit
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var type = _paymentTypeService.All().Where(x => x.Id == id).Include(x => x.Picture).FirstOrDefault();
            if (type != null)
            {
                type.IsDelete = true;
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction(MVC.Admin.PaymentType.Index());
        }

        [BreadCrumb(Title = "Edit", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = await _paymentTypeService.All().Include(x => x.Picture).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return RedirectToAction(MVC.Admin.PaymentType.ActionNames.Index);

            if (model.PictureId.HasValue)
            {
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopPaymentTypePath, model.Picture.FileName);
            }
            var editModel = new PaymentTypeModel
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                Password = model.Password,
                GateWayUrl = model.GateWayUrl,
                IsDefault = model.IsDefault,
                UserName = model.UserName,
                TerminalId = model.TerminalId,
                IsActive = model.IsActive
            };

            return View(MVC.Admin.PaymentType.Views._Edit, editModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(PaymentTypeModel model, ActionType actionType)
        {
            var type = _paymentTypeService.SingleOrDefault(model.Id);
            if (type == null)
                return HttpNotFound();


            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.PaymentType.Index());
            }

            #region Update

            type.IsActive = model.IsActive;
            type.Name = model.Name;
            type.UserName = model.UserName;
            type.Description = model.Description;
            type.GateWayUrl = model.GateWayUrl;
            type.TerminalId = model.TerminalId;
            type.Password = model.Password;
            type.IsDelete = model.IsDefault;

            if (model.Image != null)
            {
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
            }
            #endregion

            _paymentTypeService.Update(type);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return RedirectToAction(MVC.Admin.PaymentType.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.PaymentType.Index());

        }
        #endregion
        

    }
}