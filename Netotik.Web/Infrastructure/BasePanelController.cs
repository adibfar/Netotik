using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Netotik.Domain.Entity;
using Netotik.IocConfig;
using Netotik.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netotik.Web.Infrastructure
{
    public class BasePanelController : BaseController
    {
        #region Fields

        public readonly IApplicationUserManager _userManager;
        #endregion

        #region Constructor

        public BasePanelController()
        {
            _userManager = ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>();
        }

        #endregion
        public User UserLogined
        {
            get
            {
                return _userManager.FindUserById(long.Parse(User.Identity.GetUserId()));
            }
        }

        public IList<string> UserPermissions
        {
            get
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var roleManager = ProjectObjectFactory
                        .Container
                        .GetInstance<IApplicationRoleManager>();

                    return roleManager.FindUserPermissions(long.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId()));
                }
                return null;
            }
        }
    }
}