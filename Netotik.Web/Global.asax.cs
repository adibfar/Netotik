using Netotik.Data.Context;
using Netotik.Data.Migrations;
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
using System.Globalization;
using System.Threading;
using Netotik.Common.Binders;

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
        protected void Application_BeginRequest()
        {
            if (!Request.IsLocal)
            {
                if (!Context.Request.IsSecureConnection)
                    Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:").ToLower());

            }

        }



    }
}
