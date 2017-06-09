using Netotik.Services.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.Owin.Security;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using System.Web;
using Netotik.Web.Infrastructure;
using Netotik.Web.Infrastructure.Caching;

namespace Netotik.Web.Controllers
{
    public partial class HomeController : BaseController
    {


        #region Fields
        private const int _oneDay = 86400;
        private const int _hour12 = 43200;
        private const int _hour1 = 3600;
        private const int _min30 = 1800;
        private const int _min15 = 900;
        private const int _min10 = 600;

        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUserMailer _userMailer;
        private readonly ISettingService _settingService;
        private readonly IContentCategoryService _contentCategoryService;
        private readonly ICategoryService _categoryService;
        private readonly ILanguageTranslationService _linkService;
        private readonly IContentService _contentService;
        private readonly IMenuService _menuService;
        private readonly ISliderService _sliderService;
        private readonly IIndexSectionService _indexSectionService;
        #endregion

        #region Constructor
        public HomeController(
            ISliderService sliderService,
            IIndexSectionService indexSectionService,
            IApplicationRoleManager applicationRoleManager,
            IAuthenticationManager authenticationManager,
            ILanguageTranslationService linkService,
            IUserMailer userMailer,
            ISettingService settingService,
            IContentCategoryService contentCategoryService,
            ICategoryService categoryService,
            IContentService contentService,
            IMenuService menuService
            )
        {
            _indexSectionService = indexSectionService;
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

        public virtual PartialViewResult LastBlog()
        {
            return PartialView(MVC.Home.Views._LastBlog, _contentService.GetLastContents(12, LanguageCache.GetLanguage(HttpContext).Id));
        }

        //[OutputCache(Duration = hour12, VaryByParam = "none")]
        public virtual PartialViewResult Slider()
        {
            return PartialView(MVC.Home.Views._Slider, _sliderService.GetAll(LanguageCache.GetLanguage(HttpContext).Id));
        }

        public virtual PartialViewResult Section()
        {
            return PartialView(MVC.Home.Views._Section, _indexSectionService.GetAll(LanguageCache.GetLanguage(HttpContext).Id));
        }


        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual PartialViewResult Footer()
        {
            return PartialView(MVC.SharedPublic.Views._Footer, _menuService.GetAllFooterMenu(LanguageCache.GetLanguage(this.HttpContext).Id));
        }

        //[OutputCache(Duration = _min10, VaryByParam = "none")]
        public virtual PartialViewResult LanguageSelector()
        {
            return PartialView(MVC.SharedPublic.Views._LanagaugeSelector, LanguageCache.GetLanguages(this.HttpContext));
        }


        public virtual PartialViewResult HeaderPublicCss()
        {
            return PartialView(MVC.SharedPublic.Views._HeaderPublicCss, LanguageCache.GetLanguages(this.HttpContext));
        }

        public virtual PartialViewResult HeaderPanelCss()
        {
            return PartialView(MVC.Shared.Views._HeaderPanelCss, LanguageCache.GetLanguages(this.HttpContext));
        }

        public virtual PartialViewResult PanelLanguageSelector()
        {
            return PartialView(MVC.Shared.Views._LanagaugeSelector, LanguageCache.GetLanguages(this.HttpContext));
        }

        
        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual PartialViewResult Header()
        {
            return PartialView(MVC.SharedPublic.Views._Header);
        }

        //[OutputCache(Duration = hour12, VaryByParam = "none")]
        public virtual PartialViewResult HeaderMenu()
        {
            return PartialView(MVC.SharedPublic.Views._HeaderMenu, _menuService.GetAllHeaderMenu(LanguageCache.GetLanguage(this.HttpContext).Id));
        }
    }
}