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
using Netotik.IocConfig;

namespace Netotik.Web.Infrastructure.Caching
{
    public class LanguageCache
    {
        public const string KeyLanguages = "LanguagesConfig";

        public static IList<Language> GetLanguages(HttpContextBase httpContext)
        {
            var languages = httpContext.CacheRead<List<Language>>(KeyLanguages);

            if (languages == null)
            {
                var languageService = ProjectObjectFactory.Container.GetInstance<ILanguageService>();
                languages = languageService.All().Where(x => x.Published).OrderBy(x => x.DisplayOrder).ToList();
                httpContext.CacheInsert(KeyLanguages, languages, 360);
            }
            return languages;
        }

        public static Language GetLanguage(HttpContextBase httpContext)
        {
            var language = GetLanguages(httpContext).FirstOrDefault(x => x.UniqueSeoCode==httpContext.Request.RequestContext.RouteData.Values["lang"].ToString());
            return language;
        }

        public static void RemoveLanguageCache(HttpContextBase httpContext)
        {
            httpContext.InvalidateCache(KeyLanguages);
        }
    }
}