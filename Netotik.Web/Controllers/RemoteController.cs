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

        #region Email
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
        public virtual JsonResult IsCompanyEmailAvailable(string email, long? Id)
        {
            var check = _applicationUserManager.CheckCompanyEmailExist(email, Id);
            return Json(!check);
        }

        #endregion

        #region PhoneNumber
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
        public virtual JsonResult IsCompanyPhoneNumberAvailable(string phoneNumber, long? Id, long? Resellerid)
        {
            var check = _applicationUserManager.CheckCompanyPhoneNumberExist(phoneNumber, Id , Resellerid);
            return check ? Json(false) : Json(true);
        }
        #endregion

        #region NationalCode
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsResellerNationalCodeAvailable(string nationalCode, long? Id)
        {
            var check = _applicationUserManager.CheckResellerNationalCodeExist(nationalCode, Id);
            var check2 = _applicationUserManager.IsNationalCodeAvailableAlgoritm(nationalCode);
            if (!check == check2 == true)
                return Json(true);
            else
                return Json(false);
        }


        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsCompanyNationalCodeAvailable(string nationalCode, long? Id, long? Resellerid)
        {
            var check = _applicationUserManager.CheckCompanyNationalCodeExist(nationalCode, Id, Resellerid);
            var check2 = _applicationUserManager.IsNationalCodeAvailableAlgoritm(nationalCode);
            if (!check == check2 == true)
                return Json(true);
            else
                return Json(false);
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
            var check = _applicationUserManager.CheckResellerCompanyNameExist(ResellerCode, Id);
            return check ? Json(false) : Json(true);

        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult IsCompanyCodeAvailable(string CompanyCode, long? Id,long? Resellerid)
        {
            var check = _applicationUserManager.CheckCompanyCompanyNameExist(CompanyCode, Id, Resellerid);
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
    }
}