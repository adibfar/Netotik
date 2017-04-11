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
    public partial class AddressStateController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AddressStateController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Detail()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detail);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> IsNameExist()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsNameExist);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AddressStateController Actions { get { return MVC.Admin.AddressState; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "AddressState";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "AddressState";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Create = "Create";
            public readonly string Detail = "Detail";
            public readonly string Remove = "Remove";
            public readonly string Edit = "Edit";
            public readonly string IsNameExist = "IsNameExist";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Create = "Create";
            public const string Detail = "Detail";
            public const string Remove = "Remove";
            public const string Edit = "Edit";
            public const string IsNameExist = "IsNameExist";
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
            public readonly string actionType = "actionType";
        }
        static readonly ActionParamsClass_Detail s_params_Detail = new ActionParamsClass_Detail();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Detail DetailParams { get { return s_params_Detail; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Detail
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Remove s_params_Remove = new ActionParamsClass_Remove();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Remove RemoveParams { get { return s_params_Remove; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Remove
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string model = "model";
            public readonly string actionType = "actionType";
        }
        static readonly ActionParamsClass_IsNameExist s_params_IsNameExist = new ActionParamsClass_IsNameExist();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IsNameExist IsNameExistParams { get { return s_params_IsNameExist; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IsNameExist
        {
            public readonly string name = "name";
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
                public readonly string Create = "Create";
                public readonly string CreateOrEdit = "CreateOrEdit";
                public readonly string Detail = "Detail";
                public readonly string Edit = "Edit";
                public readonly string Index = "Index";
            }
            public readonly string _Table = "~/Areas/Admin/Views/AddressState/_Table.cshtml";
            public readonly string Create = "~/Areas/Admin/Views/AddressState/Create.cshtml";
            public readonly string CreateOrEdit = "~/Areas/Admin/Views/AddressState/CreateOrEdit.cshtml";
            public readonly string Detail = "~/Areas/Admin/Views/AddressState/Detail.cshtml";
            public readonly string Edit = "~/Areas/Admin/Views/AddressState/Edit.cshtml";
            public readonly string Index = "~/Areas/Admin/Views/AddressState/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_AddressStateController : Netotik.Web.Areas.Admin.Controllers.AddressStateController
    {
        public T4MVC_AddressStateController() : base(Dummy.Instance) { }

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
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Common.AddressState.AddressStateModel model, Netotik.Common.Controller.ActionType actionType);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Create(Netotik.ViewModels.Common.AddressState.AddressStateModel model, Netotik.Common.Controller.ActionType actionType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "actionType", actionType);
            CreateOverride(callInfo, model, actionType);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void DetailOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Detail(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detail);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DetailOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void RemoveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Remove(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Remove);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            RemoveOverride(callInfo, id);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Common.AddressState.AddressStateModel model, Netotik.Common.Controller.ActionType actionType);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Edit(Netotik.ViewModels.Common.AddressState.AddressStateModel model, Netotik.Common.Controller.ActionType actionType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "actionType", actionType);
            EditOverride(callInfo, model, actionType);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void IsNameExistOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string name, int? id);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> IsNameExist(string name, int? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsNameExist);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            IsNameExistOverride(callInfo, name, id);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
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
