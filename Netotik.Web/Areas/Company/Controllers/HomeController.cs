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
using Netotik.ViewModels.Identity.UserCompany;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Microsoft.AspNet.Identity;

namespace Netotik.Web.Areas.Company.Controllers
{
    [Mvc5Authorize(Roles = "Company")]
    [BreadCrumb(Title = "کاربر", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
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
            return PartialView(MVC.Company.Home.Views._ProfileData, _applicationUserManager.GetUserCompanyProfile(UserLogined.Id));
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> changeImageProfile(HttpPostedFileBase image)
        {
            var user = await _applicationUserManager.FindByIdAsync(User.Identity.GetUserId<long>());

            if (image == null)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
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

            this.MessageInformation(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Company.Home.MyProfile());
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> UpdateProfile(ProfileModel model)
        {
            #region Validation
            if (_applicationUserManager.CheckCompanyEmailExist(model.Email, User.Identity.GetUserId<long>()))
                ModelState.AddModelError("Email", "این ایمیل قبلا در سیستم ثبت شده است");

            if (_applicationUserManager.CheckCompanyPhoneNumberExist(model.PhoneNumber, User.Identity.GetUserId<long>(),model.UserResellerId))
                ModelState.AddModelError("PhoneNumber", "این شماره موبایل قبلا در سیستم ثبت شده است");

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.Company.Home.ActionNames.MyProfile);
            }
            #endregion
            if (model.Email != UserLogined.Email)
                model.EmailConfirmed = false;
            model.Id = UserLogined.Id;
            model.UserResellerId = UserLogined.UserCompany.UserResellerId;
            this.MessageInformation(Messages.MissionSuccess, Messages.UpdateSuccess);
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
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            #endregion
            if (model.R_Password == "" || model.R_Password == null)
            {
                model.R_Password = UserLogined.UserCompany.R_Password;
                this.MessageInformation("توجه:", "پسورد روتر خالی نمی تواند باشد.ما پسورد قبلی که در سیستم گذاشته اید را تغییر ندادیم.");
            }
            model.Id = UserLogined.Id;
            if (model.cloud == true)
                model.R_Host = _mikrotikServices.EnableAndGetCloud(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageInformation(Messages.MissionSuccess, Messages.UpdateSuccess);
            await _applicationUserManager.UpdateUserCompanyMikrotikConf(model);
            return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
        }
        #endregion

        #region Detail

        
        public virtual ActionResult Detail(int id)
        {
            //var user = _applicationUserManager.SingleOrDefault(id);
            //if(user.Reseller_Id !=User.UserId)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            //if (user == null)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = MVC.Company.Name });

            ////ViewBag.logins = user.LoginHistories.OrderByDescending(x => x.RegisterDate).Take(10).ToList();
            //return View(user);
            return View();
        }

        #endregion

        #region Edit
        
        [BreadCrumb(Title = "ویرایش", Order = 1)]
        public virtual ActionResult Edit(int id)
        {
            //var model = _applicationUserManager.SingleOrDefault(id);
            //if (model.Reseller_Id != User.UserId)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            //if (model == null)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = MVC.Company.Name });

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

        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Edit(CompanyEditModel model, ActionType actionType)
        {
            //var companyuser = _applicationUserManager.SingleOrDefault(model.Id);
            //if (companyuser.Reseller_Id != User.UserId)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            //if (companyuser == null)
            //    return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = MVC.Company.Name });

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


        
        public virtual ActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Reseller.Home.ActionNames.ChangePassword);
            }
            var temp = await _applicationUserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.Password);
            if (temp.Succeeded)
                this.MessageInformation(Messages.MissionSuccess, Messages.UpdateSuccess);
            else
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
            return View();
        }

        #endregion



    }
}