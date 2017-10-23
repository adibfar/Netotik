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
using System.IO;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Identity.UserAdmin;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using WebGrease.Css.Extensions;
using System.Net;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "UsersList", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon-users2")]
    public partial class UserAdminController : BasePanelController
    {

        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IPictureService _pictureService;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUnitOfWork _uow;

        public UserAdminController(
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            IApplicationRoleManager applicationRoleManager,
            IUnitOfWork uow)
        {
            _pictureService = pictureservice;
            _applicationRoleManager = applicationRoleManager;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
        }
        #endregion

        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessUser)]
        public virtual ActionResult Index()
        {
            return View();
        }


        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _applicationUserManager.GetListUserAdmins(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);


        }

        #endregion

        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateUser)]
        [BreadCrumb(Title = "NewUser", GlyphIcon = "icon-plus", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await PopulateRoles();
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateUser)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(AdminAddModel model)
        {
            #region Validation
            if (_userManager.CheckAdminPhoneNumberExist(model.PhoneNumber, null))
                ModelState.AddModelError("PhoneNumber", string.Format(Captions.ExistError, Captions.MobileNumber));
            if (_userManager.CheckUserNameExist(model.UserName, null))
                ModelState.AddModelError("UserName", string.Format(Captions.ExistError, Captions.UserName));
            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", Captions.PasswordEasy);
            if (_userManager.CheckAdminEmailExist(model.Email, null))
                ModelState.AddModelError("Email", string.Format(Captions.ExistError, Captions.Email));
            #endregion

            if (!ModelState.IsValid)
            {
                await PopulateRoles(model.RoleIds);
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(model);
            }
            if (model.RoleIds == null || model.RoleIds.Length < 1)
            {
                this.MessageError(Captions.MissionFail, Captions.SelectRole);
                await PopulateRoles(model.RoleIds);
                return View(model);
            }

            var user = await _userManager.AddUser(model);
            #region Add Avatar Image
            if (model.ImageAvatar != null && model.ImageAvatar.ContentLength > 0)
            {
                var fileName = SaveFile(model.ImageAvatar, FilePathes._imagesUserAvatarsPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.ImageAvatar.FileName,
                    MimeType = model.ImageAvatar.ContentType
                };
                user.Picture = picture;
                await _uow.SaveChangesAsync();
            }
            #endregion

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.UserAdmin.Index());

        }


        #endregion



        #region Edit
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [HttpPost]
        public virtual ActionResult Remove(int id = 0)
        {
            _applicationUserManager.LogicalRemove(id);
            return RedirectToAction(MVC.Admin.UserAdmin.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteUser)]
        [HttpPost]
        public virtual ActionResult Active(int id = 0)
        {
            var user = _applicationUserManager.FindUserById(id);
            if (user.IsBanned == true && user.UserType == UserType.UserAdmin)
                _applicationUserManager.ActiveUser(id);
            // add message

            return RedirectToAction(MVC.Admin.UserAdmin.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteUser)]
        [HttpPost]
        public virtual ActionResult Banne(int id = 0)
        {
            var user = _applicationUserManager.FindUserById(id);
            if (user.IsBanned == false && user.UserType == UserType.UserAdmin)
                _applicationUserManager.BanneUser(id);
            // add message

            return RedirectToAction(MVC.Admin.UserAdmin.Index());
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteUser)]
        [BreadCrumb(Title = "EditUser", GlyphIcon = "icon-edit2", Order = 1)]
        public virtual async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _userManager.GetUserAdminByIdAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            await PopulateRoles(viewModel.Roles.Select(a => a.RoleId).ToArray());

            if (viewModel.Picture != null)
                ViewBag.Avatar = Path.Combine(Common.Controller.FilePathes._imagesUserAvatarsPath, viewModel.Picture.FileName);

            return View(viewModel);

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(AdminEditModel model)
        {
            #region Validation
            if (_userManager.CheckAdminPhoneNumberExist(model.PhoneNumber, model.Id))
                ModelState.AddModelError("PhoneNumber", string.Format(Captions.ExistError, Captions.MobileNumber));
            if (_userManager.CheckUserNameExist(model.UserName, model.Id))
                ModelState.AddModelError("UserName", string.Format(Captions.ExistError, Captions.UserName));
            if (_userManager.CheckAdminEmailExist(model.Email, model.Id))
                ModelState.AddModelError("Email", string.Format(Captions.ExistError, Captions.Email));
            #endregion


            if (!ModelState.IsValid)
            {
                await PopulateRoles(model.RoleIds);
                return View(model);
            }

            var dbUser = _userManager.FindUserById(model.Id);
            if (dbUser == null) return HttpNotFound();


            #region Add Avatar Image
            if (model.ImageAvatar != null && model.ImageAvatar.ContentLength > 0)
            {

                if (dbUser.PictureId.HasValue)
                    DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesUserAvatarsPath, dbUser.Picture.FileName)));

                var fileName = SaveFile(model.ImageAvatar, Common.Controller.FilePathes._imagesUserAvatarsPath);
                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.ImageAvatar.FileName,
                    MimeType = model.ImageAvatar.ContentType
                };
                model.Picture = picture;
            }
            #endregion

            if (!await _userManager.EditUser(model))
            {
                if (model.Picture != null)
                    DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesUserAvatarsPath, model.Picture.FileName)));

                this.MessageError(Captions.MissionFail, Captions.SelectRole);
                await PopulateRoles();
                return View(model);
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.UserAdmin.Index());
        }

        #endregion


        #region Private
        [NonAction]
        private async Task PopulateRoles(params long[] selectedIds)
        {
            var roles = await _applicationRoleManager.GetAllAsSelectListAsync();

            if (selectedIds != null)
            {
                roles.ForEach(a => a.Selected = selectedIds.Any(b => long.Parse(a.Value) == b));
            }

            ViewBag.Roles = roles;
        }

        #endregion
    }
}