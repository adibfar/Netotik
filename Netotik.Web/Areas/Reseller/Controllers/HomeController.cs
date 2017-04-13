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

namespace Netotik.Web.Areas.Reseller.Controllers
{
    [Mvc5Authorize(Roles = "Reseller")]
    public partial class HomeController : BasePanelController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMenuService _menuService;
        private readonly IApplicationUserManager _applicationUserManager;

        public HomeController(
            IApplicationUserManager applicationUserManager,
            IUnitOfWork uow, IMenuService menuService)
        {
            _applicationUserManager = applicationUserManager;
            _menuService = menuService;
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
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
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

            this.MessageInformation(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Reseller.Home.MyProfile());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> UpdateProfile(ProfileModel model)
        {
            #region Validation
            if (_applicationUserManager.CheckEmailExist(model.Email, User.Identity.GetUserId<long>()))
                ModelState.AddModelError("Email", "این ایمیل قبلا در سیستم ثبت شده است");

            if (_applicationUserManager.CheckIsPhoneNumberAvailable(model.PhoneNumber, User.Identity.GetUserId<long>()))
                ModelState.AddModelError("PhoneNumber", "این شماره موبایل قبلا در سیستم ثبت شده است");

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                //return View(MVC.Reseller.Home.Views._ProfileData, model);
                return RedirectToAction(MVC.Reseller.Home.ActionNames.MyProfile);
            }
            #endregion

            this.MessageInformation(Messages.MissionSuccess, Messages.UpdateSuccess);
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
        public virtual ActionResult ChangePassword(ChangePasswordModel model)
        {
            return View();
        }

    }
}