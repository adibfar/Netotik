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
        #endregion

        #region Constructor
        public HomeController(
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

        public virtual ActionResult Head()
        {
            return PartialView(MVC.Shared.Views._Head);
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



        public virtual ActionResult AutoComplete(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                Content(null);

            var result = new StringBuilder();
            var items = LuceneIndex.Search(q, "Name", "Description").Take(5).ToList();

            foreach (var item in items)
            {
                var prodUrl = this.Url.Action(MVC.Product.ActionNames.Show, MVC.Product.Name, new { id = item.Id, name = item.Name.GenerateSlug() });
                result.AppendLine(item.Name + "|" + prodUrl + "|" + item.ImageName + "|" + item.Description);
            }
            return Content(result.ToString());
        }

    }
}