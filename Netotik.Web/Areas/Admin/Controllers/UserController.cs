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
using Netotik.ViewModels.Identity.UserAdmin;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using WebGrease.Css.Extensions;
using System.Net;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست کاربران", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class UserController : BasePanelController
    {

        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IPictureService _pictureService;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUnitOfWork _uow;

        public UserController(
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
        [BreadCrumb(Title = "کاربر جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            await PopulateRoles();
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateUser)]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(AdminAddModel model)
        {
            #region Validation
            if (_userManager.CheckIsPhoneNumberAvailable(model.PhoneNumber, null))
                ModelState.AddModelError("PhoneNumber", "این شماره موبایل قبلا در سیستم ثبت شده است");
            if (_userManager.CheckUserNameExist(model.UserName, null))
                ModelState.AddModelError("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");
            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", "این کلمه عبور به راحتی قابل تشخیص است");
            if (_userManager.CheckEmailExist(model.Email, null))
                ModelState.AddModelError("Email", "این ایمیل قبلا در سیستم ثبت شده است");
            #endregion

            if (!ModelState.IsValid)
            {
                await PopulateRoles(model.RoleIds);
                return View(model);
            }
            if (model.RoleIds == null || model.RoleIds.Length < 1)
            {
                this.MessageError(Messages.MissionFail, "لطفا برای  کاربر مورد نظر ، گروه کاربری تعیین کنید");
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

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.User.Index());

        }


        #endregion

        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessUser)]
        public virtual ActionResult Detail(int id)
        {
            //var user = _applicationUserManager.SingleOrDefault(id);
            //if (user == null)
            //    return RedirectToAction(MVC.Admin.User.ActionNames.Index);

            ////ViewBag.logins = user.LoginHistories.OrderByDescending(x => x.RegisterDate).Take(10).ToList();
            //return View(user);
            return View();
        }

        #endregion

        #region Edit
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Remove(int id = 0)
        {
            _applicationUserManager.LogicalRemove(id);
            return RedirectToAction(MVC.Admin.User.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [BreadCrumb(Title = "ویرایش", Order = 1)]
        public virtual async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _userManager.GetUserByRolesAsync(id.Value);
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
            if (_userManager.CheckIsPhoneNumberAvailable(model.PhoneNumber, model.Id))
                ModelState.AddModelError("PhoneNumber", "این شماره موبایل قبلا در سیستم ثبت شده است");
            if (_userManager.CheckUserNameExist(model.UserName, model.Id))
                ModelState.AddModelError("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");
            if (_userManager.CheckEmailExist(model.Email, model.Id))
                ModelState.AddModelError("Email", "این ایمیل قبلا در سیستم ثبت شده است");
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

                this.MessageError(Messages.MissionFail, "لطفا برای کاربر مورد نظر ، گروه کاربری تعیین کنید");
                await PopulateRoles();
                return View(model);
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.User.Index());
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        public virtual ActionResult ChangePassword(int id)
        {
            //var user = _applicationUserManager.SingleOrDefault(id);
            //return View(new UserChangePasswordModel() { Id = user.Id });
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(UserChangePasswordModel model)
        {
            //if (!ModelState.IsValid)
            //{

            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
            //}
            //else
            //{
            //    var result = await _applicationUserManager.ChangePasswordAsync(model.Id, Encryption.EncryptingPassword(model.Password));
            //    if (result.Status)
            //    {
            //        try
            //        {
            //            await _uow.SaveChangesAsync();
            //        }
            //        catch (Exception ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.UpdateError);
            //        }
            //    }
            //    SetResultMessage(result);
            //}
            //return View(model);
            return View();
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