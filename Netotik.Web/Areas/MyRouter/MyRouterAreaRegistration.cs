using System.Web.Mvc;

namespace Netotik.Web.Areas.MyRouter
{
    public class MyRouterRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MyRouter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Router_default",
                "{lang}/MyRouter/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, lang = "fa" }
            );
        }
    }
}