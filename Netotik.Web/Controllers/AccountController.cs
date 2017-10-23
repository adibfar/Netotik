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
using Netotik.Common.Security;
using System.Threading.Tasks;
using Netotik.Web.Infrastructure;
using CaptchaMvc.Attributes;
using Netotik.Services.Identity;
using Microsoft.Owin.Security;
using Netotik.ViewModels.Identity.Account;
using Mvc.Mailer;
using System.Web.UI;
using System.Net;
using Microsoft.AspNet.Identity.Owin;
using Netotik.Common.Filters;
using Microsoft.AspNet.Identity;
using Netotik.Common.Controller;
using Netotik.Resources;

namespace Netotik.Web.Controllers
{
    public partial class AccountController : BasePanelController
    {
        #region Fields

        private readonly HttpContextBase _httpContextBase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IUserMailer _userMailer;
        #endregion

        #region Constructor

        public AccountController(HttpContextBase httpContextBase, IUnitOfWork unitOfWork,
            IApplicationUserManager applicationUserManager,
            IApplicationSignInManager applicationSignInManager,
            IAuthenticationManager authenticationManager, IUserMailer userMailer
           )
        {
            _applicationUserManager = applicationUserManager;
            _applicationSignInManager = applicationSignInManager;
            _authenticationManager = authenticationManager;
            _userMailer = userMailer;
            _unitOfWork = unitOfWork;
            _httpContextBase = httpContextBase;
        }

        #endregion




