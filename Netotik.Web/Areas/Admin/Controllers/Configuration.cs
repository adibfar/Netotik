using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MvcPaging;
using Netotik.Common.Filters;
using Netotik.Data;
using Netotik.Services.Abstract;
using Netotik.Web.Infrastructure;
using Netotik.Common.Controller;
using Netotik.Resources;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Common.Setting;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessPublicSetting)]
    [BreadCrumb(Title = "تنظیمات سایت", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-cog")]
    public partial class ConfigurationController : BaseController
    {
        private readonly ISettingService _settingService;
        private readonly IUnitOfWork _uow;

        public ConfigurationController(
            ISettingService settingService,
            IUnitOfWork uow)
        {
            _settingService = settingService;
            _uow = uow;
        }

        public virtual ActionResult Index()
        {
            return View(_settingService.GetAll());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Index(GeneralSettingModel model)
        {
            try
            {
                _settingService.Update(model);
                _uow.SaveChanges();
                Netotik.Web.Caching.WebCache.RemoveSiteConfig(HttpContext);
                this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSettingSuccess);
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            return View();
        }
    }

}
