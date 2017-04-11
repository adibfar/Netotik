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

        
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult CheckPassword(string password)
        {
            return password.IsSafePasword() ? Json(true) : Json(false);
        }
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsEmailAvailable(string email, long? Id)
        {
            var check = _applicationUserManager.CheckEmailExist(email, Id);
            return Json(!check);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult CheckNationalCode(string nationalCode, long? Id)
        {
            var check = _applicationUserManager.CheckNationalCodeExist(nationalCode, Id);
            return Json(!check);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsPhoneNumberAvailable(string phoneNumber, long? Id)
        {
            var check = _applicationUserManager.CheckIsPhoneNumberAvailable(phoneNumber, Id);
            return check ? Json(false) : Json(true);
        }


        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsResellerCompanyNameExist(string companyName,long? Id)
        {
            var check = _applicationUserManager.CheckResellerCompanyNameExist(companyName, Id);
            return check ? Json(false) : Json(true);

        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsUserNameAvailable(string userName,long? Id)
        {
            return _applicationUserManager.CheckUserNameExist(userName, Id) ? Json(false) : Json(true);
        }

    }
}