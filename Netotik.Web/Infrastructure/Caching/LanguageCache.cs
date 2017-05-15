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
        public const string Key = "LanguageConfig";

        public static IList<Language> GetLanguages(HttpContextBase httpContext, ILanguageService languageService)
        {
            var siteConfig = httpContext.CacheRead<List<Language>>(Key);

            if (siteConfig == null)
            {
                siteConfig = languageService.All().ToList();
                httpContext.CacheInsert(Key, siteConfig, 360);
            }
            return siteConfig;
        }

        public static void RemoveLanguageCache(HttpContextBase httpContext)
        {
            httpContext.InvalidateCache(Key);
        }
    }
}