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
        public const string BuyPackage = "1";
        public const string ChangePassword = "2";
        public const string Edit = "3";
        public const string Charts = "4";
        public const string Details = "5";
        public const string TrafficDetails = "6";
        public const string TimeDetails = "7";
        public const string ConnectionDetails = "8";
        public const string PeropertiseDetails = "9";
        public const string About = "10";
        public const string PackageName = "11";
        public const string PackageTime = "12";
        public const string ClientArea = "13";


        public static readonly ClientPermissionModel FoodMenuPermission = new ClientPermissionModel { Name = FoodMenu, Description = "منو غذا" };
        public static readonly ClientPermissionModel BuyPackagePermission = new ClientPermissionModel { Name = BuyPackage, Description = "خرید تعرفه" };
        public static readonly ClientPermissionModel ChangePasswordPermission = new ClientPermissionModel { Name = ChangePassword, Description = "منو تغییر گذار واژه" };
        public static readonly ClientPermissionModel EditPermission = new ClientPermissionModel { Name = Edit, Description = "ویرایش اطلاعات" };
        public static readonly ClientPermissionModel ChartsPermission = new ClientPermissionModel { Name = Charts, Description = "نمودارها" };
        public static readonly ClientPermissionModel DetailsPermission = new ClientPermissionModel { Name = Details, Description = "منوی جزئیات" };
        public static readonly ClientPermissionModel TrafficDetailsPermission = new ClientPermissionModel { Name = TrafficDetails, Description = "جزئیات ترافیک" };
        public static readonly ClientPermissionModel TimeDetailsPermission = new ClientPermissionModel { Name = TimeDetails, Description = "جزئیات زمان" };
        public static readonly ClientPermissionModel ConnectionDetailsPermission = new ClientPermissionModel { Name = ConnectionDetails, Description = "جزئیات اتصالات" };
        public static readonly ClientPermissionModel PeropertiseDetailsPermission = new ClientPermissionModel { Name = PeropertiseDetails, Description = "جزئیات کاربر" };
        public static readonly ClientPermissionModel AboutPermission = new ClientPermissionModel { Name = About, Description = "منوی درباره ما" };
        public static readonly ClientPermissionModel PackageNamePermission = new ClientPermissionModel { Name = PackageName, Description = "داشبورد ویجت نام تعرفه" };
        public static readonly ClientPermissionModel PackageTimePermission = new ClientPermissionModel { Name = PackageTime, Description = "داشبورد ویجت زمان" };
        public static readonly ClientPermissionModel ClientPermission = new ClientPermissionModel { Name = ClientArea, Description = "پنل کاربری" };

        #endregion




        #region GetAllPermisions


        public static IEnumerable<ClientPermissionModel> GetPermision()
        {
            return new List<ClientPermissionModel>
            {
                ClientPermission,
                FoodMenuPermission,
                BuyPackagePermission,
                ChangePasswordPermission,
                EditPermission,
                ChartsPermission,
                DetailsPermission,
                TrafficDetailsPermission,
                TimeDetailsPermission,
                ConnectionDetailsPermission,
                PeropertiseDetailsPermission,
                AboutPermission,
                PackageNamePermission,
                PackageTimePermission
            };
        }



        public static IEnumerable<string> GetPermisionNames()
        {
            return new List<String>
            {
                ClientArea,
                FoodMenu,
                BuyPackage,
                ChangePassword,
                Edit,
                Charts,
                Details,
                TrafficDetails,
                TimeDetails,
                ConnectionDetails,
                PeropertiseDetails,
                About,
                PackageName,
                PackageTime
            };
        }
        #endregion

    }
}