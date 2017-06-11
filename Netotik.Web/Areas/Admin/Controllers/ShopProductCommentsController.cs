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

using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;

namespace Netotik.Web.Areas.Admin.Controllers
{
       [BreadCrumb(Title = "لیست نظرات محصولات", UseDefaultRouteUrl = true,Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopProductCommentsController : BaseController
    {

        #region ctor
        private readonly IProductCommentService _productCommentService;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _uow;

        public ShopProductCommentsController(
            IProductCommentService productCommentService,
            IProductService productService,
            IUnitOfWork uow)
        {
            _productCommentService = productCommentService;
            _productService = productService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProductComment)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _productCommentService.All()
                .Where(x => x.Status != CommentStatus.Deleted)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreateDate)
                .ToPagedList<ProductComment>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ShopProductComments.Views._Table, pageList);
            else
                return View(MVC.Admin.ShopProductComments.ActionNames.Index, pageList);
        }
        #endregion


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProductComment)]
        public virtual ActionResult Detail(int id)
        {
            var comment = _productCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            ViewBag.Comments = comment.Product.ProductComments.Where(x => x.Status != CommentStatus.Deleted).OrderByDescending(x => x.CreateDate).ToList();

            return PartialView(comment);
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAcceptProductComment)]
        [HttpPost]
        public virtual async Task<ActionResult> Accept(int id)
        {
            var comment = _productCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Status = CommentStatus.Accepted;
            comment.Product.CommentCount++;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.ShopProductComments.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDontAcceptProductComment)]
        [HttpPost]
        public virtual async Task<ActionResult> DontAccept(int id)
        {
            var comment = _productCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Status = CommentStatus.NotAccept;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.ShopProductComments.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteProductComment)]
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id)
        {
            var comment = _productCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Product.CommentCount--;
            comment.Status = CommentStatus.Deleted;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.ShopProductComments.ActionNames.Index);
        }




    }
}