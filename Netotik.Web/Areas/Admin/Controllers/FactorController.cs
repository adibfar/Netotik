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
using System.Web.UI;
using System.Threading.Tasks;
using Netotik.Web;
using System.Data.Entity.Validation;

using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Common.City;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessCity)]
    [BreadCrumb(Title = "Factores", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon-th-large")]
    public partial class FactorController : BaseController
    {

        #region ctor

        private readonly IUnitOfWork _uow;
        private readonly IFactorService _factorService;

        public FactorController(
            IUnitOfWork uow,
            IFactorService factorService
            )
        {
            _uow = uow;
            _factorService = factorService;
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

            var result = _factorService.GetList(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public virtual JsonResult GetPriceFactorChartData()
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-19);
            var factores = _factorService.All().Where(x => x.RegisterDate > date).ToList();
            var monthSuccessFactores = new long[20];
            var monthFailFactores = new long[20];
            var monthUnpayFactores = new long[20];
            var dates = new string[20];

            for (var i = 0; i < 20; i++)
            {
                dates[i] = PersianDate.ConvertDate.ToFa(date, "d");
                monthSuccessFactores[i] = factores.Where(x => x.FactorStatus == FactorStatus.Success && x.RegisterDate.Date == date.Date).Sum(x => x.PaymentPrice);
                monthFailFactores[i] = factores.Where(x => x.FactorStatus == FactorStatus.Fail && x.RegisterDate.Date == date.Date).Sum(x => x.PaymentPrice);
                monthUnpayFactores[i] = factores.Where(x => x.FactorStatus == FactorStatus.Unpaid && x.RegisterDate.Date == date.Date).Sum(x => x.PaymentPrice);
                date = date.AddDays(1);

            }


            return Json(new
            {
                dates = dates,
                success = monthSuccessFactores,
                unpaid = monthUnpayFactores,
                fail = monthFailFactores,
            }, JsonRequestBehavior.AllowGet);

        }

        public virtual JsonResult GetCountFactorChartData()
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-19);
            var factores = _factorService.All().Where(x => x.RegisterDate > date).ToList();
            var monthSuccessFactores = new long[20];
            var monthFailFactores = new long[20];
            var monthUnpayFactores = new long[20];
            var dates = new string[20];

            for (var i = 0; i < 20; i++)
            {
                dates[i] = PersianDate.ConvertDate.ToFa(date, "d");
                monthSuccessFactores[i] = factores.Where(x => x.FactorStatus == FactorStatus.Success && x.RegisterDate.Date == date.Date).Count();
                monthFailFactores[i] = factores.Where(x => x.FactorStatus == FactorStatus.Fail && x.RegisterDate.Date == date.Date).Count();
                monthUnpayFactores[i] = factores.Where(x => x.FactorStatus == FactorStatus.Unpaid && x.RegisterDate.Date == date.Date).Count();
                date = date.AddDays(1);

            }


            return Json(new
            {
                dates = dates,
                success = monthSuccessFactores,
                unpaid = monthUnpayFactores,
                fail = monthFailFactores,
            }, JsonRequestBehavior.AllowGet);
        }
    }

}