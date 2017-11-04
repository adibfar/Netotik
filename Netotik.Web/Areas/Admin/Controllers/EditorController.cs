using Netotik.Common.Filters;
using Netotik.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Netotik.Common.Extensions;
using Netotik.Common.Controller;
using System.Web.Mvc;
using System.Web;
using System.IO;
using Netotik.Resources;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize]
    public partial class EditorController : BasePanelController
    {
        [Route("admin/editor/upload")]
        [HttpPost]
        public virtual ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {

            string fileName = String.Empty;
            string resultMesssage = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    fileName = base.SaveFile(upload, FilePathes._imagesEditorPath);
                    resultMesssage = Captions.AddSuccess;
                }
            }
            catch
            {
                resultMesssage = Captions.AddError;
            }
            var vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + FilePathes._imagesEditorPath + "/" + fileName + "\", \"" + resultMesssage + "\");</script></body></html>";
            return Content(vOutput);

        }
    }
}