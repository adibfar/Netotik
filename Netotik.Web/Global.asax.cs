using Netotik.Data.Context;
using Netotik.Data.Migrations;
using Netotik.Web.Infrastructure.Binders;
using Netotik.Common.Filters;
using CaptchaMvc.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Netotik.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // change captcha provider for using cookie
          //  CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();


            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());


            ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ApplicationStart.Config();



            var resourceService = (Netotik.Services.Abstract.IResourceService)IocConfig.ProjectObjectFactory.Container.GetInstance(typeof(Netotik.Services.Abstract.IResourceService));
            resourceService.SeedDataBase(System.IO.File.ReadAllText(Server.MapPath("/App_Data/DefaultResources.xml")), "en-us");

        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var cryptoEx = error as CryptographicException;
            if (cryptoEx != null)
            {
                FormsAuthentication.SignOut();
                Server.ClearError();
            }
        }

    }
}
