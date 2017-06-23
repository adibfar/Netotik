using System.Web.Mvc;

namespace Netotik.Web.Areas.Client
{
    public class ClientAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Client";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User_default",
                "{lang}/Client/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, lang = "fa" }
            );
        }
    }
}