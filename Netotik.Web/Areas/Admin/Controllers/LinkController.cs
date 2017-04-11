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
using Netotik.ViewModels.CMS.Link;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessLinks)]
    [BreadCrumb(Title = "لیست پیوندها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class LinkController : BaseController
    {

        #region ctor
        private readonly ILinkService _linkService;
        private readonly IUnitOfWork _uow;

        public LinkController(
            ILinkService linkService,
            IUnitOfWork uow)
        {
            _linkService = linkService;
            _uow = uow;
        }
        #endregion

        #region Index
        public virtual ActionResult Index()
        {

            var list = _linkService.All().OrderBy(x => x.Order).ToList();
            return View(MVC.Admin.Link.ActionNames.Index, list);

        }
        #endregion


        [BreadCrumb(Title = "پیوند جدید", Order = 1)]
        public virtual ActionResult Create()
        {

            return View(MVC.Admin.Link.ActionNames.Create, new LinkModel { Order = 0 });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(LinkModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            #region Initial Content
            var link = new Netotik.Domain.Entity.Link()
            {
                Url = model.Url,
                Order = model.Order,
                Text = model.Text
            };

            #endregion

            _linkService.Add(link);


            #region SaveChanges
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            this.MessageSuccess(Messages.MissionFail, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.Link.Index());

        }
        #endregion

        #region Edit
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var link = _linkService.SingleOrDefault(id);
            if (link != null)
            {
                _linkService.Remove(link);
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [BreadCrumb(Title = "ویرایش پیوند", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = await _linkService.All().FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return RedirectToAction("Index");


            var editModel = new LinkModel
            {
                Id = model.Id,
                Text = model.Text,
                Url = model.Url,
                Order = model.Order,
            };

            return View(MVC.Admin.Link.ActionNames.Edit, editModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(LinkModel model, ActionType actionType)
        {
            var link = _linkService.SingleOrDefault(model.Id);
            if (link == null)
                return RedirectToAction(MVC.Admin.Link.Index());


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            #region Update

            link.Order = model.Order;
            link.Text = model.Text;
            link.Url = model.Url;

            #endregion

            _linkService.Update(link);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return View();
            }

            this.MessageSuccess(Messages.MissionFail, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Link.Index());
        }

        #endregion

    }
}