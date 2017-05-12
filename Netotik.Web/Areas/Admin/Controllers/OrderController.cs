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
using Netotik.ViewModels.Shop.Order;

namespace Netotik.Web.Areas.Admin.Controllers
{
       [BreadCrumb(Title = "لیست سفارشات موفق", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
    Order = 0, GlyphIcon = "icon icon-table")]
    public partial class OrderController : BaseController
    {

        #region ctor
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IUnitOfWork _uow;

        public OrderController(
            IOrderService orderService,
            ICategoryService categroyService,
            IDiscountService discountService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IUnitOfWork uow)
        {
            _orderService = orderService;
            _categoryService = categroyService;
            _manufacturerService = manufacturerService;
            _discountService = discountService;
            _productService = productService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProductComment)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _orderService.GetOrderDataTable(Search)
                .ToPagedList<TableOrderModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.Order.Views._Table, pageList);
            else
                return View(MVC.Admin.Order.ActionNames.Index, pageList);
        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessProductComment)]
        public virtual ActionResult Detail(int id)
        {
            var order = _orderService
                .All()
                .Include(x => x.Address)
                .Include(x => x.OrderItems)
                .Include(x => x.OrderPayments)
                .FirstOrDefault(x => x.Id == id && x.PaymentStatus == PaymentStatus.Success);

            if (order == null) return HttpNotFound();
            return View(order);
        }

        #endregion


        #region Accept

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteProduct)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AcceptOrder(int id = 0)
        {
            var order = _orderService.All().FirstOrDefault(x => x.OrderStatus == OrderStatus.WaitForProcess && x.PaymentStatus == PaymentStatus.Success && x.Id == id);
            if (order == null) return HttpNotFound();

            order.OrderStatus = OrderStatus.WaitForSend;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.Order.ActionNames.Detail, new { id = id });
        }

        #endregion


    }


}