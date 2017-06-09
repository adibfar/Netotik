using Netotik.Data;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Identity.UserAdmin;
using Netotik.Services.Abstract;
using Netotik.Services.Identity;
using Netotik.Web.Infrastructure;
using Netotik.Web.Infrastructure.Filters;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Netotik.Common.Filters;
using System.Web;
using System.IO;
using Netotik.Common.Controller;
using Netotik.Domain.Entity;
using Netotik.Resources;
using System.Linq;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(AssignableToRolePermissions.CanViewAdminPanel)]
    public partial class HomeController : BasePanelController
    {
        private readonly IInboxContactUsMessageService _inboxMessageService;
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly ILanguageService _languageService;
        private readonly IUnitOfWork _uow;

        public HomeController(
            ILanguageService languageService,
            IInboxContactUsMessageService inboxMessageService,
            IApplicationUserManager applicationUserManager,
            IUnitOfWork uow)
        {
            _languageService = languageService;
            _applicationUserManager = applicationUserManager;
            _inboxMessageService = inboxMessageService;
            _uow = uow;
        }
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
            PopulateLangauges();
            return PartialView(MVC.Admin.Home.Views._ProfileData, _applicationUserManager.GetUserAdminProfile());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> UpdateProfile(ProfileModel model)
        {
            #region Validation
            if (_applicationUserManager.CheckAdminEmailExist(model.Email, User.Identity.GetUserId<long>()))
                ModelState.AddModelError("Email",string.Format(Captions.ExistError,Captions.Email));

            if (_applicationUserManager.CheckUserNameExist(model.UserName, User.Identity.GetUserId<long>()))
                ModelState.AddModelError("UserName", string.Format(Captions.ExistError, Captions.UserName));

            if (_applicationUserManager.CheckAdminPhoneNumberExist(model.PhoneNumber, User.Identity.GetUserId<long>()))
                ModelState.AddModelError("PhoneNumber", string.Format(Captions.ExistError, Captions.MobileNumber));

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Home.MyProfile());
            }
            #endregion

            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            await _applicationUserManager.UpdateUserAdminProfile(model);
            return RedirectToAction(MVC.Admin.Home.ActionNames.MyProfile);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> changeImageProfile(HttpPostedFileBase image)
        {
            var user = await _applicationUserManager.FindByIdAsync(User.Identity.GetUserId<long>());

            if (image == null)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(MVC.Admin.Home.Views.MyProfile);
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
            return RedirectToAction(MVC.Admin.Home.MyProfile());
        }


        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordModel model)
        {

            //if (ModelState.IsValid)
            //{
            //    SetResultMessage(_applicationUserManager.ChangePassword(model, User.UserId));
            //}
            return RedirectToAction(MVC.Admin.Home.ActionNames.ChangePassword);
        }

        private void PopulateLangauges(int? selectedId = null)
        {
            ViewBag.Languages = _languageService
                .All()
                .Where(x => x.Published)
                .OrderBy(x => x.DisplayOrder)
                .ToList();
        }

    }
}