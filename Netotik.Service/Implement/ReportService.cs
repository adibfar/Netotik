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
using System.Web.Mvc;
using System.Collections;
using Netotik.ViewModels.Mikrotik;
using System.IO;
using System.Drawing;

namespace Netotik.Services.Implement
{
    public class ReportService : IReportService
    {
        public ReportService()
        {
        }
        public Image imagePathToImageCode(string path)
        {
            var imageIn = new Bitmap(path);
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            MemoryStream memorystream = new MemoryStream(ms.ToArray());
            return Image.FromStream(memorystream);
        }

        public ActionResult Print(List<UserModel> userlist, long id)
        {
            var test = new List<InternetCardModel>();
            var report = new StiReport();

            var logo = imagePathToImageCode(HostingEnvironment.MapPath("~/content/Reports/logo/"+"userid"+".jpg"));
            var background = imagePathToImageCode(HostingEnvironment.MapPath("~/content/Reports/logo/" + "userid" + ".jpg"));

            report.Load(HostingEnvironment.MapPath("~/Content/Reports/InternetCard.mrt"));
            foreach (var user in userlist)
            {
                test.Add(new InternetCardModel() {
                    Password = "گذرواژه= "+user.password,
                    Username = "نام کاربری = " + user.username,
                    Package = "بسته= " + user.actual_profile,
                    Title = "هتل بزرگ شیراز",
                    Discription = "لطفا به شبکه وایرلس ShirazHotel متصل شوید",
                    Logo = logo,
                    Background = background
                });
            }

            report.CalculationMode = StiCalculationMode.Interpretation;
            report.RegBusinessObject("InternetCard",test);
            StiReportResponse.PrintAsHtml(report);
            return StiMvcViewer.ExportReportResult(report);
        }
    }
}

