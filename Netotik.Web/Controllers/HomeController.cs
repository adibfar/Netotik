using Netotik.Services.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Netotik.Web.Extension;
using System.Text;
using Netotik.Web.Lucene;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using System.Web;

namespace Netotik.Web.Controllers
{
    public partial class HomeController : Controller
    {


        #region Fields
        private const int oneDay = 86400;
        private const int hour12 = 43200;
        private const int hour1 = 3600;
        private const int min30 = 1800;
        private const int min15 = 900;
        private const int min10 = 600;

        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUserMailer _userMailer;
        private readonly ISettingService _settingService;
        private readonly IContentCategoryService _contentCategoryService;
        private readonly ICategoryService _categoryService;
        private readonly ILinkService _linkService;
        private readonly IContentService _contentService;
        private readonly IMenuService _menuService;
        private readonly ISliderService _sliderService;
        #endregion

        #region Constructor
        public HomeController(
            ISliderService sliderService,
            IApplicationRoleManager applicationRoleManager,
            IAuthenticationManager authenticationManager,
            ILinkService linkService,
            IUserMailer userMailer,
            ISettingService settingService,
            IContentCategoryService contentCategoryService,
            ICategoryService categoryService,
            IContentService contentService,
            IMenuService menuService)
        {
            _sliderService = sliderService;
            _applicationRoleManager = applicationRoleManager;
            _authenticationManager = authenticationManager;
            _userMailer = userMailer;
            _settingService = settingService;
            _contentCategoryService = contentCategoryService;
            _categoryService = categoryService;
            _linkService = linkService;
            _menuService = menuService;
            _contentService = contentService;
        }
        #endregion



        // [ThrottleAttribute(Seconds = 1, Message = "لطفا دوباره امتحان کنید")]
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult AdminHeader()
        {
            return PartialView(MVC.Shared.Views._Header);
        }

        [Authorize]
        public virtual ActionResult AdminMenu()
        {
            var menues = _menuService.All().Where(x => x.IsActive).Include(x => x.SubMenues).ToList();
            if (User.IsInRole(Netotik.ViewModels.Identity.Security.AssignableToRolePermissions.CanViewAdminPanel))
            {
                return PartialView(MVC.Shared.Views._SideBarAdminMenu, menues);
            }
            else if (User.IsInRole("Reseller"))
            {
                return PartialView(MVC.Shared.Views._SideBarResellerMenu, menues);
            }
            else if (User.IsInRole("Company"))
            {
                return PartialView(MVC.Shared.Views._SideBarCompanyMenu, menues);
            }
            return View("");
        }

        //[OutputCache(Duration = min10, VaryByParam = "none")]
        public virtual PartialViewResult LastBlog()
        {
            return PartialView(MVC.Home.Views._LastBlog, _contentService.GetLastContents(6));
        }



        //[OutputCache(Duration = hour12, VaryByParam = "none")]
        public virtual PartialViewResult Slider()
        {
            var list = _sliderService.All().Where(x => x.IsActive)
                .OrderBy(x => x.Order)
                .Include(x => x.Picture)
                .ToList();
            return PartialView(MVC.Home.Views._Slider, list);
        }

        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual PartialViewResult Footer()
        {
            //ViewBag.setting = WebCache.GetSiteConfig(HttpContext, _settingService);
            return PartialView(MVC.SharedPublic.Views._Footer);
        }


        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual PartialViewResult FooterAddress()
        {
            return PartialView(MVC.SharedPublic.Views._FooterAddress);//, WebCache.GetSiteConfig(HttpContext, _settingService));
        }

        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual PartialViewResult FooterPopularPost()
        {
            return PartialView(MVC.SharedPublic.Views._FooterPopularPost, _contentService.GetLastPopular(4));
        }


        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual PartialViewResult Header()
        {
            return PartialView(MVC.SharedPublic.Views._Header);
        }

        //[OutputCache(Duration = hour12, VaryByParam = "none")]
        public virtual PartialViewResult HeaderMenu()
        {
            var list = _menuService.All()
                .Where(x => x.IsActive).Include(x => x.SubMenues).OrderBy(x => x.Order).ToList();

            return PartialView(MVC.SharedPublic.Views._HeaderMenu, list);
        }
        public virtual ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction(MVC.Home.Index());
        }

    }
}