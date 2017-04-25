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
    public partial class UserManagerController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected UserManagerController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult ResetCounter()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResetCounter);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult CloseSession()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CloseSession);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Userdisable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Userdisable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Userremove()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Userremove);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ProfileRemove()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ProfileRemove);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Userenable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Userenable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PackageDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UserDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UserEdit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserEdit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult UserEdit_Save()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserEdit_Save);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public UserManagerController Actions { get { return MVC.Company.UserManager; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Company";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "UserManager";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "UserManager";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ResetCounter = "ResetCounter";
            public readonly string CloseSession = "CloseSession";
            public readonly string PackageCreate = "PackageCreate";
            public readonly string UserList = "UserList";
            public readonly string Userdisable = "Userdisable";
            public readonly string Userremove = "Userremove";
            public readonly string ProfileRemove = "ProfileRemove";
            public readonly string Userenable = "Userenable";
            public readonly string PackageDetails = "PackageDetails";
            public readonly string UserDetails = "UserDetails";
            public readonly string UserCreate = "UserCreate";
            public readonly string PackageList = "PackageList";
            public readonly string Report = "Report";
            public readonly string Hotspot_Temp = "Hotspot_Temp";
            public readonly string Register = "Register";
            public readonly string UserEdit = "UserEdit";
            public readonly string UserEdit_Save = "UserEdit_Save";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ResetCounter = "ResetCounter";
            public const string CloseSession = "CloseSession";
            public const string PackageCreate = "PackageCreate";
            public const string UserList = "UserList";
            public const string Userdisable = "Userdisable";
            public const string Userremove = "Userremove";
            public const string ProfileRemove = "ProfileRemove";
            public const string Userenable = "Userenable";
            public const string PackageDetails = "PackageDetails";
            public const string UserDetails = "UserDetails";
            public const string UserCreate = "UserCreate";
            public const string PackageList = "PackageList";
            public const string Report = "Report";
            public const string Hotspot_Temp = "Hotspot_Temp";
            public const string Register = "Register";
            public const string UserEdit = "UserEdit";
            public const string UserEdit_Save = "UserEdit_Save";
            public const string RedirectToLocal = "RedirectToLocal";
        }


        static readonly ActionParamsClass_ResetCounter s_params_ResetCounter = new ActionParamsClass_ResetCounter();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ResetCounter ResetCounterParams { get { return s_params_ResetCounter; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ResetCounter
        {
            public readonly string user = "user";
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_CloseSession s_params_CloseSession = new ActionParamsClass_CloseSession();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CloseSession CloseSessionParams { get { return s_params_CloseSession; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CloseSession
        {
            public readonly string user = "user";
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_PackageCreate s_params_PackageCreate = new ActionParamsClass_PackageCreate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PackageCreate PackageCreateParams { get { return s_params_PackageCreate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PackageCreate
        {
            public readonly string model = "model";
            public readonly string actionType = "actionType";
        }
        static readonly ActionParamsClass_Userdisable s_params_Userdisable = new ActionParamsClass_Userdisable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Userdisable UserdisableParams { get { return s_params_Userdisable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Userdisable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Userremove s_params_Userremove = new ActionParamsClass_Userremove();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Userremove UserremoveParams { get { return s_params_Userremove; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Userremove
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_ProfileRemove s_params_ProfileRemove = new ActionParamsClass_ProfileRemove();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ProfileRemove ProfileRemoveParams { get { return s_params_ProfileRemove; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ProfileRemove
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Userenable s_params_Userenable = new ActionParamsClass_Userenable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Userenable UserenableParams { get { return s_params_Userenable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Userenable
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_PackageDetails s_params_PackageDetails = new ActionParamsClass_PackageDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PackageDetails PackageDetailsParams { get { return s_params_PackageDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PackageDetails
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_UserDetails s_params_UserDetails = new ActionParamsClass_UserDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UserDetails UserDetailsParams { get { return s_params_UserDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UserDetails
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_UserCreate s_params_UserCreate = new ActionParamsClass_UserCreate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UserCreate UserCreateParams { get { return s_params_UserCreate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UserCreate
        {
            public readonly string model = "model";
            public readonly string actionType = "actionType";
        }
        static readonly ActionParamsClass_UserEdit s_params_UserEdit = new ActionParamsClass_UserEdit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UserEdit UserEditParams { get { return s_params_UserEdit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UserEdit
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_UserEdit_Save s_params_UserEdit_Save = new ActionParamsClass_UserEdit_Save();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UserEdit_Save UserEdit_SaveParams { get { return s_params_UserEdit_Save; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UserEdit_Save
        {
            public readonly string model = "model";
            public readonly string actionType = "actionType";
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
                public readonly string PackageCreate = "PackageCreate";
                public readonly string PackageDetails = "PackageDetails";
                public readonly string PackageList = "PackageList";
                public readonly string Register = "Register";
                public readonly string Report = "Report";
                public readonly string UserCreate = "UserCreate";
                public readonly string UserDetails = "UserDetails";
                public readonly string UserEdit = "UserEdit";
                public readonly string UserList = "UserList";
            }
            public readonly string _Table = "~/Areas/Company/Views/UserManager/_Table.cshtml";
            public readonly string PackageCreate = "~/Areas/Company/Views/UserManager/PackageCreate.cshtml";
            public readonly string PackageDetails = "~/Areas/Company/Views/UserManager/PackageDetails.cshtml";
            public readonly string PackageList = "~/Areas/Company/Views/UserManager/PackageList.cshtml";
            public readonly string Register = "~/Areas/Company/Views/UserManager/Register.cshtml";
            public readonly string Report = "~/Areas/Company/Views/UserManager/Report.cshtml";
            public readonly string UserCreate = "~/Areas/Company/Views/UserManager/UserCreate.cshtml";
            public readonly string UserDetails = "~/Areas/Company/Views/UserManager/UserDetails.cshtml";
            public readonly string UserEdit = "~/Areas/Company/Views/UserManager/UserEdit.cshtml";
            public readonly string UserList = "~/Areas/Company/Views/UserManager/UserList.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_UserManagerController : Netotik.Web.Areas.Company.Controllers.UserManagerController
    {
        public T4MVC_UserManagerController() : base(Dummy.Instance) { }

        [NonAction]
        partial void ResetCounterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string user, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult ResetCounter(string user, string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResetCounter);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "user", user);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ResetCounterOverride(callInfo, user, id);
            return callInfo;
        }

        [NonAction]
        partial void CloseSessionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string user, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult CloseSession(string user, string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CloseSession);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "user", user);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CloseSessionOverride(callInfo, user, id);
            return callInfo;
        }

        [NonAction]
        partial void PackageCreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PackageCreate()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageCreate);
            PackageCreateOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PackageCreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Mikrotik.Usermanager_ProfileLimitionCreateModel model, Netotik.Common.Controller.ActionType actionType);

        [NonAction]
        public override System.Web.Mvc.ActionResult PackageCreate(Netotik.ViewModels.Mikrotik.Usermanager_ProfileLimitionCreateModel model, Netotik.Common.Controller.ActionType actionType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageCreate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "actionType", actionType);
            PackageCreateOverride(callInfo, model, actionType);
            return callInfo;
        }

        [NonAction]
        partial void UserListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult UserList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserList);
            UserListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UserdisableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Userdisable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Userdisable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            UserdisableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void UserremoveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Userremove(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Userremove);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            UserremoveOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void ProfileRemoveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult ProfileRemove(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ProfileRemove);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ProfileRemoveOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void UserenableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Userenable(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Userenable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            UserenableOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void PackageDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult PackageDetails(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PackageDetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void UserDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult UserDetails(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            UserDetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void UserCreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult UserCreate()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserCreate);
            UserCreateOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UserCreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Mikrotik.Usermanager_UserRegisterModel model, Netotik.Common.Controller.ActionType actionType);

        [NonAction]
        public override System.Web.Mvc.ActionResult UserCreate(Netotik.ViewModels.Mikrotik.Usermanager_UserRegisterModel model, Netotik.Common.Controller.ActionType actionType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserCreate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "actionType", actionType);
            UserCreateOverride(callInfo, model, actionType);
            return callInfo;
        }

        [NonAction]
        partial void PackageListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PackageList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageList);
            PackageListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ReportOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Report()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Report);
            ReportOverride(callInfo);
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
        partial void RegisterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Register()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Register);
            RegisterOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UserEditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.ActionResult UserEdit(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserEdit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            UserEditOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void UserEdit_SaveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Netotik.ViewModels.Mikrotik.Usermanager_UserEditModel model, Netotik.Common.Controller.ActionType actionType);

        [NonAction]
        public override System.Web.Mvc.ActionResult UserEdit_Save(Netotik.ViewModels.Mikrotik.Usermanager_UserEditModel model, Netotik.Common.Controller.ActionType actionType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserEdit_Save);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "actionType", actionType);
            UserEdit_SaveOverride(callInfo, model, actionType);
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
