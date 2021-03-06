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
namespace Netotik.Web.Controllers
{
    public partial class GetRouterTemplateController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected GetRouterTemplateController(Dummy d) { }

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
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetLogin()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetLogin);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetLogout()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetLogout);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetStatus()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetStatus);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetError()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetError);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetAlogin()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetAlogin);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public GetRouterTemplateController Actions { get { return MVC.GetRouterTemplate; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "GetRouterTemplate";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "GetRouterTemplate";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string GetLogin = "GetLogin";
            public readonly string GetLogout = "GetLogout";
            public readonly string GetStatus = "GetStatus";
            public readonly string GetError = "GetError";
            public readonly string GetAlogin = "GetAlogin";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string GetLogin = "GetLogin";
            public const string GetLogout = "GetLogout";
            public const string GetStatus = "GetStatus";
            public const string GetError = "GetError";
            public const string GetAlogin = "GetAlogin";
        }


        static readonly ActionParamsClass_GetLogin s_params_GetLogin = new ActionParamsClass_GetLogin();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetLogin GetLoginParams { get { return s_params_GetLogin; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetLogin
        {
            public readonly string RouterCode = "RouterCode";
        }
        static readonly ActionParamsClass_GetLogout s_params_GetLogout = new ActionParamsClass_GetLogout();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetLogout GetLogoutParams { get { return s_params_GetLogout; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetLogout
        {
            public readonly string RouterCode = "RouterCode";
        }
        static readonly ActionParamsClass_GetStatus s_params_GetStatus = new ActionParamsClass_GetStatus();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetStatus GetStatusParams { get { return s_params_GetStatus; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetStatus
        {
            public readonly string RouterCode = "RouterCode";
        }
        static readonly ActionParamsClass_GetError s_params_GetError = new ActionParamsClass_GetError();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetError GetErrorParams { get { return s_params_GetError; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetError
        {
            public readonly string RouterCode = "RouterCode";
        }
        static readonly ActionParamsClass_GetAlogin s_params_GetAlogin = new ActionParamsClass_GetAlogin();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetAlogin GetAloginParams { get { return s_params_GetAlogin; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetAlogin
        {
            public readonly string RouterCode = "RouterCode";
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
                public readonly string GetAlogin = "GetAlogin";
                public readonly string GetError = "GetError";
                public readonly string GetLogin = "GetLogin";
                public readonly string GetLogout = "GetLogout";
                public readonly string GetStatus = "GetStatus";
            }
            public readonly string GetAlogin = "~/Views/GetRouterTemplate/GetAlogin.cshtml";
            public readonly string GetError = "~/Views/GetRouterTemplate/GetError.cshtml";
            public readonly string GetLogin = "~/Views/GetRouterTemplate/GetLogin.cshtml";
            public readonly string GetLogout = "~/Views/GetRouterTemplate/GetLogout.cshtml";
            public readonly string GetStatus = "~/Views/GetRouterTemplate/GetStatus.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_GetRouterTemplateController : Netotik.Web.Controllers.GetRouterTemplateController
    {
        public T4MVC_GetRouterTemplateController() : base(Dummy.Instance) { }

        [NonAction]
        partial void GetLoginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string RouterCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetLogin(string RouterCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetLogin);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "RouterCode", RouterCode);
            GetLoginOverride(callInfo, RouterCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void GetLogoutOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string RouterCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetLogout(string RouterCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetLogout);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "RouterCode", RouterCode);
            GetLogoutOverride(callInfo, RouterCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void GetStatusOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string RouterCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetStatus(string RouterCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetStatus);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "RouterCode", RouterCode);
            GetStatusOverride(callInfo, RouterCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void GetErrorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string RouterCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetError(string RouterCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetError);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "RouterCode", RouterCode);
            GetErrorOverride(callInfo, RouterCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void GetAloginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string RouterCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> GetAlogin(string RouterCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetAlogin);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "RouterCode", RouterCode);
            GetAloginOverride(callInfo, RouterCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
