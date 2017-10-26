// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Netotik.Web.Areas.MyRouter.Controllers
{
    public partial class HomeController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected HomeController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> changeImageProfile()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.changeImageProfile);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> UpdateProfile()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateProfile);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult BuySmsPackage()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BuySmsPackage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController Actions { get { return MVC.MyRouter.Home; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "MyRouter";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string MyProfile = "MyProfile";
            public readonly string ProfileData = "ProfileData";
            public readonly string changeImageProfile = "changeImageProfile";
            public readonly string UpdateProfile = "UpdateProfile";
            public readonly string MikrotikConf = "MikrotikConf";
            public readonly string TelegramBot = "TelegramBot";
            public readonly string ChangePassword = "ChangePassword";
            public readonly string Sms = "Sms";
            public readonly string BuySmsPackage = "BuySmsPackage";
            public readonly string LoadProfiles = "LoadProfiles";
            public readonly string GetUserCount = "GetUserCount";
            public readonly string GetPackageCount = "GetPackageCount";
            public readonly string GetActiceSessionCount = "GetActiceSessionCount";
            public readonly string GetRouterDateTime = "GetRouterDateTime";
            public readonly string GetLastProfile = "GetLastProfile";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string MyProfile = "MyProfile";
            public const string ProfileData = "ProfileData";
            public const string changeImageProfile = "changeImageProfile";
            public const string UpdateProfile = "UpdateProfile";
            public const string MikrotikConf = "MikrotikConf";
            public const string TelegramBot = "TelegramBot";
            public const string ChangePassword = "ChangePassword";
            public const string Sms = "Sms";
            public const string BuySmsPackage = "BuySmsPackage";
            public const string LoadProfiles = "LoadProfiles";
            public const string GetUserCount = "GetUserCount";
            public const string GetPackageCount = "GetPackageCount";
            public const string GetActiceSessionCount = "GetActiceSessionCount";
            public const string GetRouterDateTime = "GetRouterDateTime";
            public const string GetLastProfile = "GetLastProfile";
            public const string RedirectToLocal = "RedirectToLocal";
        }


        static readonly ActionParamsClass_changeImageProfile s_params_changeImageProfile = new ActionParamsClass_changeImageProfile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_changeImageProfile changeImageProfileParams { get { return s_params_changeImageProfile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_changeImageProfile
        {
            public readonly string image = "image";
        }
        static readonly ActionParamsClass_UpdateProfile s_params_UpdateProfile = new ActionParamsClass_UpdateProfile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateProfile UpdateProfileParams { get { return s_params_UpdateProfile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateProfile
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_TelegramBot s_params_TelegramBot = new ActionParamsClass_TelegramBot();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_TelegramBot TelegramBotParams { get { return s_params_TelegramBot; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_TelegramBot
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_MikrotikConf s_params_MikrotikConf = new ActionParamsClass_MikrotikConf();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_MikrotikConf MikrotikConfParams { get { return s_params_MikrotikConf; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_MikrotikConf
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ChangePassword s_params_ChangePassword = new ActionParamsClass_ChangePassword();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ChangePassword ChangePasswordParams { get { return s_params_ChangePassword; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ChangePassword
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Sms s_params_Sms = new ActionParamsClass_Sms();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Sms SmsParams { get { return s_params_Sms; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Sms
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_BuySmsPackage s_params_BuySmsPackage = new ActionParamsClass_BuySmsPackage();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_BuySmsPackage BuySmsPackageParams { get { return s_params_BuySmsPackage; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_BuySmsPackage
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_RedirectToLocal s_params_RedirectToLocal = new ActionParamsClass_RedirectToLocal();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RedirectToLocal RedirectToLocalParams { get { return s_params_RedirectToLocal; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RedirectToLocal
        {
            public readonly string returnUrl = "returnUrl";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _ImageProfile = "_ImageProfile";
                public readonly string _IndexChart = "_IndexChart";
                public readonly string _ProfileData = "_ProfileData";
                public readonly string _Profiles = "_Profiles";
                public readonly string ChangePassword = "ChangePassword";
                public readonly string Index = "Index";
                public readonly string MikrotikConf = "MikrotikConf";
                public readonly string MyProfile = "MyProfile";
                public readonly string Sms = "Sms";
                public readonly string TelegramBot = "TelegramBot";
            }
            public readonly string _ImageProfile = "~/Areas/MyRouter/Views/Home/_ImageProfile.cshtml";
            public readonly string _IndexChart = "~/Areas/MyRouter/Views/Home/_IndexChart.cshtml";
            public readonly string _ProfileData = "~/Areas/MyRouter/Views/Home/_ProfileData.cshtml";
            public readonly string _Profiles = "~/Areas/MyRouter/Views/Home/_Profiles.cshtml";
            public readonly string ChangePassword = "~/Areas/MyRouter/Views/Home/ChangePassword.cshtml";
            public readonly string Index = "~/Areas/MyRouter/Views/Home/Index.cshtml";
            public readonly string MikrotikConf = "~/Areas/MyRouter/Views/Home/MikrotikConf.cshtml";
            public readonly string MyProfile = "~/Areas/MyRouter/Views/Home/MyProfile.cshtml";
            public readonly string Sms = "~/Areas/MyRouter/Views/Home/Sms.cshtml";
            public readonly string TelegramBot = "~/Areas/MyRouter/Views/Home/TelegramBot.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_HomeController : Netotik.Web.Areas.MyRouter.Controllers.HomeController
    {
        public T4MVC_HomeController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void MyProfileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MyProfile()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MyProfile);
            MyProfileOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ProfileDataOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ProfileData()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ProfileData);
            ProfileDataOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void changeImageProfileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Web.HttpPostedFileBase image);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> changeImageProfile(System.Web.HttpPostedFileBase image)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.changeImageProfile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "image", image);
            changeImageProfileOverride(callInfo, image);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void UpdateProfileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Identity.UserRouter.ProfileModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> UpdateProfile(Netotik.ViewModels.Identity.UserRouter.ProfileModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateProfile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            UpdateProfileOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void MikrotikConfOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MikrotikConf()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MikrotikConf);
            MikrotikConfOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void TelegramBotOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult TelegramBot()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.TelegramBot);
            TelegramBotOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void TelegramBotOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Identity.UserRouter.TelegramBotModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> TelegramBot(Netotik.ViewModels.Identity.UserRouter.TelegramBotModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.TelegramBot);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            TelegramBotOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void MikrotikConfOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Identity.UserRouter.MikrotikConfModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> MikrotikConf(Netotik.ViewModels.Identity.UserRouter.MikrotikConfModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MikrotikConf);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            MikrotikConfOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void ChangePasswordOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangePassword()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangePassword);
            ChangePasswordOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ChangePasswordOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Identity.UserRouter.ChangePasswordModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ChangePassword(Netotik.ViewModels.Identity.UserRouter.ChangePasswordModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangePassword);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ChangePasswordOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void SmsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Sms()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Sms);
            SmsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SmsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Identity.UserRouter.SmsModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Sms(Netotik.ViewModels.Identity.UserRouter.SmsModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Sms);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SmsOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void BuySmsPackageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult BuySmsPackage(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BuySmsPackage);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            BuySmsPackageOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void LoadProfilesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult LoadProfiles()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LoadProfiles);
            LoadProfilesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetUserCountOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetUserCount()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetUserCount);
            GetUserCountOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetPackageCountOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetPackageCount()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetPackageCount);
            GetPackageCountOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetActiceSessionCountOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetActiceSessionCount()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetActiceSessionCount);
            GetActiceSessionCountOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetRouterDateTimeOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetRouterDateTime()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRouterDateTime);
            GetRouterDateTimeOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetLastProfileOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetLastProfile()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetLastProfile);
            GetLastProfileOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RedirectToLocalOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string returnUrl);

        [NonAction]
        public override System.Web.Mvc.ActionResult RedirectToLocal(string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            RedirectToLocalOverride(callInfo, returnUrl);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
