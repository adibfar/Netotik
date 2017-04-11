using Netotik.Data;
using Netotik.Services.Abstract;
using Netotik.Web.Infrastructure;
using Netotik.Web.Infrastructure.Filters;
using Netotik.Common.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Netotik.Resources;
using Netotik.Domain.Entity;
using Netotik.Common.Security;
using Netotik.ViewModels;
using Netotik.Services.Enums;
using Netotik.Common;
using CaptchaMvc.Attributes;
using System.Data.Entity;
using Netotik.ViewModels.Identity.UserReseller;

namespace Netotik.Web.Areas.Reseller.Controllers
{
    [Mvc5Authorize(Roles = "Reseller")]
    public partial class HomeController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMenuService _menuService;

        public HomeController(
            IUnitOfWork uow, IMenuService menuService)
        {
            _menuService = menuService;
            _uow = uow;
        }
        public virtual ActionResult Index()
        {
            //return View(_ResellerService.GetProfile(User.UserId));
            return View();
        }

        public virtual ActionResult Profile()
        {
            //return View(_ResellerService.GetProfile(User.UserId));
            return View();
        }

        [HttpPost]
        public virtual ActionResult Profile(ProfileModel model)
        {
            //if (true)
            //{
            //    SetResultMessage(_ResellerService.UpdateProfile(model, User.UserId));
            //}
            //return RedirectToAction(MVC.Reseller.Home.Profile());

            return View();
        }

        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordModel model)
        {

            //if (ModelState.IsValid)
            //{
            //    SetResultMessage(_ResellerService.ChangePassword(model, User.UserId));
            //}
            //return RedirectToAction(MVC.Reseller.Home.Index());

            return View();
        }
        
    }
}