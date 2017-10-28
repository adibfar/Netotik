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
        public virtual System.Web.Mvc.ActionResult NatRemove()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NatRemove);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult NatEnable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NatEnable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult NatDisable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NatDisable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AccessDisable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessDisable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AccessEnable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessEnable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RouterController Actions { get { return MVC.MyRouter.Router; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "MyRouter";
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
            public readonly string RouterSetting = "RouterSetting";
            public readonly string Reboot = "Reboot";
            public readonly string ResetRouter = "ResetRouter";
            public readonly string RestoreUsermanager = "RestoreUsermanager";
            public readonly string RestoreRouter = "RestoreRouter";
            public readonly string ResetUsermanager = "ResetUsermanager";
            public readonly string RemoveLogs = "RemoveLogs";
            public readonly string BackupUsermanager = "BackupUsermanager";
            public readonly string BackupRouter = "BackupRouter";
            public readonly string Nat = "Nat";
            public readonly string NatRemove = "NatRemove";
            public readonly string NatEnable = "NatEnable";
            public readonly string NatDisable = "NatDisable";
            public readonly string WebSitesLogs = "WebSitesLogs";
            public readonly string GetRouterResource = "GetRouterResource";
            public readonly string GetRouterIdentity = "GetRouterIdentity";
            public readonly string GetRouterLicense = "GetRouterLicense";
            public readonly string GetRouterPackageUpdate = "GetRouterPackageUpdate";
            public readonly string GetRouterClock = "GetRouterClock";
            public readonly string GetRouterBoard = "GetRouterBoard";
            public readonly string Access = "Access";
            public readonly string AccessDisable = "AccessDisable";
            public readonly string AccessEnable = "AccessEnable";
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
            public const string RouterSetting = "RouterSetting";
            public const string Reboot = "Reboot";
            public const string ResetRouter = "ResetRouter";
            public const string RestoreUsermanager = "RestoreUsermanager";
            public const string RestoreRouter = "RestoreRouter";
            public const string ResetUsermanager = "ResetUsermanager";
            public const string RemoveLogs = "RemoveLogs";
            public const string BackupUsermanager = "BackupUsermanager";
            public const string BackupRouter = "BackupRouter";
            public const string Nat = "Nat";
            public const string NatRemove = "NatRemove";
            public const string NatEnable = "NatEnable";
            public const string NatDisable = "NatDisable";
            public const string WebSitesLogs = "WebSitesLogs";
            public const string GetRouterResource = "GetRouterResource";
            public const string GetRouterIdentity = "GetRouterIdentity";
            public const string GetRouterLicense = "GetRouterLicense";
            public const string GetRouterPackageUpdate = "GetRouterPackageUpdate";
            public const string GetRouterClock = "GetRouterClock";
            public const string GetRouterBoard = "GetRouterBoard";
            public const string Access = "Access";
            public const string AccessDisable = "AccessDisable";
            public const string AccessEnable = "AccessEnable";
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
        static readonly ActionParamsClass_Nat s_params_Nat = new ActionParamsClass_Nat();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Nat NatParams { get { return s_params_Nat; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Nat
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_NatRemove s_params_NatRemove = new ActionParamsClass_NatRemove();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NatRemove NatRemoveParams { get { return s_params_NatRemove; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NatRemove
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_NatEnable s_params_NatEnable = new ActionParamsClass_NatEnable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NatEnable NatEnableParams { get { return s_params_NatEnable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NatEnable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_NatDisable s_params_NatDisable = new ActionParamsClass_NatDisable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NatDisable NatDisableParams { get { return s_params_NatDisable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NatDisable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_AccessDisable s_params_AccessDisable = new ActionParamsClass_AccessDisable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AccessDisable AccessDisableParams { get { return s_params_AccessDisable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AccessDisable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_AccessEnable s_params_AccessEnable = new ActionParamsClass_AccessEnable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AccessEnable AccessEnableParams { get { return s_params_AccessEnable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AccessEnable
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
                public readonly string Access = "Access";
                public readonly string Info = "Info";
                public readonly string InterfaceDetails = "InterfaceDetails";
                public readonly string Interfaces = "Interfaces";
                public readonly string Nat = "Nat";
                public readonly string PPP = "PPP";
                public readonly string RouterSetting = "RouterSetting";
                public readonly string WebSitesLogs = "WebSitesLogs";
                public readonly string Wireless = "Wireless";
                public readonly string WirelessDetails = "WirelessDetails";
            }
            public readonly string _Table = "~/Areas/MyRouter/Views/Router/_Table.cshtml";
            public readonly string Access = "~/Areas/MyRouter/Views/Router/Access.cshtml";
            public readonly string Info = "~/Areas/MyRouter/Views/Router/Info.cshtml";
            public readonly string InterfaceDetails = "~/Areas/MyRouter/Views/Router/InterfaceDetails.cshtml";
            public readonly string Interfaces = "~/Areas/MyRouter/Views/Router/Interfaces.cshtml";
            public readonly string Nat = "~/Areas/MyRouter/Views/Router/Nat.cshtml";
            public readonly string PPP = "~/Areas/MyRouter/Views/Router/PPP.cshtml";
            public readonly string RouterSetting = "~/Areas/MyRouter/Views/Router/RouterSetting.cshtml";
            public readonly string WebSitesLogs = "~/Areas/MyRouter/Views/Router/WebSitesLogs.cshtml";
            public readonly string Wireless = "~/Areas/MyRouter/Views/Router/Wireless.cshtml";
            public readonly string WirelessDetails = "~/Areas/MyRouter/Views/Router/WirelessDetails.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RouterController : Netotik.Web.Areas.MyRouter.Controllers.RouterController
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
        partial void NatOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Mikrotik.Router_NatModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Nat(Netotik.ViewModels.Mikrotik.Router_NatModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Nat);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            NatOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void NatOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Nat()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Nat);
            NatOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void NatRemoveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult NatRemove(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NatRemove);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            NatRemoveOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void NatEnableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult NatEnable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NatEnable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            NatEnableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void NatDisableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult NatDisable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NatDisable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            NatDisableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void WebSitesLogsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult WebSitesLogs()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.WebSitesLogs);
            WebSitesLogsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetRouterResourceOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetRouterResource()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRouterResource);
            GetRouterResourceOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetRouterIdentityOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetRouterIdentity()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRouterIdentity);
            GetRouterIdentityOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetRouterLicenseOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetRouterLicense()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRouterLicense);
            GetRouterLicenseOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetRouterPackageUpdateOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetRouterPackageUpdate()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRouterPackageUpdate);
            GetRouterPackageUpdateOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetRouterClockOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetRouterClock()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRouterClock);
            GetRouterClockOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetRouterBoardOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetRouterBoard()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRouterBoard);
            GetRouterBoardOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AccessOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Access()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Access);
            AccessOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AccessDisableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessDisable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessDisable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AccessDisableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AccessEnableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessEnable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessEnable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AccessEnableOverride(callInfo, id);
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
