using DNTBreadCrumb;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using Netotik.Common.Filters;
using Netotik.Data;
using Netotik.Resources;
using Netotik.Services.Abstract;
using Netotik.Services.Identity;
using Netotik.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netotik.Web.Areas.Company.Controllers
{
    [Mvc5Authorize(Roles = "Company")]
    [BreadCrumb(Title = "FactorList", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class FactorController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly ISmsPackageService _smsPackageService;
        private readonly IFactorService _factorService;
        private readonly ISmsService _smsService;
        private readonly IUnitOfWork _uow;

        public FactorController(
            IPaymentTypeService paymentTypeService,
        IFactorService factorService,
            ISmsPackageService smsPackageService,
            IApplicationUserManager applicationUserManager,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _paymentTypeService= paymentTypeService;
            _factorService = factorService;
            _smsPackageService = smsPackageService;
            _applicationUserManager = applicationUserManager;
            _smsService = smsService;
            _uow = uow;
        }
        #endregion

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _factorService.GetUserFactorList(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }


        public virtual ActionResult Result(long Id)
        {
            var factor = _factorService.SingleOrDefault(Id);
            if (factor == null) return HttpNotFound();

            var paymentType = _paymentTypeService.All().FirstOrDefault();
            if (paymentType == null)
            {
                this.MessageError(Captions.MissionFail, "درگاه پرداختی در سیسیتم ثبت نشده. با مدیریت تماس بگیرید.");
                return RedirectToAction(MVC.Company.Factor.Index());
            }


            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
                    int Amount = (int)factor.PaymentPrice;
                    long RefID;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    var zp = new ZarinPalService.PaymentGatewayImplementationServicePortTypeClient();

                    int Status = zp.PaymentVerification(paymentType.MerchantId, Request.QueryString["Authority"].ToString(), Amount, out RefID);
                    factor.Result = Status.ToString();

                    if (Status == 100)
                    {
                        factor.FactorStatus = Domain.Entity.FactorStatus.Success;
                        factor.TransactionId = RefID.ToString();
                        factor.PaymentDate = DateTime.Now;
                        if (factor.FactorType==Domain.Entity.FactorType.CompanyBySmsPackage)
                        {
                            factor.User.UserCompany.SmsCharge += factor.FactorSmsDetail.SmsCount;
                        }
                        this.MessageSuccess(Captions.MissionSuccess, "پرداخت شما با موفقیت انجام شد. شماره تراکنش شما : " + RefID);
                    }
                    else
                    {
                        this.MessageError("پرداخت ناموفق", "خطا! وضعیت:" + Request.QueryString["Status"].ToString());
                    }
                    _uow.SaveChanges();
                }
            }
            else
            {
                this.MessageError("پرداخت ناموفق : ", "خطا در پرداخت ، " + Request.QueryString["Authority"].ToString() + " وضعیت: " + Request.QueryString["Status"].ToString());
            }

            return RedirectToAction(MVC.Company.Factor.Index());
        }
    }
}