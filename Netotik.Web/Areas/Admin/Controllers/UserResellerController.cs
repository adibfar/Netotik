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
using Netotik.ViewModels.Identity.UserReseller;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using WebGrease.Css.Extensions;
using System.Net;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست نمایندگان", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class UserResellerController : BasePanelController
    {

        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IPictureService _pictureService;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUnitOfWork _uow;

        public UserResellerController(
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

            var result = _applicationUserManager.GetListUserResellers(model, out totalCount, out showCount);

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
        [BreadCrumb(Title = "نماینده جدید", Order = 1)]
        public virtual async Task<ActionResult> Create()
        {
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateUser)]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ResellerAddModel model)
        {
            #region Validation
            if (_userManager.CheckResellerPhoneNumberExist(model.PhoneNumber, null))
                ModelState.AddModelError("PhoneNumber", "این شماره موبایل قبلا در سیستم ثبت شده است");
            if (_userManager.CheckUserNameExist(model.UserName, null))
                ModelState.AddModelError("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");
            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", "این کلمه عبور به راحتی قابل تشخیص است");
            if (_userManager.CheckResellerEmailExist(model.Email, null))
                ModelState.AddModelError("Email", "این ایمیل قبلا در سیستم ثبت شده است");
            #endregion

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }

            var user = await _userManager.AddReseller(model);
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
            return RedirectToAction(MVC.Admin.UserReseller.Index());

        }


        #endregion

        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteUser)]
        [HttpPost]
        public virtual ActionResult Remove(int id = 0)
        {
            _applicationUserManager.LogicalRemove(id);
            return RedirectToAction(MVC.Admin.UserReseller.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteUser)]
        [HttpPost]
        public virtual ActionResult Active(int id = 0)
        {
            var user = _applicationUserManager.FindUserById(id);
            if (user.IsBanned == true && user.UserType == UserType.UserReseller)
                _applicationUserManager.ActiveUser(id);
            // add message

            return RedirectToAction(MVC.Admin.UserReseller.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteUser)]
        [HttpPost]
        public virtual ActionResult Banne(int id = 0)
        {
            var user = _applicationUserManager.FindUserById(id);
            if (user.IsBanned == false && user.UserType == UserType.UserReseller)
                _applicationUserManager.BanneUser(id);
            // add message

            return RedirectToAction(MVC.Admin.UserReseller.Index());
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [BreadCrumb(Title = "ویرایش", Order = 1)]
        public virtual async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _userManager.GetUserResellerByIdAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            if (viewModel.Picture != null)
                ViewBag.Avatar = Path.Combine(Common.Controller.FilePathes._imagesUserAvatarsPath, viewModel.Picture.FileName);

            return View(viewModel);

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ResellerEditModel model)
        {
            #region Validation
            if (_userManager.CheckResellerPhoneNumberExist(model.PhoneNumber, model.Id))
                ModelState.AddModelError("PhoneNumber", "این شماره موبایل قبلا در سیستم ثبت شده است");
            if (_userManager.CheckUserNameExist(model.UserName, model.Id))
                ModelState.AddModelError("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");
            if (_userManager.CheckResellerEmailExist(model.Email, model.Id))
                ModelState.AddModelError("Email", "این ایمیل قبلا در سیستم ثبت شده است");
            #endregion


            if (!ModelState.IsValid)
            {
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

            if (!await _userManager.EditReseller(model))
            {
                if (model.Picture != null)
                    DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesUserAvatarsPath, model.Picture.FileName)));

                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return View(model);
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.UserReseller.Index());
        }

        #endregion

    }
}