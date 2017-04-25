using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MvcPaging;
using Netotik.Common.Filters;
using Netotik.Web.Infrastructure;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.Common;
using Netotik.Resources;
using Netotik.Domain.Entity;
using Netotik.Common.Security;
using Netotik.Web.Infrastructure.Filters;
using System.Web.UI;
using System.Threading.Tasks;
using Netotik.Web;
using System.Data.Entity.Validation;
using Netotik.Web.Extension;
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContactUs)]
    [BreadCrumb(Title = "پیام های تماس باما", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ContactUsController : BaseController
    {

        #region ctor
        private readonly IInboxContactUsMessageService _inboxMessageService;
        private readonly IUnitOfWork _uow;
        public ContactUsController(IInboxContactUsMessageService inboxMessageService, IUnitOfWork uow)
        {
            _uow = uow;
            _inboxMessageService = inboxMessageService;
        }
        #endregion

        #region Index
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _inboxMessageService.GetList(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detail

        public virtual async Task<ActionResult> Show(int id)
        {
            var message = _inboxMessageService.SingleOrDefault(id);
            if (message == null)
                return RedirectToAction("Index");

            message.IsRead = true;
            await _uow.SaveChangesAsync();

            return PartialView(MVC.Admin.ContactUs.Views._Show,message);
        }

        #endregion

        #region Delete

        [HttpPost]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var message = _inboxMessageService.SingleOrDefault(id);
            if (message == null)
                return RedirectToAction("Index");

            _inboxMessageService.Remove(message);
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.ContactUs.Index());
        }

        #endregion
        
    }
}