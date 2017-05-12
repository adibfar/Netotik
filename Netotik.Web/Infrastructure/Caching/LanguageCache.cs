using System;
using System.Configuration;
using System.Web;
using Netotik.Services.Abstract;
using Netotik.ViewModels.Common.Setting;
using Netotik.Common.Caching;
using System.Collections.Generic;
using Netotik.Services.Identity;
using Netotik.Domain.Entity;
using System.Linq;

namespace Netotik.Web.Infrastructure.Caching
{
    public class LanguageCache
    {
        public const string SiteConfigKey = "LanguageConfig";

        public static IList<Language> GetLanguages(HttpContextBase httpContext, ILanguageService languageService)
        {
            var siteConfig = httpContext.CacheRead<List<Language>>(SiteConfigKey);

            if (siteConfig == null)
            {
                siteConfig = languageService.All().ToList();
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