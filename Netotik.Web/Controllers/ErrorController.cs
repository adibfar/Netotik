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
using Netotik.ViewModels.Common.Error;
using Netotik.Web.Infrastructure.Caching;
//Test Comment
namespace Netotik.Web.Controllers
{
    public partial class ErrorController : BaseController
    {
        private readonly ISettingService _settingService;
        private readonly IUnitOfWork _uow;

        public ErrorController(
            ISettingService settingService,
            IMenuService menuService,
            IUnitOfWork uow)
        {
            _settingService = settingService;
            _uow = uow;
        }

        public virtual ActionResult Error()
        {
            ViewBag.siteConfig = PublicUICache.GetSiteConfig(HttpContext, _settingService);
            ErrorInfoModel ErrorInfoModel = new ErrorInfoModel();
            ErrorInfoModel.Message = "An Error Has Occured";
            ErrorInfoModel.Description = "An unexpected error occured on our website. The website administrator has been notified.";
            return View(ErrorInfoModel);
        }
        public virtual ActionResult BadRequest()
        {
            ViewBag.siteConfig = PublicUICache.GetSiteConfig(HttpContext, _settingService);
            ErrorInfoModel ErrorInfoModel = new ErrorInfoModel();
            ErrorInfoModel.Message = "Bad Request";
            ErrorInfoModel.Description = "The request cannot be fulfilled due to bad syntax.";
            return View(ErrorInfoModel);
        }
        public virtual ActionResult NotFound()
        {
            ViewBag.siteConfig = PublicUICache.GetSiteConfig(HttpContext, _settingService);
            ErrorInfoModel ErrorInfoModel = new ErrorInfoModel();
            ErrorInfoModel.Message = "We are sorry, the page you requested cannot be found.";
            ErrorInfoModel.Description = "The URL may be misspelled or the page you're looking for is no longer available.";
            return View(ErrorInfoModel);
        }

        public virtual ActionResult Forbidden()
        {
            ViewBag.siteConfig = PublicUICache.GetSiteConfig(HttpContext, _settingService);
            ErrorInfoModel ErrorInfoModel = new ErrorInfoModel();
            ErrorInfoModel.Message = "403 Forbidden";
            ErrorInfoModel.Description = "Forbidden: You don't have permission to access [directory] on this server.";
            return View(ErrorInfoModel);
        }
        public virtual ActionResult UrlTooLong()
        {
            ViewBag.siteConfig = PublicUICache.GetSiteConfig(HttpContext, _settingService);
            ErrorInfoModel ErrorInfoModel = new ErrorInfoModel();
            ErrorInfoModel.Message = "URL Too Long";
            ErrorInfoModel.Description = "The requested URL is too large to process. That’s all we know.";
            return View(ErrorInfoModel);
        }
        public virtual ActionResult ServiceUnavailable()
        {
            ViewBag.siteConfig = PublicUICache.GetSiteConfig(HttpContext, _settingService);
            ErrorInfoModel ErrorInfoModel = new ErrorInfoModel();
            ErrorInfoModel.Message = "Service Unavailable";
            ErrorInfoModel.Description = "Our apologies for the temporary inconvenience. This is due to overloading or maintenance of the server.";
            return View(ErrorInfoModel);
        }

    }
}