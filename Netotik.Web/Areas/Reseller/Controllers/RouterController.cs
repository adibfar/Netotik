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

namespace Netotik.Web.Areas.Reseller.Controllers
{
    [BreadCrumb(Title = "RouterList", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class RouterController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IPictureService _pictureService;
        private readonly IUserMailer _userMailer;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUnitOfWork _uow;
        private readonly ISmsService _smsService;

        public RouterController(
            IApplicationUserManager applicationUserManager,
            IPictureService pictureservice,
            IUserMailer userMailer,
            IApplicationRoleManager applicationRoleManager,
            IMikrotikServices mikrotikServices,
            ISmsService smsService,
            IUnitOfWork uow)
        {
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
        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult Index()
        {
            return View(_applicationUserManager.GetListUserRouter(UserLogined.UserReseller.Id));
        }
        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult RouterLoginURL()
        {
            ViewBag.ResellerCode = UserLogined.UserReseller.ResellerCode;
            return View();
        }
        #endregion

        [Mvc5Authorize(Roles = "Reseller")]
        [BreadCrumb(Title = "NewRouter", Order = 1)]
        public virtual ActionResult Create()
        {
            var list = _applicationUserManager.GetListUserRouter(UserLogined.UserReseller.Id);
            if (list.Count() > 3)
            {
                this.MessageError(Captions.MissionFail, "تنها مجاز به ایجاد سه روتر می باشید.");
                return RedirectToAction(MVC.Reseller.Router.Index());
            }
            PopulateClientPermissions();
            PopulateRouterPermissions();
            this.MessageInformation(Captions.Attention, Captions.PanelsFreeExpireDate);
            return View();
        }

        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult Remove(int id = 0)
        {
            var check = _applicationUserManager.FindUserById(id);
            if (check.UserRouter.UserResellerId != UserLogined.Id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.Reseller.Router.ActionNames.Index);
            }
            _applicationUserManager.LogicalRemove(id);
            return RedirectToAction(MVC.Reseller.Router.Index());
        }

        [Mvc5Authorize(Roles = "Reseller")]
        [BreadCrumb(Title = "NewRouter", Order = 1)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(Register model)
        {
            var list = _applicationUserManager.GetListUserRouter(UserLogined.UserReseller.Id);
            if (list.Count() > 3)
            {
                this.MessageError(Captions.MissionFail, "تنها مجاز به ایجاد سه روتر می باشید.");
                return View(model);
            }


            PopulateClientPermissions(model.ClientPermissionNames);
            PopulateRouterPermissions(model.RouterPermissionNames);

            if (_userManager.CheckRouterPhoneNumberExist(model.PhoneNumber, null))
                ModelState.AddModelError("PhoneNumber", string.Format(Captions.ExistError, Captions.MobileNumber));

            if (_userManager.CheckUserNameExist(model.UserName, null))
                ModelState.AddModelError("UserName", string.Format(Captions.ExistError, Captions.UserName));

            if (_userManager.CheckRouterEmailExist(model.Email, null))
                ModelState.AddModelError("Email", string.Format(Captions.ExistError, Captions.Email));

            if (!_userManager.IsNationalCodeValid(model.NationalCode))
                ModelState.AddModelError("NationalCode", string.Format(Captions.NotValidError, Captions.NationalCode));

            if (_userManager.CheckRouterRouterCodeExist(model.RouterCode, null))
                ModelState.AddModelError("RouterCode", string.Format(Captions.ExistError, Captions.RouterCode));

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(model);
            }
            if (model.cloud)
                if (!_mikrotikServices.IP_Port_Check(model.R_Host, model.R_Port, model.R_User, model.R_Password))
                    this.MessageWarning(Captions.Information, Captions.IPPORTClientError);
                else
                if (!_mikrotikServices.User_Pass_Check(model.R_Host, model.R_Port, model.R_User, model.R_Password))
                    this.MessageWarning(Captions.Information, Captions.UserPasswordClientError);
                else
                    if (!_mikrotikServices.Usermanager_IsInstall(model.R_Host, model.R_Port, model.R_User, model.R_Password))
                    this.MessageWarning(Captions.Information, Captions.UsermanagerClientError);
                else
                    model.R_Host = _mikrotikServices.EnableAndGetCloud(model.R_Host, model.R_Port, model.R_User, model.R_Password);
            model.UserResellerId = UserLogined.UserReseller.Id;
            var userId = await _applicationUserManager.AddRouter(model);

            await SendConfirmationEmail(model.Email, userId);
            var Success = Captions.CreateSuccsessGoToEmail;

            ModelState.Clear();
            this.MessageSuccess(Captions.ActiveYourAccount, Success);
            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Reseller.Router.ActionNames.Index);

        }

        #region Detail

        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult Detail(int id)
        {
            return View();
        }

        #endregion




        #region Edit
        [Mvc5Authorize(Roles = "Reseller")]
        [BreadCrumb(Title = "EditRouter", Order = 1)]
        public virtual async Task<ActionResult> Edit(long? id)
        {

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _userManager.GetUserRouterByIdAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            var check = _applicationUserManager.FindUserById(viewModel.Id);
            if (check.UserRouter.UserResellerId != UserLogined.Id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.Reseller.Router.ActionNames.Index);
            }
            if (viewModel.Picture != null)
                ViewBag.Avatar = Path.Combine(Common.Controller.FilePathes._imagesUserAvatarsPath, viewModel.Picture.FileName);

            PopulateClientPermissions(_applicationUserManager.FindClientPermissions(viewModel.Id).ToArray());
            PopulateRouterPermissions(_applicationUserManager.FindRouterPermissions(viewModel.Id).ToArray());

            return View(viewModel);

        }

