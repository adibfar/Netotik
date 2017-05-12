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

using Netotik.ViewModels.Shop.ProductAdmin;
using System.IO;
using Netotik.ViewModels;
using System.Net;
using Netotik.ViewModels.Shop.ProductAttributeValue;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.CMS.ContentCategory;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست محصولات", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]

    public partial class ShopProductController : BaseController
    {

        #region ctor

        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeValueService _productAttributeValueService;
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _uow;

        public ShopProductController(
            IProductAttributeValueService productAttributeValueService,
            IProductAttributeService productAttributeService,
            ICategoryService categroyService,
            IDiscountService discountService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IPictureService pictureService,
            IUnitOfWork uow)
        {
            _productAttributeValueService = productAttributeValueService;
            _productAttributeService = productAttributeService;
            _categoryService = categroyService;
            _manufacturerService = manufacturerService;
            _discountService = discountService;
            _productService = productService;
            _pictureService = pictureService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProduct)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _productService.GetDataTable(Search)
                .ToPagedList<TableProductModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopProduct.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopProduct.ActionNames.Index, pageList);
        }
        #endregion


        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateProduct)]
        [BreadCrumb(Title = "محصول جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await PopulateCategories();
            await LoadManufacturer();
            PopulateDisCountes();

            return View(new ProductModel { IsPublished = true, Price = 0, Quantity = 0, MinQuantityNotify = 0, Weight = 0, OrderMaximumQuantity = 10 });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateProduct)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ProductModel model, ActionType actionType)
        {
            await LoadManufacturer(model.ManufacturerId);

            if (model.CategoryIds == null) await PopulateCategories();
            else await PopulateCategories(model.CategoryIds);

            if (model.DiscountIds == null) PopulateDisCountes();
            else PopulateDisCountes(model.DiscountIds);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }

            #region Initial Content
            var now = DateTime.Now;
            var prod = new Product()
            {
                Name = model.Name,
                ManufacturerId = model.ManufacturerId,
                TaxId = model.TaxId,
                DeliveryDateId = model.DeliveryDateId,
                IsFreeShipping = model.IsFreeShipping,
                Quantity = model.Quantity,
                MinQuantityNotify = model.MinQuantityNotify,
                IsMinQuantityNotify = model.IsMinQuantityNotify,
                DisplayQuantityForUser = model.DisplayQuantityForUser,
                OrderMaximumQuantity = model.OrderMaximumQuantity,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                ShowOnHomePage = model.ShowOnHomePage,
                Price = model.Price,
                MaxOffPrice = model.MaxOffPrice,
                Weight = model.Weight,
                DisableBuyButton = model.DisableBuyButton,
                CanBuyIfNotInStock = model.CanBuyIfNotInStock,
                CallForPrice = model.CallForPrice,
                IsPublished = model.IsPublished,
                AvailableStartDate = model.AvailableStartDate ?? DateTime.Now,
                AvailableEndDate = model.AvailableEndDate,
                AdminComment = model.AdminComment,
                AllowUserComment = model.AllowComment,
                AllowViewComments = model.AllowViewComments,
                MetaDescription = model.MetaDescription,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle,

                IsDeleted = false,
                CommentCount = 0,
                CountView = 0,
                CreateDate = now,
                EditDate = now,
            };

            if (model.DiscountIds != null) prod.Discounts = _discountService.GetDiscountesbyIds(model.DiscountIds);
            if (model.CategoryIds != null) prod.Categories = _categoryService.GetbyIds(model.CategoryIds);

            #endregion

            #region Add Content Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopProductPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                prod.Picture = picture;
            }
            #endregion

            _productService.Add(prod);

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
            return RedirectToAction(MVC.Admin.ShopProduct.Index());

        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProduct)]
        public virtual ActionResult Detail(int id)
        {
            var prod = _productService.SingleOrDefault(id);
            if (prod == null) return HttpNotFound();
            return View(prod);
        }

        #endregion


        #region Remove

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteProduct)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var prod = _productService.SingleOrDefault(id);
            if (prod == null) return HttpNotFound();

            prod.IsDeleted = true;
            await _uow.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.ShopProduct.ActionNames.Index);
        }

        #endregion


        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditProduct)]
        [BreadCrumb(Title = "ویرایش محصول", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {

            var model = _productService.SingleOrDefault(id);
            if (model == null) return HttpNotFound();

            await PopulateCategories(model.Categories.Select(x => x.Id).ToArray());
            await LoadManufacturer(model.ManufacturerId);

            PopulateDisCountes(model.Discounts.Select(x => x.Id).ToArray());

            if (model.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopProductPath, model.Picture.FileName);

            var editModel = new ProductModel
            {
                Id = model.Id,
                Name = model.Name,
                ManufacturerId = model.ManufacturerId,
                TaxId = model.TaxId,
                DeliveryDateId = model.DeliveryDateId,
                IsFreeShipping = model.IsFreeShipping,
                Quantity = model.Quantity,
                MinQuantityNotify = model.MinQuantityNotify,
                IsMinQuantityNotify = model.IsMinQuantityNotify,
                DisplayQuantityForUser = model.DisplayQuantityForUser,
                OrderMaximumQuantity = model.OrderMaximumQuantity,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                ShowOnHomePage = model.ShowOnHomePage,
                Price = model.Price,
                MaxOffPrice = model.MaxOffPrice,
                Weight = model.Weight,
                DisableBuyButton = model.DisableBuyButton,
                CallForPrice = model.CallForPrice,
                CanBuyIfNotInStock = model.CanBuyIfNotInStock,
                IsPublished = model.IsPublished,
                AvailableStartDate = model.AvailableStartDate,
                AvailableEndDate = model.AvailableEndDate,
                AdminComment = model.AdminComment,
                AllowComment = model.AllowUserComment,
                AllowViewComments = model.AllowViewComments,
                MetaDescription = model.MetaDescription,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle
            };

            return View(editModel);

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditProduct)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ProductModel model, ActionType actionType)
        {
            var prod = _productService.SingleOrDefault(model.Id);
            if (prod == null) return HttpNotFound();


            await LoadManufacturer(model.ManufacturerId);

            if (model.DiscountIds == null) PopulateDisCountes();
            else PopulateDisCountes(model.DiscountIds);

            if (model.CategoryIds == null) await PopulateCategories();
            else await PopulateCategories(model.CategoryIds);


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            #region Update

            prod.Name = model.Name;
            prod.ManufacturerId = model.ManufacturerId;
            prod.TaxId = model.TaxId;
            prod.DeliveryDateId = model.DeliveryDateId;
            prod.IsFreeShipping = model.IsFreeShipping;
            prod.Quantity = model.Quantity;
            prod.MinQuantityNotify = model.MinQuantityNotify;
            prod.IsMinQuantityNotify = model.IsMinQuantityNotify;
            prod.DisplayQuantityForUser = model.DisplayQuantityForUser;
            prod.OrderMaximumQuantity = model.OrderMaximumQuantity;
            prod.ShortDescription = model.ShortDescription;
            prod.FullDescription = model.FullDescription;
            prod.ShowOnHomePage = model.ShowOnHomePage;
            prod.Price = model.Price;
            prod.MaxOffPrice = model.MaxOffPrice;
            prod.Weight = model.Weight;
            prod.DisableBuyButton = model.DisableBuyButton;
            prod.CallForPrice = model.CallForPrice;
            prod.CanBuyIfNotInStock = model.CanBuyIfNotInStock;
            prod.IsPublished = model.IsPublished;
            prod.AvailableEndDate = model.AvailableEndDate;
            prod.AdminComment = model.AdminComment;
            prod.AllowUserComment = model.AllowComment;
            prod.AllowViewComments = model.AllowViewComments;
            prod.MetaDescription = model.MetaDescription;
            prod.MetaKeywords = model.MetaKeywords;
            prod.MetaTitle = model.MetaTitle;
            prod.EditDate = DateTime.Now;

            if (model.AvailableStartDate.HasValue) prod.AvailableStartDate = model.AvailableStartDate;
            else model.AvailableStartDate = prod.AvailableStartDate.Value;

            prod.Discounts.Clear();
            prod.Discounts = _discountService.GetDiscountesbyIds(model.DiscountIds);

            prod.Categories.Clear();
            prod.Categories = _categoryService.GetbyIds(model.CategoryIds);

            #endregion



            #region Add Avatar Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesShopProductPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                prod.Picture = picture;
            }

            #endregion


            if (prod.PictureId.HasValue)
                ViewBag.Avatar = Path.Combine(FilePathes._imagesShopProductPath, prod.Picture.FileName);

            _productService.Update(prod);

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
            return RedirectToAction(MVC.Admin.ShopProduct.Index());
        }

        #endregion


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanManageAttributeProduct)]
        public virtual ActionResult ProductAttribute(int id)
        {
            var fields = _productAttributeService.GetAttributesByProductId(id);
            return View(fields);
        }

        [HttpPost]
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanManageAttributeProduct)]
        public virtual ActionResult ProductAttribute(IEnumerable<ProductAttributeValueModel> values)
        {
            if (!ModelState.IsValid) return View(values);
            try
            {
                _productAttributeValueService.AddOrUpdateProductAttributes(values);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View(values);
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return View(values);
        }


        #region Private

        [NonAction]
        private void PopulateDisCountes(params int[] selectedIds)
        {
            var discountes = _discountService.Where(x => x.DiscountType == DiscountType.ProductDiscount).ToList().Select(x => new
            SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            }).ToList();
            ViewBag.Discountes = discountes;
        }

        [NonAction]
        private async Task PopulateCategories(params int[] selectedIds)
        {
            //var list = (await _categoryService.All().Where(x => !x.IsDeleted && x.ParentCategoryId == null).ToListAsync()).Select(x => new
            //ContentCategoryTreeJsModel()
            //{
            //    Name = x.Name,
            //    Id = x.Id,
            //    Childs = x.SubCategories.Select(z => new ContentCategoryTreeJsModel()
            //    {
            //        Id = z.Id,
            //        Name = z.Name,
            //        Selected = (selectedIds != null) ? selectedIds.Any(y => y == z.Id) : false,
            //        Childs = z.SubCategories.Select(d => new ContentCategoryTreeJsModel()
            //        {
            //            Id = d.Id,
            //            Name = d.Name,
            //            Selected = (selectedIds != null) ? selectedIds.Any(y => y == d.Id) : false,
            //            Childs = d.SubCategories.Select(s => new ContentCategoryTreeJsModel()
            //            {
            //                Id = s.Id,
            //                Name = s.Name,
            //                Selected = (selectedIds != null) ? selectedIds.Any(y => y == s.Id) : false
            //            }).ToList()
            //        }).ToList()
            //    }).ToList(),
            //    Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            //}).ToList(); ;


            //ViewBag.Categories = list;
        }
        private async Task LoadManufacturer(int? selectedId = null)
        {
            var list = await _manufacturerService.All().ToListAsync();
            ViewBag.Manufactureres = new SelectList(list, "Id", "Name", selectedId);
        }
        
        #endregion


        #region Images

        [HttpGet]
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanManagePictureProduct)]
        public virtual ActionResult Pictures(int id = 0)
        {
            var gallery = _productService.SingleOrDefault(id);
            if (gallery == null) return HttpNotFound();
            ViewBag.ProductId = id;
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanManagePictureProduct)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult List(int productId = 0)
        {
            var prod = _productService.All().Include(x => x.Pictures).FirstOrDefault(x => x.Id == productId);
            ViewBag.ProductId = productId;
            return PartialView(MVC.Admin.ShopProduct.Views._ListPartial, prod.Pictures.ToList());


        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanManagePictureProduct)]
        public virtual ActionResult AddPictureToFolder(int? productId)
        {
            if (productId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (_productService.Exists(productId.Value) == false) return HttpNotFound();
            ViewBag.productId = productId.Value;
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanManagePictureProduct)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AddImage(IEnumerable<HttpPostedFileBase> files, int productId)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/jpg",
                "image/png"
            };
            var httpPostedFileBases = files as HttpPostedFileBase[] ?? files.ToArray();
            if (!httpPostedFileBases.Any())
            {
                ModelState.AddModelError("", "مشکلی در انجام عملیات پیش آمده است");
                return View();
            }

            var prod = await _productService.SingleOrDefaultAsync(productId);

            if (prod == null) return HttpNotFound();

            foreach (var file in httpPostedFileBases)
            {
                if (file != null && !validImageTypes.Contains(file.ContentType))
                {
                    ModelState.AddModelError("", " پسوند تصویر انتخاب شده غیر مجاز است");
                    return View();
                }
                if (file == null || file.ContentLength <= 0) continue;
                string uploadDir = FilePathes._imagesShopProductPath;


                var fileName = SaveFile(file, uploadDir);

                var item = new Picture { OrginalName = file.FileName, FileName = fileName, MimeType = file.ContentType };
                prod.Pictures.Add(item);
            }
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.ShopProduct.ActionNames.Pictures, MVC.Admin.ShopProduct.Name,
                new { Id = productId });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanManagePictureProduct)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DeletePicture(int? id)
        {

            if (id == null) return Content(null);
            var picItem = _pictureService.SingleOrDefault(id.Value);
            if (picItem == null) return HttpNotFound();


            var pic = _pictureService.SingleOrDefault(picItem.Id);
            await _pictureService.RemoveAsync(id.Value);

            _uow.SaveChanges();

            DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesShopProductPath, pic.FileName)));

            return Content("ok");
        }


        #endregion

    }


}