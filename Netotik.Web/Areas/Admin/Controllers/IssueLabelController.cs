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
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Support.Issue;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessIssueLabel)]
    [BreadCrumb(Title = "لیست برچسب های وظایف", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class IssueLabelController : BaseController
    {


        #region ctor
        private readonly IIssueLabelService _issueLabelService;
        private readonly IIssueService _issueService;
        private readonly IUnitOfWork _uow;

        public IssueLabelController(
            IIssueLabelService issueLabelService,
            IIssueService issueService,
            IUnitOfWork uow)
        {
            _issueService = issueService;
            _issueLabelService = issueLabelService;
            _uow = uow;
        }
        #endregion


        #region Index
        public virtual ActionResult Index(string Search = "")
        {

            var pageList = _issueLabelService.All()
                .Where(x => x.Name.Contains(Search))
                .ToList();

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.IssueLabel.Views._Table, pageList);
            else
                return View(MVC.Admin.IssueLabel.ActionNames.Index, pageList);

        }
        #endregion


        #region Create
        [BreadCrumb(Title = "برچسب وظایف جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(IssueLabelModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }

            var label = new IssueLabel()
            {
                Name = model.Name,
                ColorCode = model.Color
            };

            _issueLabelService.Add(label);


            try
            {
                await _uow.SaveChangesAsync();
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.IssueLabel.Index());

        }
        #endregion


        #region Edit
        [HttpPost]
        [BreadCrumb(Title = "ویرایش برچسب وظایف", Order = 1)]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var label = new IssueLabel { Id = id };
            _issueLabelService.Remove(label);
            await _uow.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.IssueLabel.ActionNames.Index);
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _issueLabelService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.IssueLabel.ActionNames.Index);

            return View(new IssueLabelModel
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.ColorCode
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(IssueLabelModel model, ActionType actionType)
        {

            var label = _issueLabelService.SingleOrDefault(model.Id);
            if (label == null)
                return RedirectToAction(MVC.Admin.IssueLabel.ActionNames.Index);


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            label.Name = model.Name;
            label.ColorCode = model.Color;

            _issueLabelService.Update(label);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.IssueLabel.Index());
        }

        #endregion



        #region private

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsNameExist(string name, int? id)
        {
            return await _issueLabelService.IsExistByName(name, id) ? Json(false) : Json(true);
        }
        #endregion



    }
}