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
//Test Comment
namespace Netotik.Web.Controllers
{
    public partial class RegisterController : BaseController
    {
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IUserMailer _userMailer;
        private readonly IUnitOfWork _uow;
        private readonly IMenuService _menuService;

        public RegisterController(
            IApplicationUserManager applicationUserManager,
            IUserMailer userMailer,
            IMenuService menuService,
            IUnitOfWork uow)
        {
            _userMailer = userMailer;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
            _menuService = menuService;
        }

        [AllowAnonymous]
        public virtual ActionResult Company(string ReturnUrl, string CompanyName)
        {
            if (User != null && User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = "Company" });
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Company(Netotik.ViewModels.Identity.UserCompany.LoginModel model, string ReturnUrl, string CompanyName, int fromPage = 0)
        {
            return View();
        }


        [AllowAnonymous]
        public virtual ActionResult Reseller(string ReturnUrl)
        {
            IUserMailer mailer = new UserMailer();
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        // [CheckReferrer]
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