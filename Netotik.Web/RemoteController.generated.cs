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
    public partial class RemoteController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RemoteController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult CheckPassword()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.CheckPassword);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult IsEmailAvailable()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsEmailAvailable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult CheckNationalCode()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.CheckNationalCode);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult IsPhoneNumberAvailable()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsPhoneNumberAvailable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult IsResellerCompanyNameExist()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsResellerCompanyNameExist);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult IsUserNameAvailable()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsUserNameAvailable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToLocal()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToLocal);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RemoteController Actions { get { return MVC.Remote; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Remote";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Remote";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string CheckPassword = "CheckPassword";
            public readonly string IsEmailAvailable = "IsEmailAvailable";
            public readonly string CheckNationalCode = "CheckNationalCode";
            public readonly string IsPhoneNumberAvailable = "IsPhoneNumberAvailable";
            public readonly string IsResellerCompanyNameExist = "IsResellerCompanyNameExist";
            public readonly string IsUserNameAvailable = "IsUserNameAvailable";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string CheckPassword = "CheckPassword";
            public const string IsEmailAvailable = "IsEmailAvailable";
            public const string CheckNationalCode = "CheckNationalCode";
            public const string IsPhoneNumberAvailable = "IsPhoneNumberAvailable";
            public const string IsResellerCompanyNameExist = "IsResellerCompanyNameExist";
            public const string IsUserNameAvailable = "IsUserNameAvailable";
            public const string RedirectToLocal = "RedirectToLocal";
        }


        static readonly ActionParamsClass_CheckPassword s_params_CheckPassword = new ActionParamsClass_CheckPassword();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CheckPassword CheckPasswordParams { get { return s_params_CheckPassword; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CheckPassword
        {
            public readonly string password = "password";
        }
        static readonly ActionParamsClass_IsEmailAvailable s_params_IsEmailAvailable = new ActionParamsClass_IsEmailAvailable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IsEmailAvailable IsEmailAvailableParams { get { return s_params_IsEmailAvailable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IsEmailAvailable
        {
            public readonly string email = "email";
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_CheckNationalCode s_params_CheckNationalCode = new ActionParamsClass_CheckNationalCode();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CheckNationalCode CheckNationalCodeParams { get { return s_params_CheckNationalCode; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CheckNationalCode
        {
            public readonly string nationalCode = "nationalCode";
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_IsPhoneNumberAvailable s_params_IsPhoneNumberAvailable = new ActionParamsClass_IsPhoneNumberAvailable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IsPhoneNumberAvailable IsPhoneNumberAvailableParams { get { return s_params_IsPhoneNumberAvailable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IsPhoneNumberAvailable
        {
            public readonly string phoneNumber = "phoneNumber";
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_IsResellerCompanyNameExist s_params_IsResellerCompanyNameExist = new ActionParamsClass_IsResellerCompanyNameExist();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IsResellerCompanyNameExist IsResellerCompanyNameExistParams { get { return s_params_IsResellerCompanyNameExist; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IsResellerCompanyNameExist
        {
            public readonly string companyName = "companyName";
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_IsUserNameAvailable s_params_IsUserNameAvailable = new ActionParamsClass_IsUserNameAvailable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_IsUserNameAvailable IsUserNameAvailableParams { get { return s_params_IsUserNameAvailable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_IsUserNameAvailable
        {
            public readonly string userName = "userName";
            public readonly string Id = "Id";
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
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RemoteController : Netotik.Web.Controllers.RemoteController
    {
        public T4MVC_RemoteController() : base(Dummy.Instance) { }

        [NonAction]
        partial void CheckPasswordOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string password);

        [NonAction]
        public override System.Web.Mvc.JsonResult CheckPassword(string password)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.CheckPassword);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "password", password);
            CheckPasswordOverride(callInfo, password);
            return callInfo;
        }

        [NonAction]
        partial void IsEmailAvailableOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string email, long? Id);

        [NonAction]
        public override System.Web.Mvc.JsonResult IsEmailAvailable(string email, long? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsEmailAvailable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "email", email);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            IsEmailAvailableOverride(callInfo, email, Id);
            return callInfo;
        }

        [NonAction]
        partial void CheckNationalCodeOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string nationalCode, long? Id);

        [NonAction]
        public override System.Web.Mvc.JsonResult CheckNationalCode(string nationalCode, long? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.CheckNationalCode);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "nationalCode", nationalCode);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            CheckNationalCodeOverride(callInfo, nationalCode, Id);
            return callInfo;
        }

        [NonAction]
        partial void IsPhoneNumberAvailableOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string phoneNumber, long? Id);

        [NonAction]
        public override System.Web.Mvc.JsonResult IsPhoneNumberAvailable(string phoneNumber, long? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsPhoneNumberAvailable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "phoneNumber", phoneNumber);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            IsPhoneNumberAvailableOverride(callInfo, phoneNumber, Id);
            return callInfo;
        }

        [NonAction]
        partial void IsResellerCompanyNameExistOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string companyName, long? Id);

        [NonAction]
        public override System.Web.Mvc.JsonResult IsResellerCompanyNameExist(string companyName, long? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsResellerCompanyNameExist);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyName", companyName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            IsResellerCompanyNameExistOverride(callInfo, companyName, Id);
            return callInfo;
        }

        [NonAction]
        partial void IsUserNameAvailableOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string userName, long? Id);

        [NonAction]
        public override System.Web.Mvc.JsonResult IsUserNameAvailable(string userName, long? Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.IsUserNameAvailable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userName", userName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            IsUserNameAvailableOverride(callInfo, userName, Id);
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
