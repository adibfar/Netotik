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
//Test Comment
namespace Netotik.Web.Controllers
{
    [Authorize]
    public partial class LoginController : BaseController
    {
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUnitOfWork _uow;
        private readonly IMenuService _menuService;

        public LoginController(
            IApplicationRoleManager applicationRoleManager,
            IApplicationSignInManager applicationSignInManager,
            IApplicationUserManager applicationUserManager,
            IMenuService menuService,
            IUnitOfWork uow)
        {
            _applicationRoleManager = applicationRoleManager;
            _applicationSignInManager = applicationSignInManager;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
            _menuService = menuService;
        }

        [AllowAnonymous]
        [Route("{lang}/Net/{ResellerCode}")]
        public virtual async Task<ActionResult> Company(string ReturnUrl, string ResellerCode)
        {
            if (User.Identity.IsAuthenticated && _applicationRoleManager.FindUserPermissions(long.Parse(User.Identity.GetUserId())).Any(x => x == "Company"))
                return RedirectToAction(MVC.Company.Home.Index());

            var reseller = await _applicationUserManager.FindByResellerCodeAsync(ResellerCode);
            if (reseller == null) return HttpNotFound();
            
            ViewBag.CompanyName = ResellerCode;
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{lang}/Net/{ResellerCode}")]
        public virtual async Task<ActionResult> Company(Netotik.ViewModels.Identity.UserCompany.LoginModel model, string ReturnUrl, string ResellerCode)
        {
            ViewBag.CompanyName = ResellerCode;
            ViewBag.ReturnUrl = ReturnUrl;

            if (!ModelState.IsValid)
            {
                ViewBag.Message = Captions.UsernameOrPasswordWrong;
                return View(model);
            }

            var reseller = await _applicationUserManager.FindByResellerCodeAsync(ResellerCode);
            if (reseller == null) return HttpNotFound();

            var loggedinUser = await _applicationUserManager.FindAsync(model.UserName, model.Password);
            if (loggedinUser == null || loggedinUser.IsDeleted)
            {
                ViewBag.Message = Captions.UsernameOrPasswordWrong;
                return View(model);
            }

            if (loggedinUser.UserType == Domain.Entity.UserType.UserCompany)
            {
                ViewBag.Message = Captions.UsernameOrPasswordWrong;
                return View(model);
            }


            /*
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
            */
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


        [AllowAnonymous]
        public virtual ActionResult Reseller(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated && _applicationRoleManager.FindUserPermissions(long.Parse(User.Identity.GetUserId())).Any(x => x == "Reseller"))
                return RedirectToAction(MVC.Reseller.Home.Index());

            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
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


            if (loggedinUser.UserType == Domain.Entity.UserType.UserReseller)
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
                    return RedirectToLocal(ReturnUrl);
                case SignInStatus.LockedOut:
                    ModelState.AddModelError("UserName",
                        $"دقیقه دوباره امتحان کنید {_applicationUserManager.DefaultAccountLockoutTimeSpan} حساب شما قفل شد ! لطفا بعد از ");
                    return View(model);
                case SignInStatus.Failure:
                    ModelState.AddModelError("UserName", Captions.UsernameOrPasswordWrong);
                    ModelState.AddModelError("Password", Captions.UsernameOrPasswordWrong);
                    return View(model);
                default:
                    ModelState.AddModelError("UserName",
                        "در این لحظه امکان ورود به  سابت وجود ندارد . مراتب را با مسئولان سایت در میان بگذارید"
                       );
                    return View(model);
            }
        }
    }
}