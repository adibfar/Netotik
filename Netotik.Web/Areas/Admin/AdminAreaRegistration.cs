using System.Web.Mvc;

namespace Netotik.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "{lang}/Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, lang = "fa" },
                new[] { "Netotik.Web.Areas.Admin.Controllers" }
            );
        }
    }
}