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
using Netotik.Common.MikrotikAPI;
using Netotik.ViewModels.Identity.UserRouter;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Microsoft.AspNet.Identity;
using Netotik.Services.Implement;
using Mvc.Mailer;
using Netotik.ViewModels.Identity.Account;
using Netotik.ViewModels.Identity.Security;
using WebGrease.Css.Extensions;
using System.Net;
using Netotik.Common.DataTables;
using Microsoft.Owin.Security;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessUser)]
    [BreadCrumb(Title = "RouterList", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class UserRouterController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IPictureService _pictureService;
        private readonly IUserMailer _userMailer;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUnitOfWork _uow;
        private readonly ISmsService _smsService;

        public UserRouterController(
            IApplicationSignInManager applicationSignInManager,
            IAuthenticationManager authenticationManager,
            IApplicationUserManager applicationUserManager,
            IPictureService pictureservice,
            IUserMailer userMailer,
            IApplicationRoleManager applicationRoleManager,
            IMikrotikServices mikrotikServices,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _authenticationManager = authenticationManager;
            _applicationSignInManager = applicationSignInManager;
            _pictureService = pictureservice;
            _userMailer = userMailer;
            _applicationRoleManager = applicationRoleManager;
            _applicationUserManager = applicationUserManager;
            _mikrotikServices = mikrotikServices;
            _smsService = smsService;
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

            var result = _applicationUserManager.GetListUserRouters(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);


        }

        #endregion


        public virtual ActionResult Remove(long id = 0)
        {
            _applicationUserManager.LogicalRemove(id);
            return RedirectToAction(MVC.Admin.UserRouter.Index());
        }



        #region Detail

        public virtual ActionResult Detail(long id)
        {
            return View();
        }

        #endregion
        #region Edit
        [BreadCrumb(Title = "EditRouter", Order = 1)]
        public virtual async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _userManager.GetAdminUserRouterByIdAsync(id.Value);
            if (viewModel == null) return HttpNotFound();



            if (viewModel.Picture != null)
                ViewBag.Avatar = Path.Combine(Common.Controller.FilePathes._imagesUserAvatarsPath, viewModel.Picture.FileName);

            PopulateResellers(viewModel.ResellerId);
            PopulateClientPermissions(_applicationUserManager.FindClientPermissions(viewModel.Id).ToArray());

            return View(viewModel);

        }

        [BreadCrumb(Title = "EditRouter", Order = 1)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(RouterAdminEditModel model)
        {
            PopulateClientPermissions(model.ClientPermissionNames);
            PopulateResellers(model.ResellerId);

            #region Validation
            if (_userManager.CheckRouterPhoneNumberExist(model.PhoneNumber, model.Id))
                ModelState.AddModelError("PhoneNumber", string.Format(Captions.ExistError, Captions.MobileNumber));

            if (_userManager.CheckUserNameExist(model.UserName, model.Id))
                ModelState.AddModelError("UserName", string.Format(Captions.ExistError, Captions.UserName));

            if (_userManager.CheckRouterEmailExist(model.Email, model.Id))
                ModelState.AddModelError("Email", string.Format(Captions.ExistError, Captions.Email));

            if (!_userManager.IsNationalCodeValid(model.NationalCode))
                ModelState.AddModelError("NationalCode", string.Format(Captions.NotValidError, Captions.NationalCode));

            if (_userManager.CheckRouterRouterCodeExist(model.RouterCode, model.Id))
                ModelState.AddModelError("RouterCode", string.Format(Captions.ExistError, Captions.RouterCode));

            #endregion

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var dbUser = _userManager.FindUserById(model.Id);
            if (dbUser == null || dbUser.IsDeleted) return HttpNotFound();

            if (model.R_Password == "" || model.R_Password == null)
            {
                model.R_Password = dbUser.UserRouter.R_Password;
                this.MessageInformation(Captions.Attention, Captions.RouterPasswordEmptyInformation);
            }

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

            if (!_mikrotikServices.IP_Port_Check(model.R_Host, model.R_Port, model.R_User, model.R_Password))
            {
                this.MessageWarning(Captions.Information, Captions.IPPORTClientError);
            }
            else
            {
                if (!_mikrotikServices.User_Pass_Check(model.R_Host, model.R_Port, model.R_User, model.R_Password))
                {
                    this.MessageWarning(Captions.Information, Captions.UserPasswordClientError);
                }
                else
                {
                    if (!_mikrotikServices.Usermanager_IsInstall(model.R_Host, model.R_Port, model.R_User, model.R_Password))
                    {
                        this.MessageWarning(Captions.Information, Captions.UsermanagerClientError);
                    }
                    else
                    {
                        if (model.cloud)
                        {
                            model.R_Host = _mikrotikServices.EnableAndGetCloud(model.R_Host, model.R_Port, model.R_User, model.R_Password);
                        }
                    }
                }
            }

            if (!await _userManager.EditRouter(model))
            {
                if (model.Picture != null)
                    DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesUserAvatarsPath, model.Picture.FileName)));

                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return View(model);
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.UserRouter.Index());
        }


        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.ChangePassword);
            }
            var temp = await _applicationUserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.Password);
            if (temp.Succeeded)
            {
                var Router = _applicationUserManager.FindUserById(User.Identity.GetUserId<long>());
                if (Router.UserRouter.SmsCharge > 0 && Router.UserRouter.SmsActive && Router.UserRouter.SmsAdminChangeAdminPassword)
                    _smsService.SendSms(UserLogined.PhoneNumber, string.Format(Captions.SmsRouterPasswordChange, UserLogined.UserName), UserLogined.Id);
                this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            }
            else
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
            return View();
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [HttpPost]
        public virtual async Task<ActionResult> LoginRouter(long id)
        {
            var user = await _userManager.FindByIdAsync(id);
            _authenticationManager.SignOut
                (
                    DefaultAuthenticationTypes.ExternalCookie,
                    DefaultAuthenticationTypes.ApplicationCookie
                );

            await _applicationSignInManager.SignInAsync(user, false, false);
            return RedirectToAction(MVC.MyRouter.Home.Index());
        }



        public virtual async Task<ActionResult> Disable(int id = 0)
        {
            var model = _userManager.FindUserById(id);
            if (model == null || model.UserRouter.UserResellerId != UserLogined.Id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.Reseller.Router.ActionNames.Index);
            }
            model.IsBanned = true;
            await _uow.SaveAllChangesAsync();
            this.MessageSuccess(Captions.Active, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Reseller.Router.ActionNames.Index);
        }

        public virtual async Task<ActionResult> Enable(int id = 0)
        {
            var model = _userManager.FindUserById(id);
            if (model == null || model.UserRouter.UserResellerId != UserLogined.Id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.Reseller.Router.ActionNames.Index);
            }
            model.IsBanned = false;
            await _userManager.UpdateAsync(model);
            this.MessageSuccess(Captions.Active, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Reseller.Router.ActionNames.Index);
        }
        #endregion


        [NonAction]
        private void PopulateClientPermissions(params string[] selectedpermissions)
        {
            var permissions = AssignablePermissionToClient.GetAsSelectListItems();

            if (selectedpermissions != null)
            {
                permissions.ForEach(
                    a => a.Selected = selectedpermissions.Any(s => s == a.Value));
            }
            ViewBag.ClientPermissions = permissions;
        }

        [NonAction]
        private void PopulateResellers(long? resellerId = null)
        {
            ViewBag.Resellers = _userManager.GetResellersSelectList(resellerId);
        }
    }
}