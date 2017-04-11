using System;
using System.Configuration;
using System.Web;
using Netotik.Services.Abstract;
using Netotik.ViewModels.Common.Setting;
using Netotik.Common.Caching;
using System.Collections.Generic;
using Netotik.Services.Identity;

namespace Netotik.Web.Caching
{
    public class WebCache
    {
        public const string SiteConfigKey = "SiteConfig";
        public const string UserRolesKey = "UserRoles";


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


        public static IList<string> GetUserRoles(HttpContextBase httpContext, IApplicationRoleManager roleManager, long UserId)
        {
            var Roles = httpContext.CacheRead<IList<string>>(UserRolesKey);

            if (Roles == null)
            {
                Roles = roleManager.FindUserPermissions(UserId);
                httpContext.CacheInsert(SiteConfigKey, Roles, 10);
            }
            return Roles;
        }

        public static void RemoveUserRoles(HttpContextBase httpContext)
        {
            httpContext.InvalidateCache(UserRolesKey);
        }
    }
}