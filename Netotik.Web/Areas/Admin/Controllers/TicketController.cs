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
using System.Threading.Tasks;
using Netotik.Web;
using System.Data.Entity.Validation;

using System.IO;
using Microsoft.AspNet.Identity;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Ticket.Issue;
using Netotik.Services.Identity;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست تیکت ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
     Order = 0, GlyphIcon = "icon icon-table")]
    public partial class TicketController : BasePanelController
    {

        #region ctor

        private readonly ITicketService _issueService;
        private readonly ITicketTagService _issueLabelService;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUnitOfWork _uow;

        public TicketController(
            ITicketService issueService,
            ITicketTagService issueLabelService,
            IApplicationUserManager applicationUserManager,
            IApplicationRoleManager applicationRoleManager,
            IUnitOfWork uow)
        {
            _issueLabelService = issueLabelService;
            _issueService = issueService;
            _applicationUserManager = applicationUserManager;
            _applicationRoleManager = applicationRoleManager;
            _uow = uow;
        }
        #endregion

        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessTicket)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {
            var pageList = _issueService.GetContentTable(Search, 1, new string[] { "1" })
                .ToPagedList<TableIssueModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.Ticket.Views._Table, pageList);
            else
                return View(MVC.Admin.Ticket.ActionNames.Index, pageList);
        }
        #endregion

        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateTicket)]
        [BreadCrumb(Title = "کار جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            PopulateRoles();
            PopulateUserIssueUsers(1);
            PopulateLabels();

            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateTicket)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(IssueModel model)
        {
            if (model.RoleIds != null) PopulateRoles(model.RoleIds);
            else PopulateRoles();
            if (model.UserIds != null) PopulateUserIssueUsers(model.UserIds);
            else PopulateUserIssueUsers();
            if (model.LabelIds != null) PopulateLabels(model.LabelIds);
            else PopulateLabels();

            if (model.UserIds == null)
            {
                this.MessageError(Messages.MissionFail, "لطفا مسئول این کار را مشخص کنید.");
                return View();
            }

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            #region Initial Content
            var now = DateTime.Now;

            var issue = new Netotik.Domain.Entity.Ticket()
            {
                CreateDate = now,
                CreatedUserId = User.Identity.GetUserId<long>(),
                status = IssueStatus.open,
                Description = model.Description,
                Periority = model.Periority,
                Title = model.Title,
                LastResponseDate = now,
                LastResponseUserId = User.Identity.GetUserId<long>(),
                MessageCount = 0
            };

            issue.TicketTags = _issueLabelService.GetbyIds(model.LabelIds);

            #endregion

            _issueService.Add(issue);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            this.MessageError(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.Ticket.Index());
        }
        #endregion



        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessTicket)]
        public virtual ActionResult Show(int id = 0)
        {
            var issue = _issueService.SingleOrDefault(id);
            if (issue == null) return HttpNotFound();
            var canShow = (issue.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllTicket);
            if (canShow) return View(issue);
            return HttpNotFound();
        }



        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessTicket)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult IssueTrack(IssueTrackModel model, ActionType actionType)
        {
            var issue = _issueService.SingleOrDefault(model.IssueId);
            if (issue == null)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Ticket.ActionNames.Index);
            }
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Description))
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Ticket.ActionNames.Show, new { id = model.IssueId });
            }

            #region Initial Issue Track
            var now = DateTime.Now;


            var issueTrack = new Netotik.Domain.Entity.TicketTrack()
            {
                CreateDate = now,
                CreatedUserId = 1,
                Description = model.Description
            };

            issue.MessageCount++;
            issue.LastResponseDate = now;
            issue.LastResponseUserId = 1;
            issue.status = IssueStatus.ResponseByUser;
            issue.TicketTracks.Add(issueTrack);

            #endregion

            try
            {
                _uow.SaveChanges();
            }
            catch
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }


            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            ModelState.Clear();
            return View(MVC.Admin.Ticket.Views.Show, issue);
        }




        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanChangeStatusTicket)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> CloseIssue(int id = 0)
        {
            var issue = _issueService.SingleOrDefault(id);
            if (issue == null) return HttpNotFound();
            var canChange = (issue.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllTicket);
            if (canChange && issue.status != IssueStatus.close)
            {
                issue.status = IssueStatus.close;
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction(MVC.Admin.Ticket.Show(id));
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanChangeStatusTicket)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> OpenIssue(int id = 0)
        {
            var issue = _issueService.SingleOrDefault(id);
            if (issue == null) return HttpNotFound();
            var canChange = (issue.CreatedUserId == User.Identity.GetUserId<long>()) ? true : UserPermissions.Any(x => x == AssignableToRolePermissions.CanViewAllTicket);
            if (canChange && issue.status == IssueStatus.close)
            {
                issue.status = IssueStatus.Reopened;
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction(MVC.Admin.Ticket.Show(id));
        }


        #region Private

        [NonAction]
        private void PopulateUserIssueUsers(params long[] selectedIds)
        {
            //var users = _applicationUserManager.().ToList().Select(x => new
            //SelectListItem
            //{
            //    Text = string.Format("{0} {1}", x.FirstName, x.LastName),
            //    Value = x.Id.ToString(),
            //    Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            //}).ToList();
            //ViewBag.Users = users;
        }

        [NonAction]
        private void PopulateRoles(params int[] selectedIds)
        {
            //var Roles = _applicationRoleManager.All().ToList().Select(x => new
            //SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString(),
            //    Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            //}).ToList();
            //ViewBag.Roles = Roles;
        }


        [NonAction]
        private void PopulateLabels(params int[] selectedIds)
        {
            var labels = _issueLabelService.All().ToList().Select(x => new
            SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (selectedIds != null) ? selectedIds.Any(y => y == x.Id) : false
            }).ToList();
            ViewBag.Labels = labels;
        }
        #endregion
    }
}