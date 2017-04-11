using Netotik.Common;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Netotik.Web.Infrastructure
{

    public class BaseController : System.Web.Mvc.Controller
    {





        [ChildActionOnly]
        public virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index","Home");
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