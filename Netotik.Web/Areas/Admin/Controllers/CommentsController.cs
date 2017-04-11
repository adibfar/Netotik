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

namespace Netotik.Web.Areas.Admin.Controllers
{
       [BreadCrumb(Title = "نظرات مقالات", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
    Order = 0, GlyphIcon = "icon icon-table")]
    public partial class CommentsController : BaseController
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
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {

            var pageList = _contentCommentService.All()
                .Where(x => x.Status != CommentStatus.Delete)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreateDate)
                .ToPagedList<ContentComment>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.Comments.Views._Table, pageList);
            else
                return View(MVC.Admin.Comments.ActionNames.Index, pageList);
        }
        #endregion


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessComment)]
        public virtual ActionResult Detail(int id)
        {
            var comment = _contentCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            ViewBag.Comments = comment.Content.ContentComments.Where(x => x.Status != CommentStatus.Delete).OrderByDescending(x => x.CreateDate).ToList();

            return View(comment);
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAcceptComment)]
        [HttpPost]
        public virtual async Task<ActionResult> Accept(int id)
        {
            var comment = _contentCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Status = CommentStatus.Accepted;
            comment.Content.CommentCount++;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.Comments.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDontAcceptComment)]
        [HttpPost]
        public virtual async Task<ActionResult> DontAccept(int id)
        {
            var comment = _contentCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Status = CommentStatus.NotAccept;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.Comments.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteComment)]
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id)
        {
            var comment = _contentCommentService.SingleOrDefault(id);
            if (comment == null) return HttpNotFound();

            comment.Content.CommentCount--;
            comment.Status = CommentStatus.Delete;
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.Comments.ActionNames.Index);
        }




    }
}