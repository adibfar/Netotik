using Netotik.Services.Abstract;
using Netotik.Web.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Netotik.Data;
using Netotik.Domain.Entity;
using MvcPaging;
using Netotik.ViewModels;
using System.Data.Entity;
using Netotik.Common.Controller;
using Netotik.Web.Extension;
using Netotik.Web.Infrastructure;
using Netotik.ViewModels.ShopPublic;
using Newtonsoft.Json;
using DNTBreadCrumb;
using Netotik.ViewModels.Shop.ProductPanel;
using Netotik.ViewModels.CMS.Comment;


namespace Netotik.Web.Controllers
{
    [RoutePrefix("Product")]
    [BreadCrumb(Title = "فروشگاه", UseDefaultRouteUrl = false, RemoveAllDefaultRouteValues = false,
    Order = 0, GlyphIcon = "fa fa-shopping-cart")]
    public partial class ProductController : BaseController
    {
        private readonly IProductService _productSrevice;
        private readonly IProductCommentService _productCommentSrevice;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IUnitOfWork _uow;

        public ProductController(
            IProductCommentService productCommentService,
            IManufacturerService manufacturerService,
            ICategoryService categoryService,
            IProductService productService,
            IUnitOfWork uow)
        {
            _productCommentSrevice = productCommentService;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _productSrevice = productService;
            _uow = uow;
        }

        [Route("category/{categoryId=0}/brand/{brandId=0}/page/{page=1}/count/{count=16}/{term?}")]
        public virtual ActionResult Index(int? categoryId = 0, int? brandId = 0, int page = 1, int count = 16, string term = "")
        {
            categoryId = categoryId == 0 ? null : categoryId;
            brandId = brandId == 0 ? null : brandId;
            int total;
            var list = _productSrevice.GetForPublicView(out total, categoryId, brandId, page, count, term);

            var model = new ProductGridModel
            {
                CategoryId = categoryId,
                ManufacturerId = brandId,
                Count = count,
                Page = page,
                Total = total,
                Products = list
            };
            return View(model);
        }

        [Route("{id}/{name}")]
        [BreadCrumb(Title = "محصول", Order = 1)]
        public virtual async Task<ActionResult> Show(int id = 0)
        {
            var product = _productSrevice.All()
                .Include(x => x.ProductAttributeValues)
                .Include(x => x.Manufacturer.Discounts)
                .Include(x => x.Discounts)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted && x.IsPublished);
            if (product == null) return RedirectToAction(MVC.Product.ActionNames.Index);

            #region Add View
            if (Request.Cookies["ViewedPost"] != null)
            {
                if (Request.Cookies["ViewedPost"][string.Format("prodId_{0}", id)] == null)
                {
                    HttpCookie cookie = (HttpCookie)Request.Cookies["ViewedPost"];
                    cookie[string.Format("prodId_{0}", id)] = id.ToString();
                    cookie.Expires = DateTime.Now.AddHours(4);
                    Response.Cookies.Add(cookie);
                    product.CountView++;
                    await _uow.SaveChangesAsync();
                }
            }
            else
            {
                HttpCookie cookie = new HttpCookie("ViewedPost");
                cookie[string.Format("prodId_{0}", id)] = id.ToString();
                cookie.Expires = DateTime.Now.AddHours(4);
                Response.Cookies.Add(cookie);
                product.CountView++;
                await _uow.SaveChangesAsync();
            }
            #endregion

            ViewBag.RelativeProducts = _productSrevice.GetRelativeProduct(8, product.Categories.Select(x => x.Id).ToArray(), product.Id);

            if (product.AllowViewComments)
                ViewBag.Comments = product.ProductComments.Where(x => x.Status == CommentStatus.Accepted).ToList();

            ViewBag.OffPrice = _productSrevice.CalculateOff(product);

            this.SetCurrentBreadCrumbTitle(string.Format("{0}", product.Name));
            return View(product);
        }

