using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.Common.Security;
using System.Threading.Tasks;
using Netotik.Web.Infrastructure;
using CaptchaMvc.Attributes;
using Netotik.Services.Identity;
using Microsoft.Owin.Security;
using Netotik.ViewModels.Identity.Account;
using Mvc.Mailer;
using System.Web.UI;
using System.Net;
using Microsoft.AspNet.Identity.Owin;
using Netotik.Common.Filters;
using Microsoft.AspNet.Identity;
using Netotik.Common.Controller;
using Netotik.ViewModels.Identity.Security;

namespace Netotik.Web.Controllers
{
    public partial class RemoteController : BaseController
    {
        #region Fields

        private readonly IApplicationUserManager _applicationUserManager;
        #endregion

        #region Constructor

        public RemoteController(IApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        #endregion

        #region Email

        [HttpPost]
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanViewAdminPanel)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsAdminEmailAvailable(string email, long? Id)
        {
            var check = _applicationUserManager.CheckAdminEmailExist(email, Id);
            return Json(!check);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsResellerEmailAvailable(string email, long? Id)
        {
            var check = _applicationUserManager.CheckResellerEmailExist(email, Id);
            return Json(!check);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsRouterEmailAvailable(string email, long? Id)
        {
            var check = _applicationUserManager.CheckRouterEmailExist(email, Id);
            return Json(!check);
        }

        #endregion

        #region PhoneNumber
        [HttpPost]
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanViewAdminPanel)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsAdminPhoneNumberAvailable(string phoneNumber, long? Id)
        {
            var check = _applicationUserManager.CheckAdminPhoneNumberExist(phoneNumber, Id);
            return Json(!check);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsResellerPhoneNumberAvailable(string phoneNumber, long? Id)
        {
            var check = _applicationUserManager.CheckResellerPhoneNumberExist(phoneNumber, Id);
            return check ? Json(false) : Json(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsRouterPhoneNumberAvailable(string phoneNumber, long? Id, long? Resellerid)
        {
            var check = _applicationUserManager.CheckRouterPhoneNumberExist(phoneNumber, Id);
            return check ? Json(false) : Json(true);
        }
        #endregion

        #region NationalCode
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsResellerNationalCodeAvailable(string nationalCode, long? Id)
        {
            /*
            if (_applicationUserManager.CheckResellerNationalCodeExist(nationalCode, Id))
                return Json(false);
                */
            if (!_applicationUserManager.IsNationalCodeValid(nationalCode))
                return Json(false);

            return Json(true);

        }


        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsRouterNationalCodeAvailable(string nationalCode, long? Id, long? Resellerid)
        {
            /*
            if (_applicationUserManager.CheckCompanyNationalCodeExist(nationalCode, Id, Resellerid))
                return Json(false);
                */
            if (!_applicationUserManager.IsNationalCodeValid(nationalCode))
                return Json(false);

            return Json(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsNationalCodeValid(string nationalCode)
        {
            if (!_applicationUserManager.IsNationalCodeValid(nationalCode))
                return Json(false);

            return Json(true);
        }
        #endregion

        #region Username
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsUserNameAvailable(string userName, long? Id)
        {
            return _applicationUserManager.CheckUserNameExist(userName, Id) ? Json(false) : Json(true);
        }
        #endregion

        #region _Code
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsResellerCodeAvailable(string ResellerCode, long? Id)
        {
            var check = _applicationUserManager.CheckResellerRouterNameExist(ResellerCode, Id);
            return check ? Json(false) : Json(true);

        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsRouterCodeAvailable(string RouterCode, long? Id, long? Resellerid)
        {
            var check = _applicationUserManager.CheckRouterRouterCodeExist(RouterCode, Id);
            return check ? Json(false) : Json(true);

        }
        #endregion

        #region Password
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult CheckPassword(string password)
        {
            return password.IsSafePasword() ? Json(true) : Json(false);
        }
        #endregion


        #region SmsCode
        [HttpPost]
        [AllowAnonymous]
        public virtual JsonResult SmsCodeIsValid(string RegisterWithSmsCode,long? id)
        {
            var check = _applicationUserManager.SmsCodeIsValid(RegisterWithSmsCode.Replace("ي", "ی"), id);
            return check ? Json(false) : Json(true);
        }
        #endregion
    }
}