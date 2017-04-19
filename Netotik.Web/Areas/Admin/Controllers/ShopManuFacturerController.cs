using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MvcPaging;
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
using Netotik.ViewModels.Shop.Manufacturer;
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Filters;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessManufactur)]
    [BreadCrumb(Title = "لیست برند ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopManuFacturerController : BaseController
    {

        #region ctor
        private readonly IManufacturerService _manufacturerService;
        private readonly IDiscountService _discountService;
        private readonly IUnitOfWork _uow;

        public ShopManuFacturerController(
            IDiscountService discountService,
            IManufacturerService manufacturerService,
            IUnitOfWork uow)
        {
            _discountService = discountService;
            _manufacturerService = manufacturerService;
            _uow = uow;
        }
        #endregion


        #region Index
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _manufacturerService.GetDataTable(Search)
                .ToPagedList<TableManufacturerModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopManuFacturer.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopManuFacturer.ActionNames.Index, pageList);

        }
        #endregion


        #region Create

        [BreadCrumb(Title = "برند جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            PopulateDisCountes();
            return View(new ManufacturerModel() { DisplayOrder = 0, IsPublished = true });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ManufacturerModel model, ActionType actionType)
        {
            if (model.DiscountIds == null) PopulateDisCountes();
            else PopulateDisCountes(model.DiscountIds);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }

            #region Initial Content
            var now = DateTime.Now;
            var man = new Manufacturer()
            {
                Name = model.Name,
                CreateDate = now,
                EditDate = now,
                DisplayOrder = model.DisplayOrder,
                Description = model.Description,
                Url = model.Url,
                IsPublished = model.IsPublished,
                IsDeleted = false,
                ShowOnHomePage = model.ShowOnHomePage,
                MetaDescription = model.MetaDescription,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle
            };
            man.Discounts = _discountService.GetDiscountesbyIds(model.DiscountIds);
            #endregion

            #region Add Content Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopManuFacturerPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                man.Picture = picture;
            }
            #endregion

            _manufacturerService.Add(man);

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
            return RedirectToAction(MVC.Admin.ShopManuFacturer.Index());

        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessManufactur)]
        public virtual ActionResult Detail(int id)
        {
            var man = _manufacturerService.SingleOrDefault(id);
            if (man == null) return HttpNotFound();
            return View(man);
        }

        #endregion



        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var man = _manufacturerService.SingleOrDefault(id);
            if (man != null)
            {
                _manufacturerService.Remove(man);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ShopManuFacturer.ActionNames.Index);
        }


        #region Edit
        [BreadCrumb(Title = "ویرایش برند", Order = 1)]
        public virtual ActionResult Edit(int id)
        {

            var model = _manufacturerService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();

            PopulateDisCountes(model.Discounts.Select(x => x.Id).ToArray());

            if (model.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopManuFacturerPath, model.Picture.FileName);

            var editModel = new ManufacturerModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                DisplayOrder = model.DisplayOrder,
                MetaDescription = model.MetaDescription,
                Url = model.Url,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle,
                ShowOnHomePage = model.ShowOnHomePage,
                IsPublished = model.IsPublished
            };



            return View(editModel);

        }

        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ManufacturerModel model, ActionType actionType)
        {
            var man = _manufacturerService.SingleOrDefault(model.Id);
            if (man == null) return HttpNotFound();

            if (model.DiscountIds == null) PopulateDisCountes();
            else PopulateDisCountes(model.DiscountIds);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            #region Update

            man.Name = model.Name;
            man.EditDate = DateTime.Now;
            man.Description = model.Description;
            man.IsPublished = model.IsPublished;
            man.DisplayOrder = model.DisplayOrder;
            man.ShowOnHomePage = model.ShowOnHomePage;
            man.MetaDescription = model.MetaDescription;
            man.Url = model.Url;
            man.MetaKeywords = model.MetaKeywords;
            man.MetaTitle = model.MetaTitle;

            man.Discounts.Clear();
            man.Discounts = _discountService.GetDiscountesbyIds(model.DiscountIds);

            #endregion


            #region Add Avatar Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopManuFacturerPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                man.Picture = picture;
            }

            #endregion


            _manufacturerService.Update(man);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return View();
            }

            this.MessageError(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.ShopManuFacturer.Index());
        }

        #endregion



        #region Private

        [NonAction]
        private void PopulateDisCountes(params int[] selectedIds)
        {
            var discountes = _discountService.Where(x => x.DiscountType == DiscountType.ManufacturerDiscount).ToList().Select(x => new
            SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            }).ToList();
            ViewBag.Discountes = discountes;
        }
        #endregion


        #region RemoteValidations



        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsNameExist(string name, int? id)
        {
            return await _manufacturerService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
        }

        #endregion

    }
}