        [Mvc5Authorize(Roles = "Reseller")]
        [BreadCrumb(Title = "EditRouter", Order = 1)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(RouterEditModel model)
        {
            var check = _applicationUserManager.FindUserById(model.Id);
            if (check.UserRouter.UserResellerId != UserLogined.Id)
            {
                this.MessageError(Captions.Error, Captions.InvalidDataError);
                return RedirectToAction(MVC.Reseller.Router.ActionNames.Index);
            }
            PopulateClientPermissions(model.ClientPermissionNames);
            PopulateRouterPermissions(model.RouterPermissionNames);

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


            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var dbUser = _userManager.FindUserById(model.Id);
            if (dbUser == null) return HttpNotFound();

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
            return RedirectToAction(MVC.Reseller.Router.Index());

        }


        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult ChangePassword(long Id)
        {

            return View(new ChangePasswordModel() { id = Id });
        }

        [Mvc5Authorize(Roles = "Reseller")]
        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Reseller.Router.ChangePassword(model.id));
            }

            if (_applicationUserManager.FindUserById(model.id).UserRouter.UserResellerId != UserLogined.Id)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Reseller.Router.Index());
            }
            var temp = await _applicationUserManager.ChangePasswordAsync(model.id, model.OldPassword, model.Password);
            if (temp.Succeeded)
            {
                var Router = _applicationUserManager.FindUserById(model.id);
                if (Router.UserRouter.SmsCharge > 0 && Router.UserRouter.SmsActive && Router.UserRouter.SmsAdminChangeAdminPassword)
                    _smsService.SendSms(UserLogined.PhoneNumber, string.Format(Captions.SmsRouterPasswordChange, UserLogined.UserName), UserLogined.Id);
                this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            }
            else
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
            return View();
        }


        [Mvc5Authorize(Roles = "Reseller")]
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

        [Mvc5Authorize(Roles = "Reseller")]
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

        public async Task SendConfirmationEmail(string email, long userId)
        {
            var code = await _applicationUserManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Abs(Url.Action(MVC.Account.ActionNames.ConfirmEmail, MVC.Account.Name,
                new { userId, code, area = "" }, protocol: Request.Url.Scheme));

            _userMailer.ConfirmAccount(new EmailViewModel
            {
                Message = Captions.ConfirmEmailMessage,
                To = email,
                Url = callbackUrl,
                UrlText = Captions.ActiveYourAccount,
                Subject = Captions.ActiveYourAccount,
                ViewName = MVC.UserMailer.Views.ViewNames.ConfirmAccount
            }).Send();

        }



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
        private void PopulateRouterPermissions(params string[] selectedpermissions)
        {
            var permissions = AssignablePermissionToRouter.GetAsSelectListItems();

            if (selectedpermissions != null)
            {
                permissions.ForEach(
                    a => a.Selected = selectedpermissions.Any(s => s == a.Value));
            }
            ViewBag.RouterPermissions = permissions;
        }


    }
}