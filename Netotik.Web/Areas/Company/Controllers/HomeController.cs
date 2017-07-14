﻿using System;
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
using Netotik.ViewModels.Identity.UserCompany;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Microsoft.AspNet.Identity;
using Netotik.ViewModels.Identity.Security;
using WebGrease.Css.Extensions;

namespace Netotik.Web.Areas.Company.Controllers
{
    [Mvc5Authorize(Roles = "Company")]
    [BreadCrumb(Title = "User", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class HomeController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _uow;

        public HomeController(
            IMikrotikServices mikrotikServices,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _pictureService = pictureservice;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
        }
        #endregion

        #region Index

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult MyProfile()
        {
            return View();
        }

        public virtual ActionResult ProfileData()
        {
            var company = _applicationUserManager.GetUserCompanyProfile(UserLogined.Id);
            PopulatePermissions(_applicationUserManager.FindClientPermissions(company.Id).ToArray());

            return PartialView(MVC.Company.Home.Views._ProfileData, company);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> changeImageProfile(HttpPostedFileBase image)
        {
            var user = await _applicationUserManager.FindByIdAsync(User.Identity.GetUserId<long>());

            if (image == null)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(MVC.Company.Home.Views.MyProfile);
            }


            if (user.PictureId.HasValue)
                DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesUserAvatarsPath, user.Picture.FileName)));

            var fileName = SaveFile(image, Common.Controller.FilePathes._imagesUserAvatarsPath);
            var picture = new Picture
            {
                FileName = fileName,
                OrginalName = image.FileName,
                MimeType = image.ContentType
            };
            user.Picture = picture;
            await _uow.SaveAllChangesAsync();

            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Company.Home.MyProfile());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> UpdateProfile(ProfileModel model)
        {
            PopulatePermissions(model.ClientPermissionNames);
            #region Validation
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.Company.Home.ActionNames.MyProfile);
            }
            #endregion
            if (model.Email != UserLogined.Email)
                model.EmailConfirmed = false;
            model.Id = UserLogined.Id;
            model.UserResellerId = UserLogined.UserCompany.UserResellerId;

            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserCompanyProfile(model);
            return RedirectToAction(MVC.Company.Home.ActionNames.MyProfile);
        }

        public virtual ActionResult MikrotikConf()
        {
            return View(_applicationUserManager.GetUserCompanyMikrotikConf(UserLogined.Id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> MikrotikConf(MikrotikConfModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            #endregion
            if (model.R_Password == "" || model.R_Password == null)
            {
                model.R_Password = UserLogined.UserCompany.R_Password;
                this.MessageInformation(Captions.Attention, Captions.RouterPasswordEmptyInformation);
            }
            model.Id = UserLogined.Id;
            if (model.cloud == true)
                model.R_Host = _mikrotikServices.EnableAndGetCloud(model.R_Host, model.R_Port, model.R_User, model.R_Password);
            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserCompanyMikrotikConf(model);
            return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
        }
        #endregion


        #region Edit

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
                return RedirectToAction(MVC.Company.Home.ActionNames.ChangePassword);
            }
            var temp = await _applicationUserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.Password);
            if (temp.Succeeded)
                this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            else
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
            return View();
        }

        #endregion



        [NonAction]
        private void PopulatePermissions(params string[] selectedpermissions)
        {
            var permissions = AssignablePermissionToClient.GetAsSelectListItems();

            if (selectedpermissions != null)
            {
                permissions.ForEach(a => a.Selected = selectedpermissions.Any(s => s == a.Value));
            }

            ViewBag.ClientPermissions = permissions;
        }

    }
}