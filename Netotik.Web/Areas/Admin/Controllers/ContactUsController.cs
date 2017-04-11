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

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContactUs)]
    [BreadCrumb(Title = "پیام های تماس باما", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ContactUsController : BaseController
    {

        #region ctor
        private readonly IInboxMessageService _inboxMessageService;
        private readonly IUnitOfWork _uow;
        public ContactUsController(IInboxMessageService inboxMessageService, IUnitOfWork uow)
        {
            _uow = uow;
            _inboxMessageService = inboxMessageService;
        }
        #endregion

        #region Index
        public virtual ActionResult Index(string Search = "", int Page = 1, int PageSize = 10)
        {

            var pageList = _inboxMessageService.All()
                .Where(x => x.Name.Contains(Search) || x.Email.Contains(Search))
                .OrderByDescending(x => x.CreateDate)
                .ToPagedList(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.ContactUs.Views._Table, pageList);
            else
                return View(MVC.Admin.ContactUs.ActionNames.Index, pageList);
        }
        #endregion

        #region Detail

        public virtual async Task<ActionResult> Detail(int id)
        {
            var message = _inboxMessageService.SingleOrDefault(id);
            if (message == null)
                return RedirectToAction("Index");

            message.IsRead = true;
            await _uow.SaveChangesAsync();

            return View(message);
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

            return RedirectToAction(MVC.ContactUs.ActionNames.Index);
        }

        #endregion


    }
}