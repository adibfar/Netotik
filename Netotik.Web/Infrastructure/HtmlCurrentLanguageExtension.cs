using Netotik.Domain.Entity;
using Netotik.IocConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netotik.Services.Abstract;

namespace Netotik.Web.Infrastructure
{
    public static class HtmlCurrentLanguageExtension
    {
        public static Language CurrentLanguage(this HtmlHelper helper, HttpContextBase context, string lang)
        {

            var langs = Caching.LanguageCache.GetLanguages(context);

            return langs.FirstOrDefault(x => x.UniqueSeoCode == lang);
        }

    }
}