using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Netotik.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}",
            new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.LowercaseUrls = true;

            routes.MapRoute(
               "account",
               "account/login",
               new { lang = "fa",controller = "Account", action = "Login" , ReturnUrl = UrlParameter.Optional }
           );


            routes.MapRoute(
               "error",
               "error/error",
               new { lang = "fa", controller = "Error", action = "Error", ReturnUrl = UrlParameter.Optional }
           );
            routes.MapRoute(
               "badrequest",
               "error/badrequest",
               new { lang = "fa", controller = "Error", action = "BadRequest", ReturnUrl = UrlParameter.Optional }
           );
            routes.MapRoute(
               "notfound",
               "error/notfound",
               new { lang = "fa", controller = "Error", action = "NotFound", ReturnUrl = UrlParameter.Optional }
           );
            routes.MapRoute(
               "forbidden",
               "error/forbidden",
               new { lang = "fa", controller = "Error", action = "Forbidden", ReturnUrl = UrlParameter.Optional }
           );
            routes.MapRoute(
               "urltoolong",
               "error/urltoolong",
               new { lang = "fa", controller = "Error", action = "UrlTooLong", ReturnUrl = UrlParameter.Optional }
           );
            routes.MapRoute(
               "serviceunavailable",
               "error/serviceunavailable",
               new { lang = "fa", controller = "Error", action = "ServiceUnavailable", ReturnUrl = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "sitemap.xml",
            url: "sitemap.xml",
            defaults: new { controller = "Sitemap", action = "Index" },
            namespaces: new[] { "Netotik.Web.Controllers" }
            );

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
              name: "multiLang",
              url: "{lang}/{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "Netotik.Web.Controllers" }
          );

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "fa" },
              namespaces: new[] { "Netotik.Web.Controllers" }
          );




        }
    }
}
