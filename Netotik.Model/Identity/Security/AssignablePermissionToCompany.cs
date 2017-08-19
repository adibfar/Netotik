using Netotik.ViewModels.Identity.Permisson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.Security
{

    public static class AssignablePermissionToCompany
    {

        #region Fields

        private static Lazy<IEnumerable<CompanyPermissionModel>> _permissionsLazy =
            new Lazy<IEnumerable<CompanyPermissionModel>>(GetPermision, LazyThreadSafetyMode.ExecutionAndPublication);

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
        public static IEnumerable<CompanyPermissionModel> Permissions
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


        public const string FoodMenu = "0";


        public static readonly CompanyPermissionModel FoodMenuPermission = new CompanyPermissionModel { Name = FoodMenu, Description = "منو غدا" };

        #endregion




        #region GetAllPermisions


        public static IEnumerable<CompanyPermissionModel> GetPermision()
        {
            return new List<CompanyPermissionModel>
            {
                FoodMenuPermission
            };
        }



        public static IEnumerable<string> GetPermisionNames()
        {
            return new List<String>
            {
                FoodMenu
            };
        }
        #endregion

    }
}