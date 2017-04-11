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
using Netotik.Web.Extension;
using System.IO;
using DNTBreadCrumb;
using Netotik.Common.MikrotikAPI;
using Netotik.ViewModels.Identity.UserCompany;
using Netotik.Services.Identity;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Company.Controllers
{
    [BreadCrumb(Title = "کاربر", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class HomeController : BaseController
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
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult Index()
        {
            //return View(_applicationUserManager.GetProfile(User.UserId));
            return View();
        }

        public virtual ActionResult Profile()
        {
            //return View(_applicationUserManager.GetProfileEdit(User.UserId));
            return View();
        }

        [HttpPost]
        public virtual ActionResult Profile(ProfileEditModel model)
        {
            //if (ModelState.IsValid)
            //    {
            //    if(model.cloud)
            //    model.R_Host=_mikrotikServices.EnableAndGetCloud(model.R_Host,model.R_Port,model.R_User,model.R_Password);
            //    SetResultMessage(_applicationUserManager.UpdateProfileEdit(model, User.UserId));
            //}
            return RedirectToAction(MVC.Company.Home.ActionNames.Profile);
        }
        #endregion

        #region Detail

        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult Detail(int id)
        {
            //var user = _applicationUserManager.SingleOrDefault(id);
            //if(user.Reseller_Id !=User.UserId)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            //if (user == null)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index);

            ////ViewBag.logins = user.LoginHistories.OrderByDescending(x => x.RegisterDate).Take(10).ToList();
            //return View(user);
            return View();
        }

        #endregion

        #region Edit
        [Mvc5Authorize(Roles = "Company")]
        [BreadCrumb(Title = "ویرایش", Order = 1)]
        public virtual ActionResult Edit(int id)
        {
            //var model = _applicationUserManager.SingleOrDefault(id);
            //if (model.Reseller_Id != User.UserId)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            //if (model == null)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index);

            //var editModel = new UserEditModel
            //{
            //    Username = model.UserName,
            //    Email = model.Email,
            //    MobileNumber = model.MobileNumber,
            //    Name = model.Name,
            //    PostalCode = model.PostalCode,
            //    Address = model.Address,
            //    CompanyName = model.CompanyName,
            //    PersonCode = model.PersonalCode,
            //    R_Host = model.R_Host,
            //    R_User = model.R_User,
            //    R_Password = model.R_Password,
            //    R_Port = model.R_Port
            //};


            //return View(editModel);


            return View();

        }

        [Mvc5Authorize(Roles = "Company")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Edit(CompanyEditModel model, ActionType actionType)
        {
            //var companyuser = _applicationUserManager.SingleOrDefault(model.Id);
            //if (companyuser.Reseller_Id != User.UserId)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            //if (companyuser == null)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index);

            //if (ModelState.IsValid)
            //{

            //    companyuser.Address = model.Address;
            //    companyuser.UserName = model.Email;
            //    companyuser.PostalCode = model.PostalCode;
            //    companyuser.PersonalCode = model.PersonCode;
            //    companyuser.Name = model.Name;
            //    companyuser.CompanyName = model.CompanyName;
            //    companyuser.Email = model.Email;
            //    companyuser.MobileNumber = model.MobileNumber;
            //    companyuser.R_User = model.R_User;
            //    companyuser.R_Host = model.R_Host;
            //    companyuser.R_Password = model.R_Password;
            //    companyuser.R_Port = model.R_Port;


            //    _applicationUserManager.Update(companyuser);

            //    if (result.Status)
            //    {
            //        try
            //        {
            //            _uow.SaveChanges();
            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //        catch (Exception ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //    }
            //    SetResultMessage(result);
            //    if (!result.Status) return View();

            //    if (actionType == ActionType.SaveContinue) return RedirectToAction(MVC.Company.Home.Edit(companyuser.Id));
            //    return RedirectToAction(MVC.Company.Home.Index());
            //}
            //else
            //{
            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
            //    return View();
            //}

            return View();

        }


        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        [Mvc5Authorize(Roles = "Company")]
        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
            //}
            //else
            //{
            //    var result = await _applicationUserManager.ChangePasswordAsync(User.UserId, Encryption.EncryptingPassword(model.Password));
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
            return View(model);
        }

        #endregion

        

    }
}