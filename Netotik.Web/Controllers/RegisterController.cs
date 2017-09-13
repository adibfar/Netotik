using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.Resources;
using Netotik.Domain.Entity;
using Netotik.Common.Security;
using Netotik.Common.Filters;
using Netotik.ViewModels;
using Netotik.Services.Enums;
using System.Threading.Tasks;
using Netotik.Web.Infrastructure.Filters;
using Netotik.Web.Infrastructure;
using Netotik.Common;
using CaptchaMvc.Attributes;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.UI;
using Netotik.ViewModels.Identity.UserReseller;
using Netotik.Services.Identity;
using Netotik.Services.Implement;
using Mvc.Mailer;
using Netotik.ViewModels.Identity.Account;
using Netotik.Common.Controller;
//Test Comment
namespace Netotik.Web.Controllers
{
    public partial class RegisterController : BaseController
    {
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUserMailer _userMailer;
        private readonly IUnitOfWork _uow;
        private readonly IMenuService _menuService;

        public RegisterController(
            IMikrotikServices mikrotikservices,
            IApplicationUserManager applicationUserManager,
            IUserMailer userMailer,
            IMenuService menuService,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikservices;
            _userMailer = userMailer;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
            _menuService = menuService;
        }
        [Route("{lang}/router/reg/{CompanyCode}")]
        [AllowAnonymous]
        public virtual ActionResult Company(string ReturnUrl, string CompanyName)
        {
            if (User != null && User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = "Company" });
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{lang}/router/reg/{CompanyCode}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Company(Netotik.ViewModels.Identity.UserCompany.LoginModel model, string ReturnUrl, string CompanyName, int fromPage = 0)
        {
            return View();
        }

        [Route("{lang}/reg/reseller")]
        [AllowAnonymous]
        public virtual ActionResult Reseller(string ReturnUrl)
        {
            IUserMailer mailer = new UserMailer();
            return View();
        }

        [AllowAnonymous]
        [Route("{lang}/userman/reg/{CompanyCode}")]
        public virtual async Task<ActionResult> Client(string ReturnUrl, string CompanyCode)
        {
            var CompanyCodeToid = await _applicationUserManager.FindByCompanyCodeAsync(CompanyCode);
            var UserLogined = _applicationUserManager.FindUserById(CompanyCodeToid.Id);
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
                //errrrrrrrrrrrrrrrrooor
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }

            var company = await _applicationUserManager.FindByCompanyCodeAsync(CompanyCode);
            if (company == null) return HttpNotFound();
            var User = _applicationUserManager.FindUserById(company.Id);
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(User.UserCompany.R_Host, User.UserCompany.R_Port, User.UserCompany.R_User, User.UserCompany.R_Password);
            ViewBag.CompanyName = CompanyCode;
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.RegisterSetting = company.UserCompany.UserCompanyRegisterSetting;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{lang}/userman/reg/{CompanyCode}")]
        public virtual async Task<ActionResult> Client(Netotik.ViewModels.Identity.UserClient.UserRegisterModel model, string ReturnUrl, string CompanyCode)
        {
            var UserByCompanyName = await _applicationUserManager.FindByCompanyCodeAsync(CompanyCode);
            var UserLogined = _applicationUserManager.FindUserById(UserByCompanyName.Id);
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
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //----------------------------
            var company = await _applicationUserManager.FindByCompanyCodeAsync(CompanyCode);
            if (company == null) return HttpNotFound();
            var User = _applicationUserManager.FindUserById(company.Id);
            model.customer = User.UserCompany.Userman_Customer;
            var Usermanuser = new Netotik.ViewModels.Identity.UserClient.UserRegisterModel()
            {
                username = model.username,
                email = model.email,
                phone = model.phone,
                first_name = model.first_name,
                last_name = model.last_name,
                password = model.password,
                comment = model.comment,
                customer = model.customer,
                location = model.location,
                profile = Request.Form["profile"].ToString()
            };
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "خطا";
                return RedirectToAction(MVC.Register.ActionNames.Client);
            }
            if (_mikrotikServices.Usermanager_IsUserExist(User.UserCompany.R_Host, User.UserCompany.R_Port, User.UserCompany.R_User, User.UserCompany.R_Password, Usermanuser.username))
            {
                //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
            }
            else
            {
                _mikrotikServices.Usermanager_UserCreate(User.UserCompany.R_Host, User.UserCompany.R_Port, User.UserCompany.R_User, User.UserCompany.R_Password, Usermanuser);
            }

            ViewBag.CompanyName = CompanyCode;
            ViewBag.ReturnUrl = ReturnUrl;
            return RedirectToAction(MVC.Register.ActionNames.Client);
        }


        [HttpPost]
        [AllowAnonymous]
        // [CheckReferrer]
        [Route("{lang}/reg/reseller")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Reseller(RegisterViewModel model)
        {
            #region Validation
            if (_applicationUserManager.CheckResellerEmailExist(model.Email, null))
                ModelState.AddModelError("Email", "این ایمیل قبلا در سیستم ثبت شده است");

            if (_applicationUserManager.CheckUserNameExist(model.UserName, null))
                ModelState.AddModelError("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");

            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", "این کلمه عبور به راحتی قابل تشخیص است");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var userId = await _applicationUserManager.AddReseller(model);

            await SendConfirmationEmail(model.Email, userId);


            ViewBag.Success = "حساب کاربری شما با موفقیت ایجاد شد. برای فعال سازی " +
                              "حساب خود به ایمیل خود مراجعه کنید";

            ModelState.Clear();
            return View();
        }

        public async Task SendConfirmationEmail(string email, long userId)
        {
            var code = await _applicationUserManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Abs(Url.Action(MVC.Account.ActionNames.ConfirmEmail, MVC.Account.Name,
                new { userId, code, area = "" }, protocol: Request.Url.Scheme));

            _userMailer.ConfirmAccount(new EmailViewModel
            {
                Message =Captions.ActivationMailMessage,
                To = email,
                Url = callbackUrl,
                UrlText = Captions.ActivationMailSubject,
                Subject = Captions.ActivationMailSubject,
                ViewName = MVC.UserMailer.Views.ViewNames.ConfirmAccount
            }).Send();

        }




    }
}