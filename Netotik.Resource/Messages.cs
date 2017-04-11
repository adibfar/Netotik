using Netotik.Resources.Resources.Abstract;
using Netotik.Resources.Resources.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Resources
{
    public class Messages
    {
        private static IResourceProvider resourceProvider = new DbResourceProvider();


        public static string GetName(string name)
        {
            return (string)resourceProvider.GetResource(name, CultureInfo.CurrentUICulture.Name);
        }

        public static string AddError
        {
            get
            {
                return (string)resourceProvider.GetResource("AddError", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string AddSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("AddSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string AdminUserCreated
        {
            get
            {
                return (string)resourceProvider.GetResource("AdminUserCreated", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ChangePasswordFail
        {
            get
            {
                return (string)resourceProvider.GetResource("ChangePasswordFail", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ChangePasswordSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("ChangePasswordSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ConfirmPasswordNotValid
        {
            get
            {
                return (string)resourceProvider.GetResource("ConfirmPasswordNotValid", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ExistError
        {
            get
            {
                return (string)resourceProvider.GetResource("ExistError", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string RangeError
        {
            get
            {
                return (string)resourceProvider.GetResource("RangeError", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string NotValidError
        {
            get
            {
                return (string)resourceProvider.GetResource("NotValidError", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string RequiredError
        {
            get
            {
                return (string)resourceProvider.GetResource("RequiredError", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MaxLengthError
        {
            get
            {
                return (string)resourceProvider.GetResource("MaxLengthError", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MinLengthError
        {
            get
            {
                return (string)resourceProvider.GetResource("MinLengthError", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string InvalidDataError
        {
            get
            {
                return (string)resourceProvider.GetResource("InvalidDataError", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string LoginFail
        {
            get
            {
                return (string)resourceProvider.GetResource("LoginFail", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MissionFail
        {
            get
            {
                return (string)resourceProvider.GetResource("MissionFail", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MissionSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("MissonSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string RemoveError
        {
            get
            {
                return (string)resourceProvider.GetResource("RemoveError", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string RemoveSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("RemoveSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SettingUpdateError
        {
            get
            {
                return (string)resourceProvider.GetResource("SettingUpdateError", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SettingUpdateSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("SettingUpdateSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UpdateError
        {
            get
            {
                return (string)resourceProvider.GetResource("UpdateError", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UpdateSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("UpdateSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UpdateSettingFail
        {
            get
            {
                return (string)resourceProvider.GetResource("UpdateSettingFail", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UpdateSettingSuccess
        {
            get
            {
                return (string)resourceProvider.GetResource("UpdateSettingSuccess", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UserIsBanned
        {
            get
            {
                return (string)resourceProvider.GetResource("UserIsBanned", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UserNameOrEmailReqired
        {
            get
            {
                return (string)resourceProvider.GetResource("UserNameOrEmailReqired", CultureInfo.CurrentUICulture.Name);
            }
        }
    }
}
