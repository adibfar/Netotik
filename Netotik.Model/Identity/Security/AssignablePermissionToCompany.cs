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

        public const string RouterInfo = "11";
        public const string RouterActions = "12";
        public const string WebsitesLogs = "13";

        public const string HotspotsOnline = "21";
        public const string HotspotsAccess = "22";

        public const string TelegramBot = "31";
        public const string Sms = "32";
        public const string MikrotikConnectionSetting = "33";


        public static readonly RouterPermissionModel UserManagerUserListPermission = new RouterPermissionModel { Name = UserManagerUserList, Description = "لیست کاربران یوزرمنیجر" };
        public static readonly RouterPermissionModel UserManagerPlanListPermission = new RouterPermissionModel { Name = UserManagerPlanList, Description = "لیست تعرفه های یوزرمنیجر" };
        public static readonly RouterPermissionModel UserManagerOnlinesPermission = new RouterPermissionModel { Name = UserManagerOnlines, Description = "لیست کاربران آنلاین یوزرمنیجر" };
        public static readonly RouterPermissionModel UserManagerRegisterSettingsPermission = new RouterPermissionModel { Name = UserManagerRegisterSettings, Description = "تنظیمات منوی ثبت نام یوزرمنیجر" };
        public static readonly RouterPermissionModel UserManagerReportsPermission = new RouterPermissionModel { Name = UserManagerReports, Description = "گزارشات یوزمنیجر" };
        public static readonly RouterPermissionModel UserManagerClientAreaPermission = new RouterPermissionModel { Name = UserManagerClientArea, Description = "تنظیمات پنل کلاینت" };

        public static readonly RouterPermissionModel RouterInfoPermission = new RouterPermissionModel { Name = RouterInfo, Description = "اطلاعات روتر" };
        public static readonly RouterPermissionModel RouterActionsPermission = new RouterPermissionModel { Name = RouterActions, Description = "عملیات های روتر" };
        public static readonly RouterPermissionModel WebsitesLogsPermission = new RouterPermissionModel { Name = WebsitesLogs, Description = "لاگ سایت های بازدید شده" };

        public static readonly RouterPermissionModel HotspotsOnlinePermission = new RouterPermissionModel { Name = HotspotsOnline, Description = "لیست کاربران آنلاین هات اسپات" };
        public static readonly RouterPermissionModel HotspotsAccessPermission = new RouterPermissionModel { Name = HotspotsAccess, Description = "دسترسی ها در هات اسپات" };

        public static readonly RouterPermissionModel TelegramBotPermission = new RouterPermissionModel { Name = TelegramBot, Description = "ربات تلگرام" };
        public static readonly RouterPermissionModel SmsPermission = new RouterPermissionModel { Name = Sms, Description = "پیامک" };
        public static readonly RouterPermissionModel MikrotikConnectionSettingPermission = new RouterPermissionModel { Name = MikrotikConnectionSetting, Description = "تنظیمات اتصال میکروتیک" };

        #endregion




        #region GetAllPermisions


        public static IEnumerable<RouterPermissionModel> GetPermision()
        {
            return new List<RouterPermissionModel>
            {
                UserManagerUserListPermission,
                UserManagerPlanListPermission,
                UserManagerOnlinesPermission,
                UserManagerRegisterSettingsPermission,
                UserManagerReportsPermission,
                UserManagerClientAreaPermission,
                RouterActionsPermission,
                RouterInfoPermission,
                HotspotsAccessPermission,
                HotspotsOnlinePermission,
                TelegramBotPermission,
                SmsPermission,
                MikrotikConnectionSettingPermission,
                WebsitesLogsPermission
            };
        }



        public static IEnumerable<string> GetPermisionNames()
        {
            return new List<String>
            {
                UserManagerUserList,
                UserManagerPlanList,
                UserManagerOnlines,
                UserManagerRegisterSettings,
                UserManagerReports,
                UserManagerClientArea,
                RouterInfo,
                RouterActions,
                HotspotsOnline,
                HotspotsAccess,
                TelegramBot,
                Sms,
                MikrotikConnectionSetting,
                WebsitesLogs
            };
        }
        #endregion

        public static IEnumerable<RouterPermissionModel> GetUserManagerMenuPermision()
        {
            return new List<RouterPermissionModel>
            {
                UserManagerUserListPermission,
                UserManagerPlanListPermission,
                UserManagerOnlinesPermission,
                UserManagerRegisterSettingsPermission,
                UserManagerReportsPermission,
                UserManagerClientAreaPermission
            };
        }

        public static IEnumerable<RouterPermissionModel> GetRouterMenuPermision()
        {
            return new List<RouterPermissionModel>
            {
                RouterActionsPermission,
                RouterInfoPermission,
                WebsitesLogsPermission
            };
        }

        public static IEnumerable<RouterPermissionModel> GetHotspotMenuPermision()
        {
            return new List<RouterPermissionModel>
            {
                HotspotsAccessPermission,
                HotspotsOnlinePermission
            };
        }

    }
}