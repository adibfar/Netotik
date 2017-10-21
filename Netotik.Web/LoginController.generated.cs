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
    public partial class LoginController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected LoginController(Dummy d) { }

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
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Router()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Router);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Reseller()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Reseller);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Client()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Client);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public LoginController Actions { get { return MVC.Login; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Login";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Login";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Router = "Router";
            public readonly string Reseller = "Reseller";
            public readonly string Client = "Client";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Router = "Router";
            public const string Reseller = "Reseller";
            public const string Client = "Client";
            public const string RedirectToLocal = "RedirectToLocal";
        }


        static readonly ActionParamsClass_Router s_params_Router = new ActionParamsClass_Router();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Router RouterParams { get { return s_params_Router; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Router
        {
            public readonly string ReturnUrl = "ReturnUrl";
            public readonly string ResellerCode = "ResellerCode";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Reseller s_params_Reseller = new ActionParamsClass_Reseller();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Reseller ResellerParams { get { return s_params_Reseller; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Reseller
        {
            public readonly string ReturnUrl = "ReturnUrl";
            public readonly string model = "model";
            public readonly string fromPage = "fromPage";
        }
        static readonly ActionParamsClass_Client s_params_Client = new ActionParamsClass_Client();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Client ClientParams { get { return s_params_Client; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Client
        {
            public readonly string ReturnUrl = "ReturnUrl";
            public readonly string RouterCode = "RouterCode";
            public readonly string model = "model";
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
                public readonly string Client = "Client";
                public readonly string Company = "Company";
                public readonly string Reseller = "Reseller";
            }
            public readonly string Client = "~/Views/Login/Client.cshtml";
            public readonly string Company = "~/Views/Login/Company.cshtml";
            public readonly string Reseller = "~/Views/Login/Reseller.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_LoginController : Netotik.Web.Controllers.LoginController
    {
        public T4MVC_LoginController() : base(Dummy.Instance) { }

        [NonAction]
        partial void RouterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string ReturnUrl, string ResellerCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Router(string ReturnUrl, string ResellerCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Router);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ReturnUrl", ReturnUrl);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ResellerCode", ResellerCode);
            RouterOverride(callInfo, ReturnUrl, ResellerCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void RouterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Identity.UserRouter.LoginModel model, string ReturnUrl, string ResellerCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Router(Netotik.ViewModels.Identity.UserRouter.LoginModel model, string ReturnUrl, string ResellerCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Router);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ReturnUrl", ReturnUrl);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ResellerCode", ResellerCode);
            RouterOverride(callInfo, model, ReturnUrl, ResellerCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void ResellerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string ReturnUrl);

        [NonAction]
        public override System.Web.Mvc.ActionResult Reseller(string ReturnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Reseller);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ReturnUrl", ReturnUrl);
            ResellerOverride(callInfo, ReturnUrl);
            return callInfo;
        }

        [NonAction]
        partial void ResellerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Identity.UserReseller.LoginModel model, string ReturnUrl, int fromPage);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Reseller(Netotik.ViewModels.Identity.UserReseller.LoginModel model, string ReturnUrl, int fromPage)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Reseller);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ReturnUrl", ReturnUrl);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "fromPage", fromPage);
            ResellerOverride(callInfo, model, ReturnUrl, fromPage);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void ClientOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string ReturnUrl, string RouterCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Client(string ReturnUrl, string RouterCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Client);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ReturnUrl", ReturnUrl);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "RouterCode", RouterCode);
            ClientOverride(callInfo, ReturnUrl, RouterCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void ClientOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Identity.UserClient.LoginModel model, string ReturnUrl, string RouterCode);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Client(Netotik.ViewModels.Identity.UserClient.LoginModel model, string ReturnUrl, string RouterCode)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Client);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ReturnUrl", ReturnUrl);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "RouterCode", RouterCode);
            ClientOverride(callInfo, model, ReturnUrl, RouterCode);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
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
