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
                ViewBag.Message = "اطلاعات وارد شده صحیح نیست.";
                return View(model);
            }

            var loggedinUser = await _applicationUserManager.FindAsync(model.UserName, model.Password);
            if (loggedinUser == null || loggedinUser.IsDeleted)
            {
                ViewBag.Message = "اطلاعات وارد شده صحیح نیست.";
                return View(model);
            }

            if (loggedinUser.IsBanned)
            {
                ViewBag.Message = "حساب کاربری شما مسدود شده است.";
                return View(model);
            }

            if (!loggedinUser.EmailConfirmed)
            {
                ViewBag.Message = "برای ورود به سایت لازم است حساب خود را فعال کنید.";
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
                    return RedirectToLocal(ReturnUrl);
                case SignInStatus.LockedOut:
                    ModelState.AddModelError("UserName",
                        $"دقیقه دوباره امتحان کنید {_applicationUserManager.DefaultAccountLockoutTimeSpan} حساب شما قفل شد ! لطفا بعد از ");
                    return View(model);
                case SignInStatus.Failure:
                    ModelState.AddModelError("UserName", "نام کاربری یا کلمه عبور  صحیح نمی باشد");
                    ModelState.AddModelError("Password", "نام کاربری یا کلمه عبور  صحیح نمی باشد");
                    return View(model);
                default:
                    ModelState.AddModelError("UserName",
                        "در این لحظه امکان ورود به  سابت وجود ندارد . مراتب را با مسئولان سایت در میان بگذارید"
                       );
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
                return View();
            this.MessageWarning("ناموفق","مشکلی در فعال سازی اکانت شما به وجود آمد");
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
                Message = "با سلام کاربر گرامی.برای بازیابی کلمه عبور خود لازم است بر روی لینک مقابل کلیک کنید",
                To = model.Email,
                Url = callbackUrl,
                UrlText = "بازیابی کلمه عبور",
                Subject = "بازیابی کلمه عبور",
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
        [CaptchaVerify("تصویر امنیتی را درست وارد کنید")]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", "این کلمه عبور به راحتی قابل تشخیص است");

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
                return RedirectToAction(MVC.Account.ResetPasswordConfirmation());
            }
            //this.AddErrors(result);
            this.MessageError("خطا: ",ModelState.GetListOfErrors());
            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult ResetPasswordConfirmation()
        {
            return View();
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
        [CaptchaVerify("تصویر امنیتی را درست وارد کنید")]
        public virtual async Task<ActionResult> ReceiveActivatorEmail(ActivationEmailViewModel viewModel)
        {
            if (!_userManager.IsEmailAvailableForConfirm(viewModel.Email))
                ModelState.AddModelError("Email", "ایمیل مورد نظر یافت نشد");
            if (_userManager.CheckIsUserBannedOrDeleteByEmail(viewModel.Email))
                ModelState.AddModelError("Email", "اکانت شما مسدود شده است");
            if (!ModelState.IsValid)
                return View(viewModel);
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            await SendConfirmationEmail(viewModel.Email, user.Id);
            this.MessageSuccess("موفق","ایمیلی تحت عنوان فعال سازی اکانت به آدرس ایمیل شما ارسال گردید");
            return RedirectToAction(MVC.Account.ReceiveActivatorEmail());
        }



        public async Task SendConfirmationEmail(string email, long userId)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Abs(Url.Action(MVC.Account.ActionNames.ConfirmEmail, MVC.Account.Name,
                new { userId, code, area = "" }, protocol: Request.Url.Scheme));

            _userMailer.ConfirmAccount(new EmailViewModel
            {
                Message = "با سلام کاربر گرامی.برای فعال سازی حساب خود لازم است بر روی لینک مقابل کلیک کنید",
                To = email,
                Url = callbackUrl,
                UrlText = "فعال سازی",
                Subject = "فعال سازی اکانت کاربری",
                ViewName = MVC.UserMailer.Views.ViewNames.ConfirmAccount
            }).Send();

        }

        #endregion



    }
}