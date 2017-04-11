using Netotik.ViewModels.Common.Setting;
using Netotik.Services.Abstract;
using Netotik.Web.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netotik.Web.Controllers
{
    public partial class MetaTagController : Controller
    {
        private readonly ISettingService _settingService;
        public MetaTagController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public virtual ActionResult Index(string title, string keywords, string description)
        {
            GeneralSettingModel siteConfig = WebCache.GetSiteConfig(HttpContext, _settingService);

            ViewBag.Title = !string.IsNullOrWhiteSpace(title)
                ? string.Format("{0} | {1}", title, siteConfig.SiteName)
                : siteConfig.SiteName;

            ViewBag.Keywords = keywords;
            ViewBag.Description = description;

            return PartialView();
        }
	}
}