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
using Netotik.ViewModels.Identity.UserCompany;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Microsoft.AspNet.Identity;
using Netotik.ViewModels.Identity.Security;
using WebGrease.Css.Extensions;
using Telegram.Bot;

namespace Netotik.Web.Areas.Company.Controllers
{
    [Mvc5Authorize(Roles = "Company")]
    [BreadCrumb(Title = "User", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class HomeController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly ISmsService _smsService;
        private readonly IUnitOfWork _uow;

        public HomeController(
            IMikrotikServices mikrotikServices,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            ISmsService smsService,
            IUnitOfWork uow)
        {
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
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
            }
            try
            {
                ViewBag.UsersCount = _mikrotikServices.Usermanager_GetUsersCount(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password).ToString();
            }
            catch { ViewBag.UsersCount = "Error"; }
            try
            {
                ViewBag.PackageCount = _mikrotikServices.Usermanager_GetPackagesCount(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password).ToString();
            }
            catch { ViewBag.PackageCount = "Error"; }
            try
            {
                ViewBag.ActiveSessionsCount = _mikrotikServices.Usermanager_GetActiveSessionsCount(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password).ToString();
            }
            catch { ViewBag.ActiveSessionsCount = "Error"; }
            try
            {
                ViewBag.Payments = _mikrotikServices.Usermanager_Payment(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, "").OrderByDescending(x => x.trans_end).Take(10);
            }
            catch { ViewBag.Payments = "Error"; }




            ViewBag.Clock = _mikrotikServices.Router_Clock(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password).FirstOrDefault();
            return View();
        }

        public virtual ActionResult MyProfile()
        {
            return View();
        }

        public virtual ActionResult ProfileData()
        {
            var company = _applicationUserManager.GetUserCompanyProfile(UserLogined.Id);
            PopulatePermissions(_applicationUserManager.FindClientPermissions(company.Id).ToArray());

            return PartialView(MVC.Company.Home.Views._ProfileData, company);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> changeImageProfile(HttpPostedFileBase image)
        {
            var user = await _applicationUserManager.FindByIdAsync(User.Identity.GetUserId<long>());

            if (image == null)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(MVC.Company.Home.Views.MyProfile);
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
            return RedirectToAction(MVC.Company.Home.MyProfile());
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
                return RedirectToAction(MVC.Company.Home.ActionNames.MyProfile);
            }
            #endregion
            if (model.Email != UserLogined.Email)
                model.EmailConfirmed = false;
            model.Id = UserLogined.Id;
            model.UserResellerId = UserLogined.UserCompany.UserResellerId;

            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserCompanyProfile(model);
            return RedirectToAction(MVC.Company.Home.ActionNames.MyProfile);
        }

        public virtual ActionResult MikrotikConf()
        {
            return View(_applicationUserManager.GetUserCompanyMikrotikConf(UserLogined.Id));
        }
        public virtual ActionResult TelegramBot()
        {
            return View(_applicationUserManager.GetUserCompanyTelegramBot(UserLogined.Id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> TelegramBot(ViewModels.Identity.UserCompany.TelegramBotModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.Company.Home.ActionNames.TelegramBot, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            #endregion
            if (model.TelegramBotToken != "" || model.TelegramBotToken != null)
            {
                TelegramBotClient Api = new TelegramBotClient(model.TelegramBotToken);
                //بروز کردن وب هوک ربات مربوطه
                string APIUrl = string.Format("https://netotik.com:443/api/telegrambot/company/{0}", UserLogined.UserCompany.CompanyCode);
                Api.SetWebhookAsync(APIUrl).Wait();
            }
            model.Id = UserLogined.Id;
            
            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserCompanyTelegramBot(model);
            return RedirectToAction(MVC.Company.Home.ActionNames.TelegramBot, MVC.Company.Home.Name, new { area = MVC.Company.Name });
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
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            #endregion
            if (model.R_Password == "" || model.R_Password == null)
            {
                model.R_Password = UserLogined.UserCompany.R_Password;
                this.MessageInformation(Captions.Attention, Captions.RouterPasswordEmptyInformation);
            }
            model.Id = UserLogined.Id;
            if (model.cloud == true)
            {
                if (_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
                    if (_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
                        model.R_Host = _mikrotikServices.EnableAndGetCloud(model.R_Host, model.R_Port, model.R_User, model.R_Password);
            }
            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserCompanyMikrotikConf(model);
            return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
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
                return RedirectToAction(MVC.Company.Home.ActionNames.ChangePassword);
            }
            var temp = await _applicationUserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.Password);
            if (temp.Succeeded)
            {
                if (UserLogined.UserCompany.SmsCharge > 0 && UserLogined.UserCompany.SmsActive && UserLogined.UserCompany.SmsAdminChangeAdminPassword)
                    _smsService.SendSms(UserLogined.PhoneNumber, string.Format(Captions.SmsCompanyPasswordChange,UserLogined.UserName), UserLogined.Id);
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
            Model = _applicationUserManager.GetUserCompanySmsSettings(UserLogined.Id);
            return View(Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Sms(SmsModel model)
        {
            model.Id = UserLogined.Id;
            if (ModelState.IsValid)
            {
                await _applicationUserManager.UpdateUserCompanySmsSettingsAsync(model);
            }else
            {
                this.MessageError(Captions.Error, Captions.ValidateError);
                return View(_applicationUserManager.GetUserCompanySmsSettings(UserLogined.Id));
            }
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult DisableSMS(long id)
        {
            if(UserLogined.Id!=id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Sms, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            var user=_applicationUserManager.FindUserById(UserLogined.Id);
            user.UserCompany.SmsActive = false;
            _uow.SaveAllChanges();
            return RedirectToAction(MVC.Company.Home.ActionNames.Sms, MVC.Company.Home.Name, new { area = MVC.Company.Name });
        }
        [HttpPost]
        public virtual ActionResult EnableSMS(long id)
        {
            if (UserLogined.Id != id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Sms, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            var user = _applicationUserManager.FindUserById(UserLogined.Id);
            user.UserCompany.SmsActive = true;
            _uow.SaveAllChanges();
            return RedirectToAction(MVC.Company.Home.ActionNames.Sms, MVC.Company.Home.Name, new { area = MVC.Company.Name });
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

    }
}