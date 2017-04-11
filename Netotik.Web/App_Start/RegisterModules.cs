
using System.Web;
using Netotik.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Netotik.Web.HtmlCleaner;
using Netotik.Web.Infrastructure.HttpModules;

[assembly: PreApplicationStartMethod(typeof(RegisterModules), "Start")]
namespace Netotik.Web
{
    public class RegisterModules
    {
        public static void Start()
        {
           // DynamicModuleUtility.RegisterModule(typeof(DosAttackModule));
            //DynamicModuleUtility.RegisterModule(typeof(AntiXssModule));
        }
    }
}