        [AllowAnonymous]
        public virtual ActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (UserLogined.UserType==Domain.Entity.UserType.UserAdmin)
                {
                    return RedirectToAction(MVC.Admin.Home.MyProfile());
                }
                else if (UserLogined.UserType == Domain.Entity.UserType.UserReseller)
                {
                    return RedirectToAction(MVC.Reseller.Home.MyProfile());
                }
                else if (UserLogined.UserType == Domain.Entity.UserType.UserRouter)
                {
                    return RedirectToAction(MVC.MyRouter.Home.MyProfile());
                }
            }

            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(ViewModels.Identity.UserAdmin.LoginModel model, string ReturnUrl)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Message = Captions.UsernameOrPasswordWrong;
                return View(model);
            }

            var loggedinUser = await _applicationUserManager.FindAsync(model.UserName, model.Password);
            if (loggedinUser == null || loggedinUser.IsDeleted)
            {
                ViewBag.Message = Captions.UsernameOrPasswordWrong;
                return View(model);
            }
            if (loggedinUser.UserType != Domain.Entity.UserType.UserAdmin)
            {
                ViewBag.Message = Captions.UsernameOrPasswordWrong;
                return View(model);
            }

            if (loggedinUser.IsBanned)
            {
                ViewBag.Message = Captions.YourAccountIsBlock;
                return View(model);
            }

            if (!loggedinUser.EmailConfirmed)
            {
                ViewBag.Message = Captions.ActiveYourAccount;
                ViewBag.Link = true;
                return View();
            }

            if (loggedinUser != null)
            {
                await _applicationUserManager.UpdateSecurityStampAsync(loggedinUser.Id);
            }

            var result = await _applicationSignInManager.PasswordSignInAsync
                (model.UserName, model.Password, model.RememberMe, shouldLockout: true);


            switch (result)
            {
                case SignInStatus.Success:
                    loggedinUser.LastLoginDate = DateTime.Now;
                    loggedinUser.LastLoginIpAddress = GetMyIp();
                    await _unitOfWork.SaveAllChangesAsync();

                    if (!string.IsNullOrWhiteSpace(ReturnUrl))
                        return RedirectToLocal(ReturnUrl);
                    else
                        return RedirectToAction(MVC.Admin.Home.Index());

                case SignInStatus.LockedOut:
                    ModelState.AddModelError("UserName", string.Format(Captions.AccountLockMessage, _applicationUserManager.DefaultAccountLockoutTimeSpan));
                    return View(model);
                case SignInStatus.Failure:
                    ModelState.AddModelError("UserName", Captions.UsernameOrPasswordWrong);
                    ModelState.AddModelError("Password", Captions.UsernameOrPasswordWrong);
                    return View(model);
                default:
                    ModelState.AddModelError("UserName", Captions.CantLogin);
                    return View(model);
            }
        }
        #region ConfirmEmail
        [AllowAnonymous]
        public virtual async Task<ActionResult> ConfirmEmail(int? userId, string code)
        {
            if (userId == null || code == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var result = await _userManager.ConfirmEmailAsync(userId.Value, code);
            if (result.Succeeded)
            {
                string LoginURL = "";
                var user = _userManager.FindUserById(userId.Value);
                if (user.UserType == Domain.Entity.UserType.UserAdmin)
                {
                    LoginURL = Url.Action(MVC.Account.Login());
                }
                if (user.UserType == Domain.Entity.UserType.UserRouter)
                {
                    var ResellerRouterName = _userManager.FindUserById(user.UserRouter.UserResellerId);
                    LoginURL = Url.Action(MVC.Login.Router("", ResellerRouterName.UserReseller.ResellerCode));
                }
                if (user.UserType == Domain.Entity.UserType.UserReseller)
                {
                    LoginURL = Url.Action(MVC.Login.Reseller());
                }
                ViewBag.LoginPage = LoginURL;
                return View();
            }
            this.MessageWarning(Captions.MissionFail, Captions.ActivationError);
            return RedirectToAction(MVC.Account.ReceiveActivatorEmail());
        }
        #endregion


        #region ForgetPassword
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return View(MVC.Account.Views.ViewNames.ResetPasswordConfirmation);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            if (Request.Url == null) return View(MVC.Account.Views.ViewNames.ForgotPasswordConfirmation);
            var callbackUrl = Url.Action(MVC.Account.ActionNames.ResetPassword, MVC.Account.Name,
                new { userId = user.Id, code }, protocol: Request.Url.Scheme);
            _userMailer.ResetPassword(new EmailViewModel
            {
                Message = Captions.ForgetPasswordMailMessage,

                To = model.Email,
                Url = callbackUrl,
                UrlText = Captions.ForgetPasswordMailSubject,
                Subject = Captions.ForgetPasswordMailSubject,
                ViewName = MVC.UserMailer.Views.ViewNames.ResetPassword
            }
               ).Send();

            return View(MVC.Account.ActionNames.ForgotPasswordConfirmation);
        }

        [AllowAnonymous]
        public virtual ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        #endregion

        #region Login,LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        // [CheckReferrer]
        [Mvc5Authorize]
        public virtual ActionResult LogOff()
        {
            _authenticationManager.SignOut
                (
                    DefaultAuthenticationTypes.ExternalCookie,
                    DefaultAuthenticationTypes.ApplicationCookie
                );

            return RedirectToAction(MVC.Home.Index());
        }

        #endregion


        #region ResePassword

        [AllowAnonymous]
        public virtual async Task<ActionResult> ResetPassword(long userId, string code)
        {
            if (await _userManager.VerifyUserTokenAsync(userId, "ResetPassword", code))
                return View(new ResetPasswordViewModel { Code = code });
            return HttpNotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", Captions.PasswordEasy);

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email.ToLower());

            if (user == null)
                return RedirectToAction(MVC.Account.ActionNames.ResetPasswordConfirmation, MVC.Account.Name);

            if (!await _userManager.VerifyUserTokenAsync(user.Id, "ResetPassword", model.Code))
                return HttpNotFound();

            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                await _applicationSignInManager.SignInAsync(user, false, false);
                if (user.UserType == Domain.Entity.UserType.UserReseller)
                    return RedirectToAction(MVC.Account.ResetPasswordConfirmation(user.Id));
                if (user.UserType == Domain.Entity.UserType.UserRouter)
                    return RedirectToAction(MVC.Account.ResetPasswordConfirmation(user.Id));
                if (user.UserType == Domain.Entity.UserType.UserAdmin)
                    return RedirectToAction(MVC.Account.ResetPasswordConfirmation(user.Id));
            }
            //this.AddErrors(result);
            this.MessageError(Captions.MissionFail, ModelState.GetListOfErrors());
            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult ResetPasswordConfirmation(long id)
        {
            var user = _userManager.FindUserById(id);
            return View(user);
        }
        #endregion

        #region ReceiveActivatorEmail
        [AllowAnonymous]
        public virtual ActionResult ReceiveActivatorEmail()
        {
            //if(enable receiveactivator feature then show receiveactivator page)
            //return view("info")
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ReceiveActivatorEmail(ActivationEmailViewModel viewModel)
        {
            if (!_userManager.IsEmailAvailableForConfirm(viewModel.Email))
                ModelState.AddModelError("Email", Captions.EmailNotFound);
            if (_userManager.CheckIsUserBannedOrDeleteByEmail(viewModel.Email))
                ModelState.AddModelError("Email", Captions.YourAccountIsBlock);
            if (!ModelState.IsValid)
                return View(viewModel);
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            await SendConfirmationEmail(viewModel.Email, user.Id);
            this.MessageSuccess(Captions.MissionSuccess, Captions.WillSendActivationAccountMessage);
            return RedirectToAction(MVC.Account.ReceiveActivatorEmail());
        }



        public async Task SendConfirmationEmail(string email, long userId)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Abs(Url.Action(MVC.Account.ActionNames.ConfirmEmail, MVC.Account.Name,
                new { userId, code, area = "" }, protocol: Request.Url.Scheme));

            _userMailer.ConfirmAccount(new EmailViewModel
            {
                Message = Captions.ActivationMailMessage,
                To = email,
                Url = callbackUrl,
                UrlText = Captions.ActivationMailSubject,
                Subject = Captions.ActivationMailSubject,
                ViewName = MVC.UserMailer.Views.ViewNames.ConfirmAccount
            }).Send();

        }

        #endregion



    }
}