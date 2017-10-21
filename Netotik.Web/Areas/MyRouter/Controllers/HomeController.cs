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
using Netotik.Common.MikrotikAPI;
using Netotik.ViewModels.Identity.UserRouter;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Microsoft.AspNet.Identity;
using Netotik.ViewModels.Identity.Security;
using WebGrease.Css.Extensions;
using Telegram.Bot;
using Netotik.ViewModels.Mikrotik;

namespace Netotik.Web.Areas.MyRouter.Controllers
{
     [Mvc5Authorize(Roles = "Router")]
    [BreadCrumb(Title = "User", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class HomeController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly ISmsPackageService _smsPackageService;
        private readonly IFactorService _factorService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IPictureService _pictureService;
        private readonly ISmsService _smsService;
        private readonly IUnitOfWork _uow;

        public HomeController(
            IPaymentTypeService paymentTypeService,
            IFactorService factorService,
            ISmsPackageService smsPackageService,
            IMikrotikServices mikrotikServices,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _paymentTypeService = paymentTypeService;
            _factorService = factorService;
            _smsPackageService = smsPackageService;
            _mikrotikServices = mikrotikServices;
            _pictureService = pictureservice;
            _applicationUserManager = applicationUserManager;
            _smsService = smsService;
            _uow = uow;
        }
        #endregion

        #region Index

        public virtual ActionResult Index()
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                    {
                        this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                    }
                }
                else
                    this.MessageError(Captions.Error, Captions.UserPasswordClientError);
            }
            else
                this.MessageError(Captions.Error, Captions.IPPORTClientError);

           
            return View();
        }

        public virtual ActionResult MyProfile()
        {
            return View();
        }

        public virtual ActionResult ProfileData()
        {
            var Router = _applicationUserManager.GetUserRouterProfile(UserLogined.Id);
            PopulatePermissions(_applicationUserManager.FindClientPermissions(Router.Id).ToArray());

            return PartialView(MVC.MyRouter.Home.Views._ProfileData, Router);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> changeImageProfile(HttpPostedFileBase image)
        {
            var user = await _applicationUserManager.FindByIdAsync(User.Identity.GetUserId<long>());

            if (image == null)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(MVC.MyRouter.Home.Views.MyProfile);
            }


            if (user.PictureId.HasValue)
                DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesUserAvatarsPath, user.Picture.FileName)));

            var fileName = SaveFile(image, Common.Controller.FilePathes._imagesUserAvatarsPath);
            var picture = new Picture
            {
                FileName = fileName,
                OrginalName = image.FileName,
                MimeType = image.ContentType
            };
            user.Picture = picture;
            await _uow.SaveAllChangesAsync();

            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.MyRouter.Home.MyProfile());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> UpdateProfile(ProfileModel model)
        {

            PopulatePermissions(model.ClientPermissionNames);
            #region Validation
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MyProfile);
            }
            #endregion
            if (model.Email != UserLogined.Email)
                model.EmailConfirmed = false;
            model.Id = UserLogined.Id;
            model.UserResellerId = UserLogined.UserRouter.UserResellerId;

            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserRouterProfile(model);
            return RedirectToAction(MVC.MyRouter.Home.ActionNames.MyProfile);
        }

        public virtual ActionResult MikrotikConf()
        {
            return View(_applicationUserManager.GetUserRouterMikrotikConf(UserLogined.Id));
        }
        public virtual ActionResult TelegramBot()
        {
            return View(_applicationUserManager.GetUserRouterTelegramBot(UserLogined.Id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> TelegramBot(ViewModels.Identity.UserRouter.TelegramBotModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.TelegramBot, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            #endregion
            if (model.TelegramBotToken != "" || model.TelegramBotToken != null)
            {
                TelegramBotClient Api = new TelegramBotClient(model.TelegramBotToken);
                //بروز کردن وب هوک ربات مربوطه
                string APIUrl = string.Format("https://netotik.com:443/api/telegrambot/Router/{0}", UserLogined.UserRouter.RouterCode);
                Api.SetWebhookAsync(APIUrl).Wait();
            }
            model.Id = UserLogined.Id;

            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserRouterTelegramBot(model);
            return RedirectToAction(MVC.MyRouter.Home.ActionNames.TelegramBot, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> MikrotikConf(MikrotikConfModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            #endregion
            if (model.R_Password == "" || model.R_Password == null)
            {
                model.R_Password = UserLogined.UserRouter.R_Password;
                this.MessageInformation(Captions.Attention, Captions.RouterPasswordEmptyInformation);
            }
            model.Id = UserLogined.Id;
            if (model.cloud == true)
            {
                if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                    if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                        model.R_Host = _mikrotikServices.EnableAndGetCloud(model.R_Host, model.R_Port, model.R_User, model.R_Password);
            }
            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserRouterMikrotikConf(model);
            return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
        }
        #endregion

        #region Edit

        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.ChangePassword);
            }
            var temp = await _applicationUserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.Password);
            if (temp.Succeeded)
            {
                if (UserLogined.UserRouter.SmsCharge > 0 && UserLogined.UserRouter.SmsActive && UserLogined.UserRouter.SmsAdminChangeAdminPassword)
                    _smsService.SendSms(UserLogined.PhoneNumber, string.Format(Captions.SmsRouterPasswordChange, UserLogined.UserName), UserLogined.Id);
                this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            }
            else
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
            _uow.SaveAllChanges();
            return View();
        }

        #endregion

        public virtual ActionResult Sms()
        {
            var Model = new SmsModel();
            Model = _applicationUserManager.GetUserRouterSmsSettings(UserLogined.Id);
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                }
                else
                    this.MessageError(Captions.Error, Captions.UserPasswordClientError);
            }
            else
                this.MessageError(Captions.Error, Captions.IPPORTClientError);


            ViewBag.Packages = _smsPackageService.All()
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.Order).ToList();

            return View(Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Sms(SmsModel model)
        {
            model.Id = UserLogined.Id;
            ViewBag.Packages = _smsPackageService.All()
         .Where(x => x.IsActive)
         .OrderByDescending(x => x.Order).ToList();
            model.SmsCharge = UserLogined.UserRouter.SmsCharge;
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                }
                else
                    this.MessageError(Captions.Error, Captions.UserPasswordClientError);
            }
            else
                this.MessageError(Captions.Error, Captions.IPPORTClientError);


            if (ModelState.IsValid)
            {
                await _applicationUserManager.UpdateUserRouterSmsSettingsAsync(model);
            }
            else
            {
                this.MessageError(Captions.Error, Captions.ValidateError);
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult BuySmsPackage(int id)
        {
            var paymentType = _paymentTypeService.All().FirstOrDefault();
            if (paymentType == null)
            {
                this.MessageError(Captions.MissionFail, "درگاه پرداختی در سیسیتم ثبت نشده. با مدیریت تماس بگیرید.");
                return RedirectToAction(MVC.MyRouter.Home.Sms());
            }

            var package = _smsPackageService.SingleOrDefault(id);
            var factor = new Factor()
            {
                PaymentTypeId = paymentType.Id,
                RegisterDate = DateTime.Now,
                FactorStatus = FactorStatus.Unpaid,
                IpAddress = this.Request.ServerVariables["REMOTE_ADDR"],
                PaymentPrice = package.Price,
                UserId = UserLogined.Id,
                FactorSmsDetail = new FactorSmsDetail()
                {
                    PackageName=package.Name,
                    SmsCount = package.SmsCount,
                    TotalPrice = package.Price,
                    UnitPrice = package.UnitPrice
                }
            };
            _factorService.Add(factor);
            _uow.SaveAllChanges();


            System.Net.ServicePointManager.Expect100Continue = false;
            var zp = new ZarinPalService.PaymentGatewayImplementationServicePortTypeClient();
            string Authority;

            int Status = zp.PaymentRequest(paymentType.MerchantId, (int)factor.PaymentPrice, "خرید بسته پیامکی", UserLogined.Email, UserLogined.PhoneNumber, Url.Action(MVC.MyRouter.Factor.Result(factor.Id), protocol: "https"), out Authority);

            if (Status == 100)
            {
                Response.Redirect(paymentType.GateWayUrl + Authority);
                //https://www.zarinpal.com/pg/StartPay/
            }
            else
            {
                this.MessageError(Captions.MissionFail, "خطا در برقراری ارتباط با درگاه پرداخت. کد وضعیت : " + Status);
            }

            return RedirectToAction(MVC.MyRouter.Home.Sms());
        }

        [HttpPost]
        public virtual ActionResult DisableSMS(long id)
        {
            if (UserLogined.Id != id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Sms, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            var user = _applicationUserManager.FindUserById(UserLogined.Id);
            user.UserRouter.SmsActive = false;
            _uow.SaveAllChanges();
            return RedirectToAction(MVC.MyRouter.Home.ActionNames.Sms, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
        }
        [HttpPost]
        public virtual ActionResult EnableSMS(long id)
        {
            if (UserLogined.Id != id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Sms, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            var user = _applicationUserManager.FindUserById(UserLogined.Id);
            user.UserRouter.SmsActive = true;
            _uow.SaveAllChanges();
            return RedirectToAction(MVC.MyRouter.Home.ActionNames.Sms, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
        }

        [NonAction]
        private void PopulatePermissions(params string[] selectedpermissions)
        {
            var permissions = AssignablePermissionToClient.GetAsSelectListItems();

            if (selectedpermissions != null)
            {
                permissions.ForEach(a => a.Selected = selectedpermissions.Any(s => s == a.Value));
            }

            ViewBag.ClientPermissions = permissions;
        }

        public virtual JsonResult GetUserCount()
        {
            var Count = "";
            try
            {
                Count = _mikrotikServices.Usermanager_GetUsersCount(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password).ToString();
            }
            catch(Exception ex)
            {
                Count = "Error";
            }
            return Json(new
            {
                Count = Count
            }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult GetPackageCount()
        {
            var Count = "";
            try
            {
                Count = _mikrotikServices.Usermanager_GetPackagesCount(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password).ToString();
            }
            catch (Exception ex)
            {
                Count = "Error";
            }
            return Json(new
            {
                Count = Count
            }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult GetActiceSessionCount()
        {
            var Count = "";
            try
            {
                Count = _mikrotikServices.Usermanager_GetActiveSessionsCount(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password).ToString();
            }
            catch (Exception ex)
            {
                Count = "Error";
            }
            return Json(new
            {
                Count = Count
            }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult GetRouterDateTime()
        {
            var Clock = new Router_ClockModel();
            try
            {
                Clock = _mikrotikServices.Router_Clock(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Clock.Router_date = "Error";
                Clock.Router_time = "Error";
            }
            return Json(new
            {

                ClockDate = EnglishConvertDate.ConvertToFa(Clock.Router_date, "D"),
                ClockTime = Clock.Router_time
            }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult GetLastProfile()
        {

            var Payments = _mikrotikServices.Usermanager_Payment(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, "").OrderByDescending(x => x.trans_end).Take(10);

            var Price = new string[10];
            var Trans = new string[10];

            int i = 0;
            foreach(var item in Payments)
            {
                Trans[i] = item.user + " - " + EnglishConvertDate.ConvertToFa(item.trans_end.Split(' ')[0], "d") + " " + item.trans_end.Split(' ')[1];
                Price[i] = item.price.Length>2 ? item.price.Remove(item.price.Length - 2, 2):item.price;
                i++;
            }
            return Json(new
            {
                Price = Price,
                Trans = Trans
            }, JsonRequestBehavior.AllowGet);
        }



    }
}