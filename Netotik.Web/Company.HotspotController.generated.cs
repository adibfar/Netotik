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
    public partial class HotspotController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected HotspotController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult IpBindigsRemove()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpBindigsRemove);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult IpWalledGardenRemove()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpWalledGardenRemove);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult IpBindigsEnable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpBindigsEnable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult IpBindigsDisable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpBindigsDisable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult IpWalledGardenEnable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpWalledGardenEnable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult IpWalledGardenDisable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpWalledGardenDisable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HotspotController Actions { get { return MVC.Company.Hotspot; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Company";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Hotspot";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Hotspot";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string AddIpWalledGarden = "AddIpWalledGarden";
            public readonly string AddIpBindings = "AddIpBindings";
            public readonly string Servers = "Servers";
            public readonly string Access = "Access";
            public readonly string IpBindigsRemove = "IpBindigsRemove";
            public readonly string IpWalledGardenRemove = "IpWalledGardenRemove";
            public readonly string IpBindigsEnable = "IpBindigsEnable";
            public readonly string IpBindigsDisable = "IpBindigsDisable";
            public readonly string IpWalledGardenEnable = "IpWalledGardenEnable";
            public readonly string IpWalledGardenDisable = "IpWalledGardenDisable";
            public readonly string Template = "Template";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string AddIpWalledGarden = "AddIpWalledGarden";
            public const string AddIpBindings = "AddIpBindings";
            public const string Servers = "Servers";
            public const string Access = "Access";
            public const string IpBindigsRemove = "IpBindigsRemove";
            public const string IpWalledGardenRemove = "IpWalledGardenRemove";
            public const string IpBindigsEnable = "IpBindigsEnable";
            public const string IpBindigsDisable = "IpBindigsDisable";
            public const string IpWalledGardenEnable = "IpWalledGardenEnable";
            public const string IpWalledGardenDisable = "IpWalledGardenDisable";
            public const string Template = "Template";
            public const string RedirectToLocal = "RedirectToLocal";
        }


        static readonly ActionParamsClass_IpBindigsRemove s_params_IpBindigsRemove = new ActionParamsClass_IpBindigsRemove();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IpBindigsRemove IpBindigsRemoveParams { get { return s_params_IpBindigsRemove; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IpBindigsRemove
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_IpWalledGardenRemove s_params_IpWalledGardenRemove = new ActionParamsClass_IpWalledGardenRemove();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IpWalledGardenRemove IpWalledGardenRemoveParams { get { return s_params_IpWalledGardenRemove; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IpWalledGardenRemove
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_IpBindigsEnable s_params_IpBindigsEnable = new ActionParamsClass_IpBindigsEnable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IpBindigsEnable IpBindigsEnableParams { get { return s_params_IpBindigsEnable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IpBindigsEnable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_IpBindigsDisable s_params_IpBindigsDisable = new ActionParamsClass_IpBindigsDisable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IpBindigsDisable IpBindigsDisableParams { get { return s_params_IpBindigsDisable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IpBindigsDisable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_IpWalledGardenEnable s_params_IpWalledGardenEnable = new ActionParamsClass_IpWalledGardenEnable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IpWalledGardenEnable IpWalledGardenEnableParams { get { return s_params_IpWalledGardenEnable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IpWalledGardenEnable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_IpWalledGardenDisable s_params_IpWalledGardenDisable = new ActionParamsClass_IpWalledGardenDisable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IpWalledGardenDisable IpWalledGardenDisableParams { get { return s_params_IpWalledGardenDisable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IpWalledGardenDisable
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
                public readonly string Access = "Access";
                public readonly string Servers = "Servers";
                public readonly string Template = "Template";
            }
            public readonly string Access = "~/Areas/Company/Views/Hotspot/Access.cshtml";
            public readonly string Servers = "~/Areas/Company/Views/Hotspot/Servers.cshtml";
            public readonly string Template = "~/Areas/Company/Views/Hotspot/Template.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_HotspotController : Netotik.Web.Areas.Company.Controllers.HotspotController
    {
        public T4MVC_HotspotController() : base(Dummy.Instance) { }

        [NonAction]
        partial void AddIpWalledGardenOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddIpWalledGarden()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddIpWalledGarden);
            AddIpWalledGardenOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AddIpBindingsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddIpBindings()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddIpBindings);
            AddIpBindingsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ServersOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Servers()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Servers);
            ServersOverride(callInfo);
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
        partial void IpBindigsRemoveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult IpBindigsRemove(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpBindigsRemove);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            IpBindigsRemoveOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void IpWalledGardenRemoveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult IpWalledGardenRemove(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpWalledGardenRemove);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            IpWalledGardenRemoveOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void IpBindigsEnableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult IpBindigsEnable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpBindigsEnable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            IpBindigsEnableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void IpBindigsDisableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult IpBindigsDisable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpBindigsDisable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            IpBindigsDisableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void IpWalledGardenEnableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult IpWalledGardenEnable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpWalledGardenEnable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            IpWalledGardenEnableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void IpWalledGardenDisableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult IpWalledGardenDisable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IpWalledGardenDisable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            IpWalledGardenDisableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void TemplateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Template()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Template);
            TemplateOverride(callInfo);
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
