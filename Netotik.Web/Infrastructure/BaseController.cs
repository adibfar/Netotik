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
using Netotik.Common.Extensions;

namespace Netotik.Web.Infrastructure
{

    public class BaseController : System.Web.Mvc.Controller
    {

        public BaseController()
        {

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
            //var vFileName = Guid.NewGuid() + Path.GetExtension(image.FileName).ToLower();
            var vFileName = image.FileName.GetFileName(path);
            var vFolderPath = Server.MapPath(path);

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

        public string GetMyIp()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

    }
}