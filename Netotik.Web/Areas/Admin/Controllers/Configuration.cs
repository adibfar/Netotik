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
    [BreadCrumb(Title = "PublicConfig", UseDefaultRouteUrl = true,Order = 0, GlyphIcon = "icon-cogs2")]
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
                Netotik.Web.Infrastructure.Caching.PublicUICache.RemoveSiteConfig(HttpContext);
                this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSettingSuccess);
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return View();
            }

            return View();
        }
    }

}
