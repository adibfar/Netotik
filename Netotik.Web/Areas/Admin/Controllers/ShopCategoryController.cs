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

using Netotik.ViewModels.Shop.Category;
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProductCategory)]
    [BreadCrumb(Title = "لیست دسته های محصولی", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopCategoryController : BaseController
    {

        #region ctor
        private readonly IDiscountService _discountService;
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWork _uow;

        public ShopCategoryController(
            IDiscountService discountService,
            ICategoryService categoryService,
            IUnitOfWork uow)
        {
            _discountService = discountService;
            _categoryService = categoryService;
            _uow = uow;
        }
        #endregion


        #region Index
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _categoryService.GetDataTable(Search)
                .ToPagedList<TableCategoryModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopCategory.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopCategory.ActionNames.Index, pageList);

        }
        #endregion


        #region Create

        [BreadCrumb(Title = "دسته جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await LoadCategories();

            PopulateDisCountes();
            return View(new CategoryModel { DisplayOrder = 0, IsPublished = true });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(CategoryModel model, ActionType actionType)
        {
            await LoadCategories(model.ParentId);

            if (model.DiscountIds == null) PopulateDisCountes();
            else PopulateDisCountes(model.DiscountIds);

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(model);
            }


            #region Initial Content
            var now = DateTime.Now;
            var cat = new Category()
            {
                ParentCategoryId = model.ParentId,
                Name = model.Name,
                CreateDate = now,
                EditDate = now,
                DisplayOrder = model.DisplayOrder,
                Description = model.Description,
                IsPublished = model.IsPublished,
                IsDeleted = false,
                ShowOnHomePage = model.ShowOnHomePage,
                MetaDescription = model.MetaDescription,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle
            };

            cat.Discounts = _discountService.GetDiscountesbyIds(model.DiscountIds);

            #endregion

            #region Add Content Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopCategoryPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                cat.Picture = picture;
            }
            #endregion

            _categoryService.Add(cat);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return View();
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.ShopCategory.Index());
        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProductCategory)]
        public virtual ActionResult Detail(int id)
        {
            var cat = _categoryService.SingleOrDefault(id);
            if (cat == null) return HttpNotFound();
            return View(cat);
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var cat = _categoryService.SingleOrDefault(id);
            if (cat != null)
            {
                _categoryService.Remove(cat);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(MVC.Admin.ShopCategory.ActionNames.Index);
        }


        #region Edit

        [BreadCrumb(Title = "ویرایش دسته", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {

            var model = _categoryService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();

            await LoadCategories(model.ParentCategoryId, model.Id);
            PopulateDisCountes(model.Discounts.Select(x => x.Id).ToArray());

            if (model.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopCategoryPath, model.Picture.FileName);

            var editModel = new CategoryModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                DisplayOrder = model.DisplayOrder,
                MetaDescription = model.MetaDescription,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle,
                ParentId = model.ParentCategoryId,
                ShowOnHomePage = model.ShowOnHomePage,
                IsPublished = model.IsPublished
            };
            return View(editModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(CategoryModel model, ActionType actionType)
        {
            var cat = _categoryService.SingleOrDefault(model.Id);
            if (cat == null) return HttpNotFound();


            await LoadCategories(model.Id, model.ParentId);
            if (model.DiscountIds == null) PopulateDisCountes();
            else PopulateDisCountes(model.DiscountIds);

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View();
            }

            #region Update

            cat.Name = model.Name;
            cat.EditDate = DateTime.Now;
            cat.Description = model.Description;
            cat.IsPublished = model.IsPublished;
            cat.DisplayOrder = model.DisplayOrder;
            cat.ShowOnHomePage = model.ShowOnHomePage;
            cat.MetaDescription = model.MetaDescription;
            cat.MetaKeywords = model.MetaKeywords;
            cat.MetaTitle = model.MetaTitle;
            cat.ParentCategoryId = model.ParentId;


            cat.Discounts.Clear();
            cat.Discounts = _discountService.GetDiscountesbyIds(model.DiscountIds);

            #endregion


            #region Add Avatar Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopCategoryPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                cat.Picture = picture;
            }

            #endregion


            _categoryService.Update(cat);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return View();
            }

            if (cat.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopCategoryPath, cat.Picture.FileName);


            this.MessageError(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.ShopCategory.Index());
        }

        #endregion


        #region Private
        private async Task LoadCategories(int? selectedId = null, int? catId = null)
        {
            var list = await _categoryService.All().Where(x => !x.IsDeleted).ToListAsync();
            if (catId.HasValue) list = list.Where(x => x.Id != catId.Value).ToList();
            ViewBag.Categories = new SelectList(list, "Id", "Name", selectedId);
        }

        [NonAction]
        private void PopulateDisCountes(params int[] selectedIds)
        {
            var discountes = _discountService.Where(x => x.DiscountType == DiscountType.CategoryDiscount).ToList().Select(x => new
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
        public virtual async Task<JsonResult> IsCategoryNameExist(string name, int? id)
        {
            return await _categoryService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
        }

        #endregion

    }
}