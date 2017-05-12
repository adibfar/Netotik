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

using Netotik.ViewModels.Shop.ProductAttribute;
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست مشخصات محصول", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopProductAttributeController : BaseController
    {

        #region ctor
        private readonly ICategoryService _categoryService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IUnitOfWork _uow;

        public ShopProductAttributeController(
            IProductAttributeService productAttributeService,
            ICategoryService categoryService,
            IUnitOfWork uow)
        {
            _productAttributeService = productAttributeService;
            _categoryService = categoryService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProductAttribute)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _productAttributeService.GetDataTable(Search)
                .ToPagedList<TableProductAttributeModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopProductAttribute.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopProductAttribute.ActionNames.Index, pageList);

        }
        #endregion


        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateProductAttribute)]
        [BreadCrumb(Title = "مشخصه جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await LoadCategories();
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateProductAttribute)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ProductAttributeModel model, ActionType actionType)
        {
            await LoadCategories(model.CategoryId);
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            #region Initial Content
            var attr = new ProductAttribute()
            {
                CategoryId = model.CategoryId,
                Name = model.Name,
                Description = model.Description,
            };

            #endregion

            _productAttributeService.Add(attr);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            this.MessageSuccess(Messages.MissionFail, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.ShopProductAttribute.Index());

        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProductAttribute)]
        public virtual ActionResult Detail(int id)
        {
            var attr = _productAttributeService.SingleOrDefault(id);
            if (attr == null) return HttpNotFound();
            return View(attr);
        }

        #endregion



        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteProductAttribute)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var attr = _productAttributeService.SingleOrDefault(id);
            if (attr != null)
            {
                _productAttributeService.Remove(attr);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ShopProductAttribute.ActionNames.Index);
        }


        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditProductAttribute)]
        [BreadCrumb(Title = "ویرایش مشخصه", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {

            var model = _productAttributeService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();

            await LoadCategories(model.CategoryId);

            var editModel = new ProductAttributeModel()
            {
                CategoryId = model.CategoryId,
                Name = model.Name,
                Description = model.Description,
            };

            return View(editModel);

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditProductAttribute)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ProductAttributeModel model, ActionType actionType)
        {
            var attr = _productAttributeService.SingleOrDefault(model.Id);
            if (attr == null) return HttpNotFound();

            await LoadCategories(model.CategoryId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            attr.Name = model.Name;
            attr.Description = model.Description;
            attr.CategoryId = model.CategoryId;

            _productAttributeService.Update(attr);

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
            return RedirectToAction(MVC.Admin.ShopProductAttribute.Index());
        }

        #endregion


        #region Private

        private async Task LoadCategories(int? selectedId = null)
        {
            var list = await _categoryService.All().ToListAsync();
            ViewBag.Categories = new SelectList(list, "Id", "Name", selectedId);
        }



        #endregion


        #region RemoteValidations



        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsCategoryNameExist(string name, int? id)
        {
            return await _categoryService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
        }

        #endregion

    }
}