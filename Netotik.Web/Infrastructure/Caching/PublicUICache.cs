using System;
using System.Configuration;
using System.Web;
using Netotik.Services.Abstract;
using Netotik.ViewModels.Common.Setting;
using Netotik.Common.Caching;
using System.Collections.Generic;
using Netotik.Services.Identity;

namespace Netotik.Web.Infrastructure.Caching
{
    public class PublicUICache
    {
        public const string SiteConfigKey = "SiteConfig";

        public static GeneralSettingModel GetSiteConfig(HttpContextBase httpContext, ISettingService optionService)
        {
            var siteConfig = httpContext.CacheRead<GeneralSettingModel>(SiteConfigKey);

            if (siteConfig == null)
            {
                siteConfig = optionService.GetAll();
                httpContext.CacheInsert(SiteConfigKey, siteConfig, 360);
            }
            return siteConfig;
        }

        public static void RemoveSiteConfig(HttpContextBase httpContext)
        {
            httpContext.InvalidateCache(SiteConfigKey);
        }
    }
}