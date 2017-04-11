using System.Web.Mvc;

namespace Netotik.Web.Areas.Reseller
{
    public class ResellerRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Reseller";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Reseller_default",
                "Reseller/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}