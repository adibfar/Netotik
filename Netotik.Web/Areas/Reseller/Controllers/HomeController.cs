using Netotik.Data;
using Netotik.Services.Abstract;
using Netotik.Web.Infrastructure;
using Netotik.Web.Infrastructure.Filters;
using Netotik.Common.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Netotik.Resources;
using Netotik.Domain.Entity;
using Netotik.Common.Security;
using Netotik.ViewModels;
using Netotik.Services.Enums;
using Netotik.Common;
using CaptchaMvc.Attributes;
using System.Data.Entity;
using Netotik.ViewModels.Identity.UserReseller;
using Netotik.Services.Identity;
using Microsoft.AspNet.Identity;
using Netotik.Common.Controller;
using System.IO;
using Netotik.ViewModels.Identity.Account;
using Mvc.Mailer;

namespace Netotik.Web.Areas.Reseller.Controllers
{
    [Mvc5Authorize(Roles = "Reseller")]
    public partial class HomeController : BasePanelController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMenuService _menuService;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IUserMailer _userMailer;

        public HomeController(
            IApplicationUserManager applicationUserManager,
            IApplicationSignInManager applicationSignInManager,
            IUserMailer userMailer,
            IUnitOfWork uow, IMenuService menuService)
        {
            _applicationUserManager = applicationUserManager;
            _applicationSignInManager = applicationSignInManager;
            _menuService = menuService;
            _userMailer = userMailer;
            _uow = uow;
        }
        public virtual ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> changeImageProfile(HttpPostedFileBase image)
        {
            var user = await _applicationUserManager.FindByIdAsync(User.Identity.GetUserId<long>());

            if (image == null)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(MVC.Reseller.Home.Views.MyProfile);
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
            return RedirectToAction(MVC.Reseller.Home.MyProfile());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> UpdateProfile(ProfileModel model)
        {
            #region Validation
            if (_applicationUserManager.CheckResellerEmailExist(model.Email, User.Identity.GetUserId<long>()))
                ModelState.AddModelError("Email", Captions.ExistError);

            if (_applicationUserManager.CheckResellerPhoneNumberExist(model.PhoneNumber, User.Identity.GetUserId<long>()))
                ModelState.AddModelError("PhoneNumber", Captions.ExistError);

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.Reseller.Home.ActionNames.MyProfile);
            }
            #endregion
            if (model.Email != UserLogined.Email)
            {
                model.EmailConfirmed = false;
                await SendConfirmationEmail(model.Email, UserLogined.Id);
                this.MessageSuccess(Captions.MissionSuccess, Captions.WillSendActivationAccountMessage);
            }
            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserResellerProfile(model);
            return RedirectToAction(MVC.Reseller.Home.ActionNames.MyProfile);
        }

        public virtual ActionResult MyProfile()
        {
            //return View(_ResellerService.GetProfile(User.UserId));
            return View();
        }
        public virtual ActionResult ProfileData()
        {
            return PartialView(MVC.Reseller.Home.Views._ProfileData, _applicationUserManager.GetUserResellerProfile());
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
                return RedirectToAction(MVC.Reseller.Home.ActionNames.ChangePassword);
            }
            var temp = await _applicationUserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.Password);
            if (temp.Succeeded)
            {
                await _applicationSignInManager.PasswordSignInAsync
                    (UserLogined.UserName, model.Password, false, shouldLockout: true);
                this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            } else
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
            return View();
        }

        public async Task SendConfirmationEmail(string email, long userId)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Abs(Url.Action(MVC.Account.ActionNames.ConfirmEmail, MVC.Account.Name,
                new { userId, code, area = "" }, protocol: Request.Url.Scheme));

            _userMailer.ConfirmAccount(new EmailViewModel
            {
                Message = Captions.ActivationMailMessage,
                To = email,
                Url = callbackUrl,
                UrlText = Captions.ActivationMailSubject,
                Subject = Captions.ActivationMailSubject,
                ViewName = MVC.UserMailer.Views.ViewNames.ConfirmAccount
            }).Send();

        }

    }
}