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
namespace Netotik.Web.Areas.Admin.Controllers
{
    public partial class TicketController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected TicketController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult IssueTrack()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IssueTrack);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public TicketController Actions { get { return MVC.Admin.Ticket; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Ticket";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Ticket";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Create = "Create";
            public readonly string Show = "Show";
            public readonly string IssueTrack = "IssueTrack";
            public readonly string CloseIssue = "CloseIssue";
            public readonly string OpenIssue = "OpenIssue";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Create = "Create";
            public const string Show = "Show";
            public const string IssueTrack = "IssueTrack";
            public const string CloseIssue = "CloseIssue";
            public const string OpenIssue = "OpenIssue";
            public const string RedirectToLocal = "RedirectToLocal";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string Search = "Search";
            public readonly string Page = "Page";
            public readonly string PageSize = "PageSize";
        }
        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Show s_params_Show = new ActionParamsClass_Show();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Show ShowParams { get { return s_params_Show; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Show
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_IssueTrack s_params_IssueTrack = new ActionParamsClass_IssueTrack();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IssueTrack IssueTrackParams { get { return s_params_IssueTrack; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IssueTrack
        {
            public readonly string model = "model";
            public readonly string actionType = "actionType";
        }
        static readonly ActionParamsClass_CloseIssue s_params_CloseIssue = new ActionParamsClass_CloseIssue();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CloseIssue CloseIssueParams { get { return s_params_CloseIssue; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CloseIssue
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_OpenIssue s_params_OpenIssue = new ActionParamsClass_OpenIssue();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_OpenIssue OpenIssueParams { get { return s_params_OpenIssue; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_OpenIssue
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
                public readonly string _IssueResponse = "_IssueResponse";
                public readonly string _Table = "_Table";
                public readonly string Create = "Create";
                public readonly string CreateOrEdit = "CreateOrEdit";
                public readonly string Index = "Index";
                public readonly string Show = "Show";
            }
            public readonly string _IssueResponse = "~/Areas/Admin/Views/Ticket/_IssueResponse.cshtml";
            public readonly string _Table = "~/Areas/Admin/Views/Ticket/_Table.cshtml";
            public readonly string Create = "~/Areas/Admin/Views/Ticket/Create.cshtml";
            public readonly string CreateOrEdit = "~/Areas/Admin/Views/Ticket/CreateOrEdit.cshtml";
            public readonly string Index = "~/Areas/Admin/Views/Ticket/Index.cshtml";
            public readonly string Show = "~/Areas/Admin/Views/Ticket/Show.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_TicketController : Netotik.Web.Areas.Admin.Controllers.TicketController
    {
        public T4MVC_TicketController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string Search, int Page, int PageSize);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(string Search, int Page, int PageSize)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Search", Search);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Page", Page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "PageSize", PageSize);
            IndexOverride(callInfo, Search, Page, PageSize);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Create()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            CreateOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Ticket.Issue.IssueModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Create(Netotik.ViewModels.Ticket.Issue.IssueModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CreateOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void ShowOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Show(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Show);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ShowOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void IssueTrackOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Ticket.Issue.IssueTrackModel model, Netotik.Common.Controller.ActionType actionType);

        [NonAction]
        public override System.Web.Mvc.ActionResult IssueTrack(Netotik.ViewModels.Ticket.Issue.IssueTrackModel model, Netotik.Common.Controller.ActionType actionType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.IssueTrack);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "actionType", actionType);
            IssueTrackOverride(callInfo, model, actionType);
            return callInfo;
        }

        [NonAction]
        partial void CloseIssueOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> CloseIssue(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CloseIssue);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CloseIssueOverride(callInfo, id);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void OpenIssueOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> OpenIssue(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.OpenIssue);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            OpenIssueOverride(callInfo, id);
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
