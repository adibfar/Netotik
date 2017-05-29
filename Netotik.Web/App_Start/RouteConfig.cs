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


            routes.MapRoute(
            name: "sitemap.xml",
            url: "sitemap.xml",
            defaults: new { controller = "Sitemap", action = "Index" },
            namespaces: new[] { "Netotik.Web.Controllers" }
            );

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
              name: "Default",
              url: "{lang}/{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "fa" },
              namespaces: new[] { "Netotik.Web.Controllers" }
          );
        }
    }
}
