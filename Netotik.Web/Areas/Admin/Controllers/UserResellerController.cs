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
using Netotik.ViewModels.Identity.UserReseller;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using WebGrease.Css.Extensions;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "ResellersList", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon-users3")]
    public partial class UserResellerController : BasePanelController
    {

        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IPictureService _pictureService;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUnitOfWork _uow;

        public UserResellerController(
            IAuthenticationManager authenticationManager,
            IApplicationSignInManager applicationSignInManager,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            IApplicationRoleManager applicationRoleManager,
            IUnitOfWork uow)
        {
            _authenticationManager = authenticationManager;
            _applicationSignInManager = applicationSignInManager;
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
        [BreadCrumb(Title = "NewReseller", GlyphIcon = "icon-plus", Order = 1)]
        public virtual ActionResult Create()
        {
            ModelState.Clear();
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateUser)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ResellerAddModel model)
        {
            #region Validation
            if (_userManager.CheckResellerPhoneNumberExist(model.PhoneNumber, null))
                ModelState.AddModelError("PhoneNumber", string.Format(Captions.ExistError, Captions.MobileNumber));
            if (_userManager.CheckUserNameExist(model.UserName, null))
                ModelState.AddModelError("UserName", string.Format(Captions.ExistError, Captions.UserName));
            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", Captions.PasswordEasy);
            if (_userManager.CheckResellerEmailExist(model.Email, null))
                ModelState.AddModelError("Email", string.Format(Captions.ExistError, Captions.Email));
            #endregion

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
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

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
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
        [BreadCrumb(Title = "EditReseller", GlyphIcon = "icon-edit", Order = 1)]
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
                ModelState.AddModelError("PhoneNumber", string.Format(Captions.ExistError, Captions.MobileNumber));
            if (_userManager.CheckUserNameExist(model.UserName, model.Id))
                ModelState.AddModelError("UserName", string.Format(Captions.ExistError, Captions.UserName));
            if (_userManager.CheckResellerEmailExist(model.Email, model.Id))
                ModelState.AddModelError("Email", string.Format(Captions.ExistError, Captions.Email));
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

                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return View(model);
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.UserReseller.Index());
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [HttpPost]
        public virtual async Task<ActionResult> LoginReseller(long id)
        {
            var user = await _userManager.FindByIdAsync(id);
            _authenticationManager.SignOut
                (
                    DefaultAuthenticationTypes.ExternalCookie,
                    DefaultAuthenticationTypes.ApplicationCookie
                );

            await _applicationSignInManager.SignInAsync(user, false, false);
            return RedirectToAction(MVC.Reseller.Home.Index());
        }

        #endregion

    }
}