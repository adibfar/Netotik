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
using Netotik.ViewModels.SmsPackage;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using Netotik.Services.Identity;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "SmsManagement", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon-th-large")]
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessSlider)]
    public partial class SmsController : BaseController
    {


        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly ISmsPackageService _smsPackageService;
        private readonly ISmsService _smsService;
        private readonly ISmsLogService _smsLogService;
        private readonly IUnitOfWork _uow;

        public SmsController(
            IApplicationUserManager applicationUserManager,
            ISmsService smsService,
            ISmsLogService smsLogService,
            ISmsPackageService smsPackageService,
            IUnitOfWork uow)
        {
            _applicationUserManager = applicationUserManager;
            _smsService = smsService;
            _smsLogService = smsLogService;
            _smsPackageService = smsPackageService;
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
            var result = _smsPackageService.GetAll();

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = result.Count,
                iTotalDisplayRecords = result.Count,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }


        public virtual JsonResult GetSmsChartData()
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-9);
            var smses = _smsLogService.All().Where(x => x.CreateDate > date).ToList();
            var systemSms = new long[10];
            var userSms = new long[10];
            var dates = new string[10];

            for (var i = 0; i < 10; i++)
            {
                dates[i] = PersianDate.ConvertDate.ToFa(date, "d");
                userSms[i] = smses.Where(x => x.UserId.HasValue && x.CreateDate.Date == date.Date).Count();
                systemSms[i] = smses.Where(x => !x.UserId.HasValue && x.CreateDate.Date == date.Date).Count();
                date = date.AddDays(1);

            }

            return Json(new
            {
                dates = dates,
                userSms = userSms,
                systemSms = systemSms,
            }, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetSmsCredit()
        {
            var credit = _smsService.GetCredit();
            return Json(new
            {
                charge = credit
            },JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetSmsCompaniesCredit()
        {
            var credit = _applicationUserManager.GetCompaniesChargre();
            return Json(new
            {
                charge = credit
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create
        public virtual ActionResult Create()
        {
            return PartialView(MVC.Admin.Sms.Views._CreatePackage, new SmsPackageModel { Order = 0, IsActive = true });
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(SmsPackageModel model, ActionType actionType = ActionType.Save)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Sms.Index());
            }

            #region Initial Package
            var slider = new Netotik.Domain.Entity.SmsPackage()
            {
                Name = model.Name,
                SmsCount = model.SmsCount,
                Price = model.Price,
                UnitPrice = model.UnitPrice,
                Order = model.Order,
                IsActive = model.IsActive
            };

            #endregion

            _smsPackageService.Add(slider);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.Sms.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.Sms.Index());
        }
        #endregion

        #region Edit
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var package = _smsPackageService.All().Where(x => x.Id == id).FirstOrDefault();
            if (package != null)
            {
                _smsPackageService.Remove(package);
                await _uow.SaveChangesAsync();

            }
            return RedirectToAction(MVC.Admin.Sms.Index());
        }

        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = await _smsPackageService.All().FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return RedirectToAction(MVC.Admin.Sms.ActionNames.Index);

            var editModel = new SmsPackageModel
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                UnitPrice = model.UnitPrice,
                SmsCount = model.SmsCount,
                Order = model.Order,
                IsActive = model.IsActive
            };

            return View(MVC.Admin.Sms.Views._EditPackage, editModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(SmsPackageModel model, ActionType actionType)
        {
            var slider = _smsPackageService.SingleOrDefault(model.Id);
            if (slider == null)
                return HttpNotFound();


            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Sms.Index());
            }

            #region Update

            slider.IsActive = model.IsActive;
            slider.Order = model.Order;
            slider.Name = model.Name;
            slider.SmsCount = model.SmsCount;
            slider.Price = model.Price;
            slider.UnitPrice = model.UnitPrice;
            #endregion

            _smsPackageService.Update(slider);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return RedirectToAction(MVC.Admin.Sms.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Sms.Index());

        }
        #endregion

    }
}