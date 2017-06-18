using System.Web;
using System.Web.Mvc;
using Netotik.ViewModels.Common.Setting;

namespace Netotik.Web.Infrastructure
{
    public static class HtmlSettingExtension
    {
        public static GeneralSettingModel Setting(this HtmlHelper helper, HttpContextBase context)
        {

            return Caching.PublicUICache.GetSiteConfig(context);
        }

    }
}