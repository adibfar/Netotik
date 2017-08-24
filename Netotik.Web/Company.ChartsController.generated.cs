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
    public partial class ChartsController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ChartsController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ChartsController Actions { get { return MVC.Company.Charts; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Company";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Charts";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Charts";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string MaxTimeUsage = "MaxTimeUsage";
            public readonly string MaxTrafficUsage = "MaxTrafficUsage";
            public readonly string MinTrafficUsage = "MinTrafficUsage";
            public readonly string UsermanagerUsage = "UsermanagerUsage";
            public readonly string Sessions = "Sessions";
            public readonly string Logs = "Logs";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string MaxTimeUsage = "MaxTimeUsage";
            public const string MaxTrafficUsage = "MaxTrafficUsage";
            public const string MinTrafficUsage = "MinTrafficUsage";
            public const string UsermanagerUsage = "UsermanagerUsage";
            public const string Sessions = "Sessions";
            public const string Logs = "Logs";
            public const string RedirectToLocal = "RedirectToLocal";
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
                public readonly string Logs = "Logs";
                public readonly string MaxTimeUsage = "MaxTimeUsage";
                public readonly string MaxTrafficUsage = "MaxTrafficUsage";
                public readonly string MinTrafficUsage = "MinTrafficUsage";
                public readonly string Sessions = "Sessions";
                public readonly string UsermanagerUsage = "UsermanagerUsage";
            }
            public readonly string Logs = "~/Areas/Company/Views/Charts/Logs.cshtml";
            public readonly string MaxTimeUsage = "~/Areas/Company/Views/Charts/MaxTimeUsage.cshtml";
            public readonly string MaxTrafficUsage = "~/Areas/Company/Views/Charts/MaxTrafficUsage.cshtml";
            public readonly string MinTrafficUsage = "~/Areas/Company/Views/Charts/MinTrafficUsage.cshtml";
            public readonly string Sessions = "~/Areas/Company/Views/Charts/Sessions.cshtml";
            public readonly string UsermanagerUsage = "~/Areas/Company/Views/Charts/UsermanagerUsage.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ChartsController : Netotik.Web.Areas.Company.Controllers.ChartsController
    {
        public T4MVC_ChartsController() : base(Dummy.Instance) { }

        [NonAction]
        partial void MaxTimeUsageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MaxTimeUsage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MaxTimeUsage);
            MaxTimeUsageOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void MaxTrafficUsageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MaxTrafficUsage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MaxTrafficUsage);
            MaxTrafficUsageOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void MinTrafficUsageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MinTrafficUsage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MinTrafficUsage);
            MinTrafficUsageOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UsermanagerUsageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult UsermanagerUsage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UsermanagerUsage);
            UsermanagerUsageOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SessionsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Sessions()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Sessions);
            SessionsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void LogsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Logs()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Logs);
            LogsOverride(callInfo);
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