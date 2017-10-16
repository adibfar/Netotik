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
    public partial class StatisticsController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected StatisticsController(Dummy d) { }

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
        public StatisticsController Actions { get { return MVC.Admin.Statistics; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Statistics";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Statistics";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string GetViewChartData = "GetViewChartData";
            public readonly string Countries = "Countries";
            public readonly string Chart = "Chart";
            public readonly string Map = "Map";
            public readonly string RequestMapData = "RequestMapData";
            public readonly string RequestUserOsData = "RequestUserOsData";
            public readonly string RequestUserBrowserData = "RequestUserBrowserData";
            public readonly string RequestVisitorsCountryData = "RequestVisitorsCountryData";
            public readonly string RequestVisitorsVectorMapData = "RequestVisitorsVectorMapData";
            public readonly string VectorMap = "VectorMap";
            public readonly string Chart2 = "Chart2";
            public readonly string BrowserTable = "BrowserTable";
            public readonly string OsTable = "OsTable";
            public readonly string Referrer = "Referrer";
            public readonly string PageView = "PageView";
            public readonly string CurrentVisitor = "CurrentVisitor";
            public readonly string Subdetails = "Subdetails";
            public readonly string RedirectToLocal = "RedirectToLocal";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string GetViewChartData = "GetViewChartData";
            public const string Countries = "Countries";
            public const string Chart = "Chart";
            public const string Map = "Map";
            public const string RequestMapData = "RequestMapData";
            public const string RequestUserOsData = "RequestUserOsData";
            public const string RequestUserBrowserData = "RequestUserBrowserData";
            public const string RequestVisitorsCountryData = "RequestVisitorsCountryData";
            public const string RequestVisitorsVectorMapData = "RequestVisitorsVectorMapData";
            public const string VectorMap = "VectorMap";
            public const string Chart2 = "Chart2";
            public const string BrowserTable = "BrowserTable";
            public const string OsTable = "OsTable";
            public const string Referrer = "Referrer";
            public const string PageView = "PageView";
            public const string CurrentVisitor = "CurrentVisitor";
            public const string Subdetails = "Subdetails";
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
                public readonly string _BrowserTablePartial = "_BrowserTablePartial";
                public readonly string _Countries = "_Countries";
                public readonly string _CurrentVisitorPartial = "_CurrentVisitorPartial";
                public readonly string _OsTablePartial = "_OsTablePartial";
                public readonly string _PageViewPartial = "_PageViewPartial";
                public readonly string _SubDetailsPartial = "_SubDetailsPartial";
                public readonly string _UserReferrerPartial = "_UserReferrerPartial";
                public readonly string Chart = "Chart";
                public readonly string Chart2 = "Chart2";
                public readonly string Index = "Index";
                public readonly string Map = "Map";
            }
            public readonly string _BrowserTablePartial = "~/Areas/Admin/Views/Statistics/_BrowserTablePartial.cshtml";
            public readonly string _Countries = "~/Areas/Admin/Views/Statistics/_Countries.cshtml";
            public readonly string _CurrentVisitorPartial = "~/Areas/Admin/Views/Statistics/_CurrentVisitorPartial.cshtml";
            public readonly string _OsTablePartial = "~/Areas/Admin/Views/Statistics/_OsTablePartial.cshtml";
            public readonly string _PageViewPartial = "~/Areas/Admin/Views/Statistics/_PageViewPartial.cshtml";
            public readonly string _SubDetailsPartial = "~/Areas/Admin/Views/Statistics/_SubDetailsPartial.cshtml";
            public readonly string _UserReferrerPartial = "~/Areas/Admin/Views/Statistics/_UserReferrerPartial.cshtml";
            public readonly string Chart = "~/Areas/Admin/Views/Statistics/Chart.cshtml";
            public readonly string Chart2 = "~/Areas/Admin/Views/Statistics/Chart2.cshtml";
            public readonly string Index = "~/Areas/Admin/Views/Statistics/Index.cshtml";
            public readonly string Map = "~/Areas/Admin/Views/Statistics/Map.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_StatisticsController : Netotik.Web.Areas.Admin.Controllers.StatisticsController
    {
        public T4MVC_StatisticsController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetViewChartDataOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetViewChartData()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetViewChartData);
            GetViewChartDataOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CountriesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Countries()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Countries);
            CountriesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ChartOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Chart()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Chart);
            ChartOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void MapOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Map()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Map);
            MapOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RequestMapDataOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult RequestMapData()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RequestMapData);
            RequestMapDataOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RequestUserOsDataOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult RequestUserOsData()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RequestUserOsData);
            RequestUserOsDataOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RequestUserBrowserDataOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult RequestUserBrowserData()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RequestUserBrowserData);
            RequestUserBrowserDataOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RequestVisitorsCountryDataOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult RequestVisitorsCountryData()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RequestVisitorsCountryData);
            RequestVisitorsCountryDataOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RequestVisitorsVectorMapDataOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult RequestVisitorsVectorMapData()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RequestVisitorsVectorMapData);
            RequestVisitorsVectorMapDataOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void VectorMapOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult VectorMap()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.VectorMap);
            VectorMapOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void Chart2Override(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Chart2()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Chart2);
            Chart2Override(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void BrowserTableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult BrowserTable()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BrowserTable);
            BrowserTableOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void OsTableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult OsTable()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.OsTable);
            OsTableOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ReferrerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Referrer()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Referrer);
            ReferrerOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PageViewOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult PageView()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PageView);
            PageViewOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CurrentVisitorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult CurrentVisitor()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CurrentVisitor);
            CurrentVisitorOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SubdetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Subdetails()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Subdetails);
            SubdetailsOverride(callInfo);
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
