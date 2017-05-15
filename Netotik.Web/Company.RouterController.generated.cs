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
namespace Netotik.Web.Areas.Company.Controllers
{
    public partial class RouterController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RouterController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult UpdateRouter()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateRouter);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UpdateRouterCheck()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateRouterCheck);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult WirelessDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WirelessDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InterfaceDisable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InterfaceDisable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult WirelessEnable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WirelessEnable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult WirelessDisable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WirelessDisable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InterfaceEnable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InterfaceEnable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InterfaceDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InterfaceDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RouterController Actions { get { return MVC.Company.Router; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Company";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Router";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Router";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Info = "Info";
            public readonly string UpdateRouter = "UpdateRouter";
            public readonly string UpdateRouterCheck = "UpdateRouterCheck";
            public readonly string PPP = "PPP";
            public readonly string Interfaces = "Interfaces";
            public readonly string Wireless = "Wireless";
            public readonly string WirelessDetails = "WirelessDetails";
            public readonly string InterfaceDisable = "InterfaceDisable";
            public readonly string WirelessEnable = "WirelessEnable";
            public readonly string WirelessDisable = "WirelessDisable";
            public readonly string InterfaceEnable = "InterfaceEnable";
            public readonly string InterfaceDetails = "InterfaceDetails";
            public readonly string Hotspot_Temp = "Hotspot_Temp";
            public readonly string RouterSetting = "RouterSetting";
            public readonly string Reboot = "Reboot";
            public readonly string ResetRouter = "ResetRouter";
            public readonly string RestoreUsermanager = "RestoreUsermanager";
            public readonly string RestoreRouter = "RestoreRouter";
            public readonly string ResetUsermanager = "ResetUsermanager";
            public readonly string RemoveLogs = "RemoveLogs";
            public readonly string BackupUsermanager = "BackupUsermanager";
            public readonly string BackupRouter = "BackupRouter";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Info = "Info";
            public const string UpdateRouter = "UpdateRouter";
            public const string UpdateRouterCheck = "UpdateRouterCheck";
            public const string PPP = "PPP";
            public const string Interfaces = "Interfaces";
            public const string Wireless = "Wireless";
            public const string WirelessDetails = "WirelessDetails";
            public const string InterfaceDisable = "InterfaceDisable";
            public const string WirelessEnable = "WirelessEnable";
            public const string WirelessDisable = "WirelessDisable";
            public const string InterfaceEnable = "InterfaceEnable";
            public const string InterfaceDetails = "InterfaceDetails";
            public const string Hotspot_Temp = "Hotspot_Temp";
            public const string RouterSetting = "RouterSetting";
            public const string Reboot = "Reboot";
            public const string ResetRouter = "ResetRouter";
            public const string RestoreUsermanager = "RestoreUsermanager";
            public const string RestoreRouter = "RestoreRouter";
            public const string ResetUsermanager = "ResetUsermanager";
            public const string RemoveLogs = "RemoveLogs";
            public const string BackupUsermanager = "BackupUsermanager";
            public const string BackupRouter = "BackupRouter";
            public const string RedirectToLocal = "RedirectToLocal";
        }


        static readonly ActionParamsClass_UpdateRouter s_params_UpdateRouter = new ActionParamsClass_UpdateRouter();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateRouter UpdateRouterParams { get { return s_params_UpdateRouter; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateRouter
        {
            public readonly string ReturnURL = "ReturnURL";
        }
        static readonly ActionParamsClass_UpdateRouterCheck s_params_UpdateRouterCheck = new ActionParamsClass_UpdateRouterCheck();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateRouterCheck UpdateRouterCheckParams { get { return s_params_UpdateRouterCheck; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateRouterCheck
        {
            public readonly string ReturnURL = "ReturnURL";
        }
        static readonly ActionParamsClass_WirelessDetails s_params_WirelessDetails = new ActionParamsClass_WirelessDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_WirelessDetails WirelessDetailsParams { get { return s_params_WirelessDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_WirelessDetails
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_InterfaceDisable s_params_InterfaceDisable = new ActionParamsClass_InterfaceDisable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InterfaceDisable InterfaceDisableParams { get { return s_params_InterfaceDisable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InterfaceDisable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_WirelessEnable s_params_WirelessEnable = new ActionParamsClass_WirelessEnable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_WirelessEnable WirelessEnableParams { get { return s_params_WirelessEnable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_WirelessEnable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_WirelessDisable s_params_WirelessDisable = new ActionParamsClass_WirelessDisable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_WirelessDisable WirelessDisableParams { get { return s_params_WirelessDisable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_WirelessDisable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_InterfaceEnable s_params_InterfaceEnable = new ActionParamsClass_InterfaceEnable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InterfaceEnable InterfaceEnableParams { get { return s_params_InterfaceEnable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InterfaceEnable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_InterfaceDetails s_params_InterfaceDetails = new ActionParamsClass_InterfaceDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InterfaceDetails InterfaceDetailsParams { get { return s_params_InterfaceDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InterfaceDetails
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
                public readonly string _Table = "_Table";
                public readonly string Hotspot_Temp = "Hotspot_Temp";
                public readonly string Info = "Info";
                public readonly string InterfaceDetails = "InterfaceDetails";
                public readonly string Interfaces = "Interfaces";
                public readonly string PPP = "PPP";
                public readonly string RouterSetting = "RouterSetting";
                public readonly string Wireless = "Wireless";
                public readonly string WirelessDetails = "WirelessDetails";
            }
            public readonly string _Table = "~/Areas/Company/Views/Router/_Table.cshtml";
            public readonly string Hotspot_Temp = "~/Areas/Company/Views/Router/Hotspot_Temp.cshtml";
            public readonly string Info = "~/Areas/Company/Views/Router/Info.cshtml";
            public readonly string InterfaceDetails = "~/Areas/Company/Views/Router/InterfaceDetails.cshtml";
            public readonly string Interfaces = "~/Areas/Company/Views/Router/Interfaces.cshtml";
            public readonly string PPP = "~/Areas/Company/Views/Router/PPP.cshtml";
            public readonly string RouterSetting = "~/Areas/Company/Views/Router/RouterSetting.cshtml";
            public readonly string Wireless = "~/Areas/Company/Views/Router/Wireless.cshtml";
            public readonly string WirelessDetails = "~/Areas/Company/Views/Router/WirelessDetails.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RouterController : Netotik.Web.Areas.Company.Controllers.RouterController
    {
        public T4MVC_RouterController() : base(Dummy.Instance) { }

        [NonAction]
        partial void InfoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Info()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Info);
            InfoOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UpdateRouterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string ReturnURL);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateRouter(string ReturnURL)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateRouter);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ReturnURL", ReturnURL);
            UpdateRouterOverride(callInfo, ReturnURL);
            return callInfo;
        }

        [NonAction]
        partial void UpdateRouterCheckOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string ReturnURL);

        [NonAction]
        public override System.Web.Mvc.ActionResult UpdateRouterCheck(string ReturnURL)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateRouterCheck);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ReturnURL", ReturnURL);
            UpdateRouterCheckOverride(callInfo, ReturnURL);
            return callInfo;
        }

        [NonAction]
        partial void PPPOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PPP()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PPP);
            PPPOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InterfacesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Interfaces()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Interfaces);
            InterfacesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void WirelessOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Wireless()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Wireless);
            WirelessOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void WirelessDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult WirelessDetails(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WirelessDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            WirelessDetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void InterfaceDisableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult InterfaceDisable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InterfaceDisable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InterfaceDisableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void WirelessEnableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult WirelessEnable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WirelessEnable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            WirelessEnableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void WirelessDisableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult WirelessDisable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WirelessDisable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            WirelessDisableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void InterfaceEnableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult InterfaceEnable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InterfaceEnable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InterfaceEnableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void InterfaceDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult InterfaceDetails(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InterfaceDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InterfaceDetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void Hotspot_TempOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Hotspot_Temp()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Hotspot_Temp);
            Hotspot_TempOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RouterSettingOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult RouterSetting()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RouterSetting);
            RouterSettingOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RebootOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Reboot()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Reboot);
            RebootOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ResetRouterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ResetRouter()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResetRouter);
            ResetRouterOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RestoreUsermanagerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult RestoreUsermanager()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RestoreUsermanager);
            RestoreUsermanagerOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RestoreRouterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult RestoreRouter()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RestoreRouter);
            RestoreRouterOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ResetUsermanagerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ResetUsermanager()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResetUsermanager);
            ResetUsermanagerOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RemoveLogsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult RemoveLogs()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemoveLogs);
            RemoveLogsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void BackupUsermanagerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult BackupUsermanager()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BackupUsermanager);
            BackupUsermanagerOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void BackupRouterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult BackupRouter()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BackupRouter);
            BackupRouterOverride(callInfo);
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
