using Netotik.ViewModels.Identity.Permisson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.Security
{

    public static class AssignablePermissionToRouter
    {

        #region Fields

        private static Lazy<IEnumerable<RouterPermissionModel>> _permissionsLazy =
            new Lazy<IEnumerable<RouterPermissionModel>>(GetPermision, LazyThreadSafetyMode.ExecutionAndPublication);

        private static Lazy<IEnumerable<string>> _permissionNamesLazy = new Lazy<IEnumerable<string>>(
            GetPermisionNames, LazyThreadSafetyMode.ExecutionAndPublication);
        #endregion



        #region GetAsSelectedList

        public static IEnumerable<SelectListItem> GetAsSelectListItems()
        {
            return Permissions.Select(a => new SelectListItem { Text = a.Description, Value = a.Name }).ToList();
        }
        #endregion


        #region Properties
        public static IEnumerable<RouterPermissionModel> Permissions
        {
            get
            {
                return _permissionsLazy.Value;
            }
        }

        public static IEnumerable<string> PermissionNames
        {
            get
            {
                return _permissionNamesLazy.Value;
            }
        }

        #endregion


        #region RolePerssiones


        public const string UserManagerUserList = "01";
        public const string UserManagerPlanList = "02";
        public const string UserManagerOnlines = "03";
        public const string UserManagerRegisterSettings = "04";
        public const string UserManagerReports = "05";
        public const string UserManagerClientArea = "06";


        public static readonly RouterPermissionModel UserManagerUserListPermission = new RouterPermissionModel { Name = UserManagerUserList, Description = "لیست کاربران یوزرمنیجر" };

        #endregion




        #region GetAllPermisions


        public static IEnumerable<RouterPermissionModel> GetPermision()
        {
            return new List<RouterPermissionModel>
            {
                UserManagerUserListPermission
            };
        }



        public static IEnumerable<string> GetPermisionNames()
        {
            return new List<String>
            {
                UserManagerUserList
            };
        }
        #endregion

        public static IEnumerable<RouterPermissionModel> GetUserManagerPermision()
        {
            return new List<RouterPermissionModel>
            {
                UserManagerUserListPermission
            };
        }

    }
}