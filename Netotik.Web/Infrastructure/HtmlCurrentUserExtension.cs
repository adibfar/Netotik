using Netotik.Domain.Entity;
using Netotik.IocConfig;
using Netotik.Services.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netotik.Web.Infrastructure
{
    public static class HtmlCurrentUserExtension
    {
        public static User CurrentUser(this HtmlHelper helper)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return ProjectObjectFactory
                    .Container
                    .GetInstance<IApplicationUserManager>()
                    .FindUserById(long.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId()));
            }
            return null;
        }


        public static IList<string> CurrentUserPermissions(this HtmlHelper helper)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var roleManager = ProjectObjectFactory
                    .Container
                    .GetInstance<IApplicationRoleManager>();

                return roleManager.FindUserPermissions(long.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId()));
            }
            return null;
        }

    }
}