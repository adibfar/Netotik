using Netotik.Web.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Netotik.Web.Infrastructure
{

    public class InternationalizationAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var languages = LanguageCache.GetLanguages(filterContext.HttpContext);

            if (filterContext.RequestContext.RouteData.Values["lang"] != null && filterContext.RequestContext.RouteData.Values["lang"] as string != "null")
            {
                var lang = (string)filterContext.RequestContext.RouteData.Values["lang"];
                string culture = languages.FirstOrDefault(x => x.IsDefault).LanguageCulture;
                filterContext.RequestContext.RouteData.Values["lang"] = languages.FirstOrDefault(x => x.IsDefault).UniqueSeoCode;
                if (lang != null)
                {
                    var language = languages.FirstOrDefault(x => x.Published && x.UniqueSeoCode == lang);
                    if (language != null)
                    {
                        culture = language.LanguageCulture;
                        filterContext.RequestContext.RouteData.Values["lang"] = language.UniqueSeoCode;
                    }
                }
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }

        }
    }
}