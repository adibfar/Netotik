using System.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Netotik.Web.Infrastructure.Caching;
using Netotik.IocConfig;
using Netotik.Services.Abstract;

namespace Netotik.Web.Infrastructure
{

    public class BaseController : System.Web.Mvc.Controller
    {

        public BaseController()
        {

        }

        protected override void Initialize(RequestContext requestContext)
        {
            var languages = LanguageCache.GetLanguages(requestContext.HttpContext);

            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                var lang = (string)requestContext.RouteData.Values["lang"];
                string culture = languages.FirstOrDefault(x => x.IsDefault).LanguageCulture;
                requestContext.RouteData.Values["lang"] = languages.FirstOrDefault(x => x.IsDefault).UniqueSeoCode;
                if (lang != null)
                {
                    var language = languages.FirstOrDefault(x => x.Published && x.UniqueSeoCode == lang);
                    if (language != null)
                    {
                        culture = language.LanguageCulture;
                        requestContext.RouteData.Values["lang"] = language.UniqueSeoCode;
                    }
                }

                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }
            base.Initialize(requestContext);
        }




        [ChildActionOnly]
        public virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public string SaveFile(HttpPostedFileBase image, string path)
        {
            var vFileName = Guid.NewGuid() + Path.GetExtension(image.FileName).ToLower();
            var vFolderPath = Server.MapPath(path);

            if (!Directory.Exists(vFolderPath)) Directory.CreateDirectory(vFolderPath);

            var vFilePath = Path.Combine(vFolderPath, vFileName);
            image.SaveAs(vFilePath);
            var vImagePath = Url.Content(path + vFileName);

            return vFileName;
        }

        public string SaveFile(HttpPostedFileBase image, string path, string fileName)
        {
            var vFileName = fileName + Path.GetExtension(image.FileName).ToLower();
            var vFolderPath = Server.MapPath(path);
            if (System.IO.File.Exists(Path.Combine(vFolderPath, vFileName)))
            {
                vFileName = new Random().Next(9999, 999999).ToString() + fileName + Path.GetExtension(image.FileName).ToLower();
            }



            if (!Directory.Exists(vFolderPath)) Directory.CreateDirectory(vFolderPath);

            var vFilePath = Path.Combine(vFolderPath, vFileName);
            image.SaveAs(vFilePath);
            var vImagePath = Url.Content(path + vFileName);

            return vFileName;
        }


        public void DeleteFile(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
            catch { }
        }

    }
}