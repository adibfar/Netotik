using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Web.Security;
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
using Netotik.Services.Identity;
using Netotik.Common;
using CaptchaMvc.Attributes;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using Netotik.ViewModels.Identity.Security;
using System;
//Test Comment
namespace Netotik.Web.Controllers
{
    [Authorize]
    public partial class LoginController : BaseController
    {
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUnitOfWork _uow;
        private readonly ISmsService _smsService;
        private readonly IMenuService _menuService;

        public LoginController(
            IMikrotikServices mikrotikServices,
            IApplicationRoleManager applicationRoleManager,
            IApplicationSignInManager applicationSignInManager,
            IApplicationUserManager applicationUserManager,
            IMenuService menuService,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _applicationRoleManager = applicationRoleManager;
            _applicationSignInManager = applicationSignInManager;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
            _smsService = smsService;
            _menuService = menuService;
        }


        [AllowAnonymous]
        [Route("{lang}/router/{ResellerCode}")]
        public virtual async Task<ActionResult> Router(string ReturnUrl, string ResellerCode)
        {
            if (User.Identity.IsAuthenticated && _applicationRoleManager.FindUserPermissions(long.Parse(User.Identity.GetUserId())).Any(x => x == "Router"))
                return RedirectToAction(MVC.MyRouter.Home.Index());

            var reseller = await _applicationUserManager.FindByResellerCodeAsync(ResellerCode);
            if (reseller == null) return HttpNotFound();

            ViewBag.user = _applicationUserManager.FindUserById(reseller.Id);
            ViewBag.RouterName = ResellerCode;
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{lang}/router/{ResellerCode}")]
        public virtual async Task<ActionResult> Router(Netotik.ViewModels.Identity.UserRouter.LoginModel model, string ReturnUrl, string ResellerCode)
        {
            var reseller = await _applicationUserManager.FindByResellerCodeAsync(ResellerCode);
            if (reseller == null) return HttpNotFound();
            ViewBag.user = _applicationUserManager.FindUserById(reseller.Id);
            ViewBag.RouterName = ResellerCode;
            ViewBag.ReturnUrl = ReturnUrl;

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

            if (loggedinUser.UserType != Domain.Entity.UserType.UserRouter)
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

            //if (loggedinUser != null)
            //{
            //    await _applicationUserManager.UpdateSecurityStampAsync(loggedinUser.Id);
            //}

            var result = await _applicationSignInManager.PasswordSignInAsync
                (model.UserName, model.Password, model.RememberMe, shouldLockout: true);


            switch (result)
            {
                case SignInStatus.Success:
                    loggedinUser.LastLoginDate = DateTime.Now;
                    loggedinUser.LastLoginIpAddress = GetMyIp();
                    
                    if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    {
                        if (loggedinUser.UserRouter.SmsCharge > 0 && loggedinUser.UserRouter.SmsActive && loggedinUser.UserRouter.SmsAdminLogins)
                            _smsService.SendSms(loggedinUser.PhoneNumber, string.Format(Captions.SmsRouterLogins, loggedinUser.UserName, PersianDate.ConvertDate.ToFa(DateTime.Now, "g"), GetMyIp()), loggedinUser.Id);
                        await _uow.SaveAllChangesAsync();
                        return RedirectToLocal(ReturnUrl);
                    }
                    else
                    {
                        if (loggedinUser.UserRouter.SmsCharge > 0 && loggedinUser.UserRouter.SmsActive && loggedinUser.UserRouter.SmsAdminLogins)
                            _smsService.SendSms(loggedinUser.PhoneNumber, string.Format(Captions.SmsRouterLogins, loggedinUser.UserName,PersianDate.ConvertDate.ToFa(DateTime.Now,"g").ToString(), GetMyIp()), loggedinUser.Id);
                        await _uow.SaveAllChangesAsync();
                        return RedirectToAction(MVC.MyRouter.Home.Index());
                    }
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

        [Route("{lang}/login/reseller")]
        [AllowAnonymous]
        public virtual ActionResult Reseller(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated && _applicationRoleManager.FindUserPermissions(long.Parse(User.Identity.GetUserId())).Any(x => x == "Reseller"))
                return RedirectToAction(MVC.Reseller.Home.Index());

            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [Route("{lang}/login/reseller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Reseller(ViewModels.Identity.UserReseller.LoginModel model, string ReturnUrl, int fromPage = 0)
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


            if (loggedinUser.UserType != Domain.Entity.UserType.UserReseller)
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

            //if (loggedinUser != null)
            //{
            //    await _applicationUserManager.UpdateSecurityStampAsync(loggedinUser.Id);
            //}

            var result = await _applicationSignInManager.PasswordSignInAsync
                (model.UserName, model.Password, model.RememberMe, shouldLockout: true);


            switch (result)
            {
                case SignInStatus.Success:
                    loggedinUser.LastLoginDate = DateTime.Now;
                    loggedinUser.LastLoginIpAddress = GetMyIp();
                    await _uow.SaveAllChangesAsync();

                    if (!string.IsNullOrWhiteSpace(ReturnUrl))
                        return RedirectToLocal(ReturnUrl);
                    else
                        return RedirectToAction(MVC.Reseller.Home.Index());

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



        [AllowAnonymous]
        [Route("{lang}/userman/{RouterCode}")]
        public virtual async Task<ActionResult> Client(string ReturnUrl, string RouterCode)
        {
            if (User.Identity.IsAuthenticated && _applicationRoleManager.FindUserPermissions(long.Parse(User.Identity.GetUserId())).Any(x => x == "Client"))
                return RedirectToAction(MVC.Client.Home.Index());

            var Router = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (Router == null) return HttpNotFound();
            ViewBag.user = _applicationUserManager.FindUserById(Router.Id);


            var Permissions = _applicationUserManager.FindClientPermissions(Router.Id);
            var CanShowPanel = Permissions.Any(x => x == AssignablePermissionToClient.ClientArea);
            if (!CanShowPanel) return HttpNotFound();

            ViewBag.RouterName = RouterCode;
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{lang}/userman/{RouterCode}")]
        public virtual async Task<ActionResult> Client(Netotik.ViewModels.Identity.UserClient.LoginModel model, string ReturnUrl, string RouterCode)
        {
            ViewBag.RouterName = RouterCode;
            ViewBag.ReturnUrl = ReturnUrl;
            var Router = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (Router == null) return HttpNotFound();
            ViewBag.user = _applicationUserManager.FindUserById(Router.Id);

            if (!ModelState.IsValid)
            {
                ViewBag.Message = Captions.UsernameOrPasswordWrong;
                return View(model);
            }
            var Permissions = _applicationUserManager.FindClientPermissions(Router.Id);
            var CanShowPanel = Permissions.Any(x => x == AssignablePermissionToClient.ClientArea);
            if (!CanShowPanel) return HttpNotFound();

            if (!_mikrotikServices.IP_Port_Check(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                ViewBag.Message = Captions.IPPORTClientError;
                return View();
            }
            if (!_mikrotikServices.User_Pass_Check(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                ViewBag.Message = Captions.UserPasswordClientError;
                return View();
            }
            if (!_mikrotikServices.Usermanager_IsInstall(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password))
            {
                ViewBag.Message = Captions.UsermanagerClientError;
                return View();
            }

            var clientUsers = _mikrotikServices.Usermanager_GetUser(Router.UserRouter.R_Host, Router.UserRouter.R_Port, Router.UserRouter.R_User, Router.UserRouter.R_Password, model.UserName);
            var user = clientUsers.FirstOrDefault(x => x.username == model.UserName && x.password == model.Password);

            if (user == null)
            {
                ViewBag.Message = Captions.UsernameOrPasswordWrong;
                return View(model);
            }


            var loginedUser = new User()
            {
                Id = Router.Id,
                UserName = user.id,
                Email = user.email,
                UserType = UserType.Client,
                UserRouter = new UserRouter()
                {
                    RouterCode = Router.UserRouter.RouterCode,
                    Expire_Date = Router.UserRouter.Expire_Date,
                    Id = Router.UserRouter.Id,
                    SmsActive = Router.UserRouter.SmsActive,
                    SmsUserAfterChangePackage = Router.UserRouter.SmsUserAfterChangePackage,
                    SmsUserhangeUserPassword = Router.UserRouter.SmsUserhangeUserPassword,
                    R_Host = Router.UserRouter.R_Host,
                    R_Password = Router.UserRouter.R_Password,
                    R_Port = Router.UserRouter.R_Port,
                    R_User = Router.UserRouter.R_User,
                    Userman_Customer = Router.UserRouter.Userman_Customer,
                    ClientPermissions = Router.UserRouter.ClientPermissions
                }
            };

            Session["Client"] = loginedUser;

            return RedirectToAction(MVC.Client.Home.Index());
        }
    }
}