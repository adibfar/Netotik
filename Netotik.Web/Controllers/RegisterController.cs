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
using System.Web.Hosting;
using Netotik.ViewModels.Identity.UserClient;
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
        public virtual async Task<ActionResult> Client(string RouterCode)
        {
            var RouterCodeToid = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (RouterCodeToid == null) return HttpNotFound();
            var Router = _applicationUserManager.FindUserById(RouterCodeToid.Id);

            if (!Router.UserRouter.UserRouterRegisterSetting.ActiveRegisterForm)
                return HttpNotFound();
            if (!_mikrotikServices.IP_Port_Check(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
                //errrrrrrrrrrrrrrrrooor
            }
            if (!_mikrotikServices.User_Pass_Check(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
            }
            if (!_mikrotikServices.Usermanager_IsInstall(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
            }
            ViewBag.RouterName = RouterCode;
            ViewBag.RegisterSetting = Router.UserRouter.UserRouterRegisterSetting;
            return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{lang}/userman/reg/{RouterCode}")]
        public virtual async Task<ActionResult> Client(Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel model, string RouterCode)
        {
            var RouterCodeToid = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (RouterCodeToid == null) return HttpNotFound();
            var Router = _applicationUserManager.FindUserById(RouterCodeToid.Id);
            if (!Router.UserRouter.UserRouterRegisterSetting.ActiveRegisterForm)
                return HttpNotFound();
            ViewBag.RouterName = RouterCode;
            ViewBag.RegisterSetting = Router.UserRouter.UserRouterRegisterSetting;

            if (Router.UserRouter.UserRouterRegisterSetting.Age == FieldType.Required && model.Age == null)
                ModelState.AddModelError("Age", string.Format(Captions.RequiredError, model.Age));

            if (Router.UserRouter.UserRouterRegisterSetting.BirthDate == FieldType.Required && model.BirthDate == null)
                ModelState.AddModelError("BirthDate", string.Format(Captions.RequiredError, model.BirthDate));

            if (Router.UserRouter.UserRouterRegisterSetting.Email == FieldType.Required && string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", string.Format(Captions.RequiredError, model.Email));
            if (Router.UserRouter.UserRouterRegisterSetting.MobileNumber == FieldType.Required && string.IsNullOrWhiteSpace(model.MobileNumber))
                ModelState.AddModelError("MobileNumber", string.Format(Captions.RequiredError, model.MobileNumber));
            if (Router.UserRouter.UserRouterRegisterSetting.Name == FieldType.Required && string.IsNullOrWhiteSpace(model.Name))
                ModelState.AddModelError("Name", string.Format(Captions.RequiredError, model.Name));
            if (Router.UserRouter.UserRouterRegisterSetting.NationalCode == FieldType.Required && string.IsNullOrWhiteSpace(model.NationalCode))
                ModelState.AddModelError("NationalCode", string.Format(Captions.RequiredError, model.NationalCode));
            if (Router.UserRouter.UserRouterRegisterSetting.Password == PasswordFieldType.Required && string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError("Password", string.Format(Captions.RequiredError, model.Password));
            if (Router.UserRouter.UserRouterRegisterSetting.Password == PasswordFieldType.MobileNumber && string.IsNullOrWhiteSpace(model.MobileNumber))
                ModelState.AddModelError("MobileNumber", string.Format(Captions.RequiredError, model.MobileNumber));
            if (Router.UserRouter.UserRouterRegisterSetting.Password == PasswordFieldType.NationalCode && string.IsNullOrWhiteSpace(model.NationalCode))
                ModelState.AddModelError("NationalCode", string.Format(Captions.RequiredError, model.NationalCode));

            if (Router.UserRouter.UserRouterRegisterSetting.Password == PasswordFieldType.Required && Router.UserRouter.UserRouterRegisterSetting.PasswordConfirm == FieldType.Required && string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("Password", string.Format(Captions.RequiredError, model.Password));
                ModelState.AddModelError("PasswordConfirm", string.Format(Captions.RequiredError, model.PasswordConfirm));
            }
            if (Router.UserRouter.UserRouterRegisterSetting.Username == UsernameFieldType.Required && string.IsNullOrWhiteSpace(model.Username))
                ModelState.AddModelError("Username", string.Format(Captions.RequiredError, model.Username));

            if (Router.UserRouter.UserRouterRegisterSetting.Username == UsernameFieldType.Email && string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", string.Format(Captions.RequiredError, model.Email));

            if (Router.UserRouter.UserRouterRegisterSetting.Username == UsernameFieldType.MobileNumber && string.IsNullOrWhiteSpace(model.MobileNumber))
                ModelState.AddModelError("MobileNumber", string.Format(Captions.RequiredError, model.MobileNumber));

            if (Router.UserRouter.UserRouterRegisterSetting.Username == UsernameFieldType.NationalCode && string.IsNullOrWhiteSpace(model.NationalCode))
                ModelState.AddModelError("NationalCode", string.Format(Captions.RequiredError, model.NationalCode));

            if (!ModelState.IsValid)
                return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());

            if (!_mikrotikServices.IP_Port_Check(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
            }
            if (!_mikrotikServices.User_Pass_Check(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
            }
            if (!_mikrotikServices.Usermanager_IsInstall(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
            }
            if (!string.IsNullOrWhiteSpace(model.MobileNumber) && model.MobileNumber[0] == '0')
                model.MobileNumber = model.MobileNumber.Remove(0, 1);

            var UserClient = new Netotik.ViewModels.Identity.UserClient.UserRegisterModel();
            UserClient.Age = model.Age;
            UserClient.Birthday = model.BirthDate;
            UserClient.CreateDate = DateTime.Now;
            UserClient.customer = Router.UserRouter.Userman_Customer;
            UserClient.email = model.Email;
            UserClient.first_name = model.Name;
            UserClient.IsMale = model.Sex == ViewModels.Identity.UserClient.Sex.Male ? true : false;
            UserClient.last_name = model.Name;
            UserClient.NationalCode = model.NationalCode;
            UserClient.phone = model.MobileNumber;
            UserClient.profile = Router.UserRouter.UserRouterRegisterSetting.ProfileName;

            switch (Router.UserRouter.UserRouterRegisterSetting.Password)
            {
                case PasswordFieldType.None:
                    var rand = new Random();
                    UserClient.password = rand.Next(1000, 9999).ToString();
                    break;
                case PasswordFieldType.Required:
                    UserClient.password = model.Password;
                    break;
                case PasswordFieldType.MobileNumber:
                    UserClient.password = model.MobileNumber;
                    break;
                case PasswordFieldType.NationalCode:
                    UserClient.password = model.NationalCode;
                    break;
                default:
                    break;
            }

            switch (Router.UserRouter.UserRouterRegisterSetting.Username)
            {
                case UsernameFieldType.None:
                    var Rand = new Random();
                    bool flag = true;
                    while (flag)
                    {
                        var Randoms = Rand.Next(1000, 999999);
                        if (_mikrotikServices.Usermanager_GetUser(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password, Randoms.ToString()).FirstOrDefault().username != Randoms.ToString())
                        {
                            flag = false;
                            UserClient.username = Randoms.ToString();
                        }
                    }
                    break;
                case UsernameFieldType.Required:
                    UserClient.username = model.Username;
                    break;
                case UsernameFieldType.MobileNumber:
                    UserClient.username = model.MobileNumber;
                    break;
                case UsernameFieldType.NationalCode:
                    UserClient.username = model.NationalCode;
                    break;
                case UsernameFieldType.Email:
                    UserClient.username = model.Email;
                    break;
                default:
                    break;
            }



            var UserFind = _mikrotikServices.Usermanager_GetUser(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password, UserClient.username).FirstOrDefault();
            if (UserFind != null && !string.IsNullOrEmpty(UserFind.username))
                if (UserFind.username != UserClient.username)
                    _mikrotikServices.Usermanager_UserCreate(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password, UserClient);
                else
                {
                    if (Convert.ToDateTime((UserFind.EditDate.Value.Year == 1 ? UserFind.CreateDate : UserFind.EditDate)).AddHours(Router.UserRouter.UserRouterRegisterSetting.RegisterAgianHour) <= DateTime.Now)
                    {
                        _mikrotikServices.Usermanager_ResetUserProfiles(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password, UserFind.username);
                        var UserEditmodel = new UserEditModel()
                        {
                            Age = model.Age,
                            Birthday = UserFind.Birthday,
                            CreateDate = UserFind.CreateDate,
                            IsMale = model.Sex == Sex.Male ? true : false,
                            MarriageDate = UserFind.MarriageDate,
                            NationalCode = model.NationalCode,
                            profile = Router.UserRouter.UserRouterRegisterSetting.ProfileName,
                            password = model.Password,
                            id = UserFind.id,
                            username = model.Username
                        };
                        _mikrotikServices.Usermanager_UserEdit(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password, UserEditmodel);
                    }
                    else
                    {
                        this.MessageError(Captions.Error, string.Format(Captions.ExistError, UserClient.username));
                        return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
                    }
                }
            _mikrotikServices.Usermanager_UserCreate(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password, UserClient);
            UserFind = _mikrotikServices.Usermanager_GetUser(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password, UserClient.username).FirstOrDefault();
            this.MessageSuccess(Captions.MissionSuccess, Captions.RegisterDone);
            if (Router.UserRouter.UserRouterRegisterSetting.ShowUserPass)
                ViewBag.UserFind = UserFind;

            //-------------Email
            if (Router.UserRouter.UserRouterRegisterSetting.SendEmailUserPass && !string.IsNullOrWhiteSpace(UserFind.email))
            {
                _userMailer.ClientUserPass(new EmailClientUserPassViewModel
                {
                    To = UserFind.email,
                    PanelLoginLink = Url.Action(MVC.Login.Client("", Router.UserRouter.RouterCode), protocol: "https"),
                    Password = UserFind.password,
                    Profile = UserFind.actual_profile,
                    RouterCode = Router.UserRouter.RouterCode,
                    Subject = Captions.AdminUserCreated,
                    Username = UserFind.username,
                    ViewName = MVC.UserMailer.Views.ViewNames.ClientUserPass
                }
                   ).Send();
            }

            //--------------SMS
            if (Router.UserRouter.RegisterFormSms && (UserFind.phone != null || UserFind.phone != "") && Router.UserRouter.SmsActive && Router.UserRouter.SmsCharge > 0)
            {
                string SmsText = Router.UserRouter.RegisterWithSmsMessage + "\n User: " + UserFind.username + "\n Pass: " + UserFind.password;
                _smsService.SendSms(UserFind.phone, SmsText, Router.Id);
                _uow.SaveAllChanges();
            }

            return View(new Netotik.ViewModels.Identity.UserClient.UserClientRegisterModel());
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
                Message = Captions.ActivationMailMessage,
                To = email,
                Url = callbackUrl,
                UrlText = Captions.ActivationMailSubject,
                Subject = Captions.ActivationMailSubject,
                ViewName = MVC.UserMailer.Views.ViewNames.ConfirmAccount
            }).Send();

        }




    }
}