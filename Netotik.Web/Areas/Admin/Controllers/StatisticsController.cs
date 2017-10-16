using Netotik.Data;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Identity.UserAdmin;
using Netotik.Services.Abstract;
using Netotik.Services.Identity;
using Netotik.Web.Infrastructure;
using Netotik.Web.Infrastructure.Filters;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Netotik.Common.Filters;
using System.Web;
using System.IO;
using Netotik.Common.Controller;
using Netotik.Domain.Entity;
using Netotik.Resources;
using System.Collections.Generic;
using Netotik.ViewModels.Statistics;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.Web.WebPages;
using UAParser;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(AssignableToRolePermissions.CanViewAdminPanel)]
    public partial class StatisticsController : BasePanelController
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IStatisticCountryService _statisticCountryService;
        private readonly IUnitOfWork _uow;

        public StatisticsController(
            IStatisticsService statisticsService,
            IStatisticCountryService statisticCountryService,
            IUnitOfWork uow)
        {
            _statisticCountryService = statisticCountryService;
            _statisticsService = statisticsService;
            _uow = uow;
        }
        public virtual ActionResult Index()
        {

            IList<Statistic> stat = _statisticsService.All().ToList();

            StatisticsViewModel svm = new StatisticsViewModel()
            {
                TodayVisits = stat.Count(ss => ss.DateStamp.Day == DateTime.Now.Day),
                TodayVisitors = stat.Where(ss=>ss.DateStamp.Day == DateTime.Now.Day)
                .GroupBy(x=>x.IpAddress).Select(x=>x.FirstOrDefault()).Count(),
                UniquVisitors = stat.GroupBy(ta => ta.IpAddress).Select(ta => ta.Key).Count(),
            };


            return View(svm);
        }

        public virtual JsonResult GetViewChartData()
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-19);
            var logs = _statisticsService.All().Where(x => x.DateStamp > date).ToList();
            var viewLog = new long[20];
            var visitorLog = new long[20];
            var dates = new string[20];

            for (var i = 0; i < 20; i++)
            {
                dates[i] = PersianDate.ConvertDate.ToFa(date, "d");
                visitorLog[i] = logs.Where(x => x.DateStamp.Date == date.Date)
                    .GroupBy(x => x.IpAddress).Select(x => x.FirstOrDefault()).Count();
                viewLog[i] = logs.Where(x => x.DateStamp.Date == date.Date).Count();
                date = date.AddDays(1);

            }

            return Json(new
            {
                dates = dates,
                views = viewLog,
                visitors = visitorLog,
            }, JsonRequestBehavior.AllowGet);
        }



        public int calculatePercentage(int CurrentValue, int totallValue)
        {

            return (int)CurrentValue * 100 / totallValue;

        }

        public virtual ActionResult Countries()
        {
            return PartialView(MVC.Admin.Statistics.Views._Countries,
                _statisticCountryService.All()
                .Take(15).ToList());
        }

        public virtual ActionResult Chart()
        {
            IList<CountryViewModel> cvm = new List<CountryViewModel>();

            var countries = _statisticCountryService.All().ToList();
            int totalvisits = countries.Sum(country => country.ViewCount);
            foreach (var country in countries)
            {

                cvm.Add(new CountryViewModel()
                {
                    ViewCount = country.ViewCount,
                    CountryName = country.CountryName,
                    TotalVisits = totalvisits,
                    Percentage = country.ViewCount * 100 / totalvisits

                });
            }

            return View(cvm);
        }
        public virtual ActionResult Map()
        {
            return View();
        }

        [HttpGet]
        public virtual JsonResult RequestMapData()
        {
            var countries = _statisticCountryService.All().ToList();
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult RequestUserOsData()
        {
            var results = _statisticsService
            .All()
            .GroupBy(ua => new { ua.UserOs }).Select(g => new { lable = g.Key.UserOs, data = g.Count() })
            .ToArray();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult RequestUserBrowserData()
        {
            var results = _statisticsService.All()
                .GroupBy(ua => new { ua.UserAgent })
                .Select(g => new { lable = g.Key.UserAgent, value = g.Count() })
                .ToArray();
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult RequestVisitorsCountryData()
        {
            var results = _statisticCountryService.All().Select(c => new { y = c.CountryName, a = c.ViewCount }).ToArray();
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult RequestVisitorsVectorMapData()
        {
            var countrydata = new List<VectorMapViewModel>();

            var results = _statisticCountryService.All();
            countrydata.AddRange(results.Select(country => new VectorMapViewModel
            {
                CountryCode = country.CountryCode,
                CountryVisit = country.ViewCount
            }));
            var jd = JsonConvert.SerializeObject(countrydata);
            return Json(jd, JsonRequestBehavior.AllowGet);

        }

        public virtual ActionResult VectorMap()
        {
            return View("VectorMap");
        }


        public virtual ActionResult Chart2()
        {
            return View();
        }

        //بارگزاری اطلاعات برای جدول درصد استفاده از مرورگرها
        public virtual ActionResult BrowserTable()
        {
            var btv = new List<BrowserTableViewModel>();

            var tottal = _statisticsService.All()
                //.GroupBy(x => x.IpAddress).Select(x => x.FirstOrDefault())
                .Count();
            btv.AddRange(_statisticsService.All()
                //.GroupBy(x => x.IpAddress).Select(x => x.FirstOrDefault())
                .GroupBy(ua => new { ua.UserAgent })
                .OrderByDescending(g => g.Count()).Select(g => new BrowserTableViewModel() { BrowserIcon = g.Key.UserAgent, BrowserName = g.Key.UserAgent, BrowserViewCount = g.Count(), TottalVisits = tottal }).ToList());

            return PartialView("_BrowserTablePartial", btv);
        }


        //بارگزاری اطلاعات برای جدول درصد استفاده از سیستم عامل ها
        public virtual ActionResult OsTable()
        {
            var otv = new List<OsTableViewModel>();
            var tottal = _statisticsService.All()
                //.GroupBy(x => x.IpAddress).Select(x => x.FirstOrDefault())
                .Count();
            otv.AddRange(_statisticsService.All()
                //.GroupBy(x => x.IpAddress).Select(x => x.FirstOrDefault())
                .GroupBy(ua => new { ua.UserOs }).OrderByDescending(g => g.Count()).Select(g => new OsTableViewModel() { OsIcon = g.Key.UserOs, OsName = g.Key.UserOs, OsViewCount = g.Count(), TottalVisits = tottal }).ToList());
            return PartialView("_OsTablePartial", otv);
        }

        public virtual ActionResult Referrer()
        {
            var ur = new List<ReferrerViewModel>();
            var st = new List<Statistic>();

            st = _statisticsService.All().ToList();

            foreach (var statisticse in st)
                statisticse.Referer = GetHostName(statisticse.Referer);

            ur.AddRange(st.GroupBy(
                r => new { r.Referer }).OrderByDescending(
                r => r.Count()).Select(r => new ReferrerViewModel()
                { ReferrerUrl = r.Key.Referer, ReferrerCount = r.Count() }).ToList());
            //r.Key.Referer.Substring(0, 100)


            return PartialView("_UserReferrerPartial", ur);
        }

        public string GetHostName(string url)
        {
            if (url != "Direct")
            {
                Uri uri = new Uri(url);
                return uri.Host;
            }
            else
            {
                return url;
            }

        }

        public virtual ActionResult PageView()
        {
            var pv = new List<PageViewViewModel>();
            var st = new List<Statistic>();

            st = _statisticsService.All().ToList();

            foreach (var statisticse in st)
                statisticse.PageViewed = NormalizePageName(statisticse.PageViewed);


            pv.AddRange(st.GroupBy(
                   r => new { r.PageViewed }).OrderByDescending(
                   r => r.Count()).Select(r => new PageViewViewModel()
                   { PageUrl = r.Key.PageViewed, PageViewCount = r.Count() }).ToList());



            return PartialView("_PageViewPartial", pv);
        }


        /// <summary>
        /// متدی برای نرمال سازی نام صفحات
        /// </summary>
        /// <param name="PageName">نام صفحه بازدید شده</param>
        /// <returns>نرمال شده نام صفحه</returns>
        public string NormalizePageName(string PageName)
        {
            if (PageName == "/")
            {
                return "Home/Index";
            }
            else
            {
                return PageName.Remove(0, 1);
            }


        }


        public virtual ActionResult CurrentVisitor()
        {
            var Cv = new CurrentVisitorViewModel()
            {

                IpAddress = GetIPAddress(),
                Browser = Request.Browser.Browser,
                OsName = GetUserOS(Request.UserAgent)
            };



            return PartialView("_CurrentVisitorPartial", Cv);
        }

        public virtual ActionResult Subdetails()
        {
            IList<Statistic> stat = new List<Statistic>();
            stat = _statisticsService.All().ToList();

            var subdetails = new SubDetailsViewModel
            {

                Today = stat.Count(d => d.DateStamp.Day == DateTime.Now.Day),
                LastDay = stat.Count(d => d.DateStamp.Day == DateTime.Now.Day - 1),
                ThisMonth = stat.Count(m => m.DateStamp.Month == DateTime.Now.Month),
                ThisYear = stat.Count(y => y.DateStamp.Year == DateTime.Now.Year),
                PeakDate = stat.GroupBy(x => x.DateStamp.ToShortDateString()).OrderByDescending(grouping => grouping.Count()).First().Key.AsDateTime(),
                LowDate = stat.GroupBy(x => x.DateStamp.ToShortDateString()).OrderByDescending(grouping => grouping.Count()).Last().Key.AsDateTime(),



            };

            //MostVisitedDate();

            return PartialView("_SubDetailsPartial", subdetails);
        }




        //Helpers
        public static string GetUserOS(string userAgent)
        {
            // get a parser with the embedded regex patterns
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            return c.OS.Family;
        }
        public string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }



    }
}