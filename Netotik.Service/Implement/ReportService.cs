using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PersianDate;
using Netotik.Common;
using Netotik.Common.DataTables;
using Netotik.Service.FarapayamakService;
using Netotik.Services.Identity;
using Netotik.ViewModels.Identity.UserClient;
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Mvc;
using System.Web.Hosting;

namespace Netotik.Services.Implement
{
    public class ReportService : IReportService
    {
        public void Print(List<UserModel> userlist, long id)
        {
            var InternetCard = new
            {
                Username = "Username = ali",
                Password = "Password = AlI",
                Title = "هتل بزرگ شیراز",
                Package = "بسته میهمان یک روزه"
            };
            var report = new StiReport();
            report.Load(HostingEnvironment.MapPath("~/Content/Reports/InternetCard.mrt"));
            report.RegBusinessObject("InternetCard", InternetCard);
            StiReportResponse.PrintAsHtml(report);
            StiMvcViewer.ExportReportResult(report);
        }
    }
}

