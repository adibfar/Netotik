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
using Netotik.Services.Identity;
using Netotik.Common.DataTables;
using Netotik.ViewModels.CMS.Comment;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "نظرات مقالات", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class CommentsController : BasePanelController
    {

        #region ctor
        private readonly IContentCommentService _contentCommentService;
        private readonly IContentService _contentService;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IUnitOfWork _uow;

        public CommentsController(
            IContentCommentService contentCommentService,
            IApplicationUserManager applicationUserManager,
            IContentService contentService,
            IUnitOfWork uow)
        {
            _contentCommentService = contentCommentService;
            _applicationUserManager = applicationUserManager;
            _contentService = contentService;
            _uow = uow;
        }
        #endregion


        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessComment)]
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _contentCommentService.GetList(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAcceptComment)]
        [HttpPost]
        public virtual async Task<ActionResult> Accept(int id)
        {
            var comment = _contentCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Status = CommentStatus.Accepted;
            comment.Content.CommentCount++;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.Comments.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDontAcceptComment)]
        [HttpPost]
        public virtual async Task<ActionResult> DontAccept(int id)
        {
            var comment = _contentCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Status = CommentStatus.NotAccept;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.Comments.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteComment)]
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id)
        {
            var comment = _contentCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Content.CommentCount--;
            comment.Status = CommentStatus.Deleted;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.Comments.Index());
        }



        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = _contentCommentService.SingleOrDefault(id);
            if (model == null) return RedirectToAction(MVC.Admin.Comments.Index());

            return PartialView(MVC.Admin.Comments.Views._Edit
                , new EditCommentModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    Text = model.Text
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(EditCommentModel model, ActionType actionType = ActionType.Save)
        {
            var comment = _contentCommentService.SingleOrDefault(model.Id);
            if (comment == null) return RedirectToAction(MVC.Admin.Comments.Index());

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Comments.Index());
            }

            comment.Name = model.Name;
            comment.Text = model.Text;
            comment.Email = model.Email;

            _contentCommentService.Update(comment);
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateSuccess);
                return RedirectToAction(MVC.Admin.Comments.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Comments.Index());
        }



        public virtual async Task<ActionResult> Reply(int id)
        {
            var model = _contentCommentService.SingleOrDefault(id);
            if (model == null) return RedirectToAction(MVC.Admin.Comments.Index());

            return PartialView(MVC.Admin.Comments.Views._Reply
                , new ReplyCommentModel
                {
                    Id = model.Id,
                    CommentName = model.Name,
                    CommentEmail = model.Email,
                    CommentText = model.Text,
                    Name = string.Format("{0} {1}", UserLogined.FirstName, UserLogined.LastName),
                    Email = UserLogined.Email
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Reply(ReplyCommentModel model, ActionType actionType = ActionType.Save)
        {
            var comment = _contentCommentService.SingleOrDefault(model.Id);
            if (comment == null) return RedirectToAction(MVC.Admin.Comments.Index());

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Comments.Index());
            }

            comment.Comments.Add(new ContentComment
            {
                CreateDate = DateTime.Now,
                Status = CommentStatus.WaitForAccept,
                UserId = UserLogined.Id,
                CommentId = comment.Id,
                ContentId = comment.ContentId,
                Text = model.Text,
                Name = model.Name,
                Email = model.Email
            });

            _contentCommentService.Update(comment);
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return RedirectToAction(MVC.Admin.Comments.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Comments.Index());
        }

    }
}