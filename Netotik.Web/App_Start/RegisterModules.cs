
using System.Web;
using Netotik.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

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