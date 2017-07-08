using Netotik.ViewModels.Identity.Permisson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.Security
{

    public static class AssignablePermissionToClient
    {

        #region Fields

        private static Lazy<IEnumerable<ClientPermissionModel>> _permissionsLazy =
            new Lazy<IEnumerable<ClientPermissionModel>>(GetPermision, LazyThreadSafetyMode.ExecutionAndPublication);

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
        public static IEnumerable<ClientPermissionModel> Permissions
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
        public const string HotelMenu = "1";
        public const string ChangePassMenu = "2";
        public const string PackageMenu = "3";
        public const string XYZMenu = "4";

        public static readonly ClientPermissionModel FoodMenuPermission = new ClientPermissionModel { Name = FoodMenu, Description = "منو غدا" };
        public static readonly ClientPermissionModel HotelMenuPermission = new ClientPermissionModel { Name = HotelMenu, Description = "منو هتل" };
        public static readonly ClientPermissionModel ChangePassPermission = new ClientPermissionModel { Name = ChangePassMenu, Description = "منو تغییر گذار واژه" };
        public static readonly ClientPermissionModel PackageMenuPermission = new ClientPermissionModel { Name = PackageMenu, Description = "منو تعرفه ها" };
        public static readonly ClientPermissionModel XYZMenuPermission = new ClientPermissionModel { Name = XYZMenu, Description = "توضیح مثال" };

        #endregion




        #region GetAllPermisions


        public static IEnumerable<ClientPermissionModel> GetPermision()
        {
            return new List<ClientPermissionModel>
            {
                FoodMenuPermission,
                HotelMenuPermission,
                ChangePassPermission,
                PackageMenuPermission,
                XYZMenuPermission
            };
        }



        public static IEnumerable<string> GetPermisionNames()
        {
            return new List<String>
            {
                FoodMenu,
                HotelMenu,
                ChangePassMenu,
                PackageMenu,
                XYZMenu
            };
        }
        #endregion

    }
}