        public virtual ActionResult AddToCart(int id, int? colorId, int? sizeId)
        {
            var prod = _productSrevice
                .All()
                .Include(x => x.Categories)
                .Include(x => x.Manufacturer.Discounts)
                .Include(x => x.Discounts)
                .FirstOrDefault(x => !x.IsDeleted && x.Price.HasValue && x.IsPublished && x.Id == id);


            List<ShoppingCartModel> cart;

            if (string.IsNullOrEmpty(HttpContext.GetCookieValue("ShoppingCart"))) cart = new List<ShoppingCartModel>();
            else cart = JsonConvert.DeserializeObject<List<ShoppingCartModel>>(HttpContext.GetCookieValue("ShoppingCart"));


            var cartItem = cart.FirstOrDefault(x => x.Id == prod.Id);
            if (cartItem != null) cartItem.Count++;
            else cartItem = new ShoppingCartModel
            {
                Id = prod.Id,
                Count = 1,
                Name = prod.Name,
                Image = prod.PictureId.HasValue ? prod.Picture.FileName : string.Empty,
                UnitPrice = prod.Price.Value,
                OffPrice = _productSrevice.CalculateOff(prod),
            };



            if (prod.OrderMaximumQuantity < cartItem.Count)
            {
                this.MessageWarning("توجه!!!",string.Format("حداکثر تعداد خرید برای این محصول {0} عدد می باشد", prod.OrderMaximumQuantity));
                return RedirectToAction(MVC.Product.ActionNames.Show, new { id = id, name = prod.Name.GenerateSlug() });
            }

            cartItem.TotalPtice = (cartItem.UnitPrice - cartItem.OffPrice) * cartItem.Count;

            if (prod.OrderMaximumQuantity < cartItem.Count)
            {
                this.MessageWarning("توجه!!!", string.Format("حداکثر تعداد خرید برای این محصول {0} عدد می باشد", prod.OrderMaximumQuantity));
                return RedirectToAction(MVC.Product.ActionNames.Show, new { id = id, name = prod.Name.GenerateSlug() });
            }

            if (!cart.Any(x => x.Id == id)) cart.Add(cartItem);

            if (string.IsNullOrEmpty(HttpContext.GetCookieValue("ShoppingCart")))
                HttpContext.AddCookie("ShoppingCart", JsonConvert.SerializeObject(cart), DateTime.Now.AddDays(3));
            else HttpContext.UpdateCookie("ShoppingCart", JsonConvert.SerializeObject(cart));


            return RedirectToAction(MVC.Factor.ActionNames.Index, MVC.Factor.Name);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AddComment(AddCommentProduct model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productSrevice.SingleOrDefaultAsync(model.ProductId);
                if (product == null && !product.AllowUserComment) return RedirectToAction(MVC.Product.ActionNames.Index, MVC.Product.Name);

                ProductComment entity = new ProductComment
                {
                    Status = CommentStatus.WaitForAccept,
                    CreateDate = DateTime.Now,
                    CommentText = model.Text,
                    Name = model.Name,
                    Email = model.Email,
                    ProductId = model.ProductId,
                };

                if (model.CommentId.HasValue)
                {
                    var comment = await _productCommentSrevice.SingleOrDefaultAsync(model.CommentId.Value);
                    if (comment != null) entity.Comments.Add(comment);
                }
                _productCommentSrevice.Add(entity);
                await _uow.SaveChangesAsync();
                
                this.MessageSuccess("ثبت شد!", "نظر شما با موفقیت ثبت شد ، پس از بررسی و تایید در سایت نمایش داده خواهد شد..");

                ModelState.Clear();

                return RedirectToAction(MVC.Product.ActionNames.Show, new { id = product.Id, name = product.Name });
            }

            return RedirectToAction(MVC.Product.ActionNames.Index);
        }





        public virtual ActionResult Filters()
        {
            var filterModel = new FilterProductModel();
            filterModel.Brands = _manufacturerService.GetDataTable("").ToList();
            filterModel.Categories = _categoryService.GetDataTable("").ToList();

            return PartialView(MVC.Product.Views._Filters, filterModel);
        }



    }
}