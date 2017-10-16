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
        private readonly IUserMailer _userMailer;
        private readonly ISmsService _smsService;
        private readonly IUnitOfWork _uow;

        public FactorController(
            IUserMailer userMailer,
            IPaymentTypeService paymentTypeService,
            IFactorService factorService,
            ISmsPackageService smsPackageService,
            IApplicationUserManager applicationUserManager,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _userMailer = userMailer;
            _paymentTypeService = paymentTypeService;
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
                this.MessageError(Captions.MissionFail, Captions.PaymentGatewayNotFound);
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
                        if (factor.FactorType == Domain.Entity.FactorType.CompanyBySmsPackage)
                        {
                            factor.User.UserCompany.SmsCharge += factor.FactorSmsDetail.SmsCount;
                        }
                        this.MessageSuccess(Captions.MissionSuccess, string.Format(Captions.PaymentSuccess, RefID));
                        _userMailer.Factor(new ViewModels.Identity.Account.EmailFactorViewModel
                        {
                            CompanyName = factor.User.FirstName,
                            FactorDate = factor.PaymentDate.Value,
                            FactorId = factor.Id,
                            Price = factor.PaymentPrice,
                            ServiceName = factor.FactorType == Domain.Entity.FactorType.CompanyBySmsPackage ? factor.FactorSmsDetail.PackageName : "",
                            Subject = string.Format("{0} - {1} : {2}", Captions.Netotik, Captions.TransactionNumber, factor.TransactionId),
                            To = factor.User.Email,
                            ViewName = MVC.UserMailer.Views.Factor
                        }).Send();
                        _userMailer.Factor(new ViewModels.Identity.Account.EmailFactorViewModel
                        {
                            CompanyName = factor.User.FirstName,
                            FactorDate = factor.PaymentDate.Value,
                            FactorId = factor.Id,
                            Price = factor.PaymentPrice,
                            ServiceName = factor.FactorType == Domain.Entity.FactorType.CompanyBySmsPackage ? factor.FactorSmsDetail.PackageName : "",
                            Subject = string.Format("{0} - {1} : {2}", Captions.Netotik, "فاکتور", factor.Id),
                            To = "ehsan2912.em@gmail.com",
                            ViewName = MVC.UserMailer.Views.Factor
                        }).Send();

                        _smsService.SendSms(factor.User.PhoneNumber, string.Format(Captions.BuyPackageFactorSmsToUser, Captions.Netotik, factor.FactorSmsDetail.PackageName));
                    }
                    else
                    {
                        this.MessageError(Captions.UnsuccessfulPaid, string.Format(Captions.PaymentUnsuccesfulMesssage, Request.QueryString["Status"].ToString()));
                    }
                    _uow.SaveChanges();
                }
            }
            else
            {
                this.MessageError(Captions.UnsuccessfulPaid, string.Format(Captions.PaymentErrorMessage, Request.QueryString["Authority"].ToString(), Request.QueryString["Status"].ToString()));
            }

            return RedirectToAction(MVC.Company.Factor.Index());
        }
    }
}