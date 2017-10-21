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
        private readonly ISmsService _smsService;
        private readonly IMenuService _menuService;

        public RegisterController(
            IMikrotikServices mikrotikservices,
            IApplicationUserManager applicationUserManager,
            IUserMailer userMailer,
            IMenuService menuService,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikservices;
            _userMailer = userMailer;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
            _smsService = smsService;
            _menuService = menuService;
        }
        [Route("{lang}/router/reg/{RouterCode}")]
        [AllowAnonymous]
        public virtual ActionResult Router(string ReturnUrl, string RouterName)
        {
            if (User != null && User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index, MVC.MyRouter.Home.Name, new { area = "Router" });
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{lang}/router/reg/{RouterCode}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Router(Netotik.ViewModels.Identity.UserRouter.LoginModel model, string ReturnUrl, string RouterName, int fromPage = 0)
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
        [Route("{lang}/userman/reg/{RouterCode}")]
        public virtual async Task<ActionResult> Client(string ReturnUrl, string RouterCode)
        {
            var RouterCodeToid = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            var UserLogined = _applicationUserManager.FindUserById(RouterCodeToid.Id);
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
                //errrrrrrrrrrrrrrrrooor
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }

            var Router = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (Router == null) return HttpNotFound();
            var User = _applicationUserManager.FindUserById(Router.Id);
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password);
            ViewBag.RouterName = RouterCode;
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.RegisterSetting = Router.UserRouter.UserRouterRegisterSetting;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{lang}/userman/reg/{RouterCode}")]
        public virtual async Task<ActionResult> Client(Netotik.ViewModels.Identity.UserClient.UserRegisterModel model, string ReturnUrl, string RouterCode)
        {
            var UserByRouterName = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            var UserLogined = _applicationUserManager.FindUserById(UserByRouterName.Id);
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //----------------------------
            var Router = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (Router == null) return HttpNotFound();
            var User = _applicationUserManager.FindUserById(Router.Id);
            model.customer = User.UserRouter.Userman_Customer;
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
            if (_mikrotikServices.Usermanager_IsUserExist(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password, Usermanuser.username))
            {
                //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
            }
            else
            {
                _mikrotikServices.Usermanager_UserCreate(User.UserRouter.R_Host, User.UserRouter.R_Port, User.UserRouter.R_User, User.UserRouter.R_Password, Usermanuser);
                if (User.UserRouter.SmsCharge > 0 && User.UserRouter.SmsActive && User.UserRouter.RegisterFormSms)
                {
                    _smsService.SendSms(model.phone, string.Format(Captions.SmsUserBuyPlan,model.username), User.Id);
                }
            }

            ViewBag.RouterName = RouterCode;
            ViewBag.ReturnUrl = ReturnUrl;
            _uow.SaveAllChanges();
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
                ModelState.AddModelError("Email", Captions.ExistError);

            if (_applicationUserManager.CheckUserNameExist(model.UserName, null))
                ModelState.AddModelError("UserName", Captions.ExistError);

            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", Captions.PasswordEasy);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var userId = await _applicationUserManager.AddReseller(model);

            await SendConfirmationEmail(model.Email, userId);


            ViewBag.Success = Captions.CreateSuccsessGoToEmail;

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