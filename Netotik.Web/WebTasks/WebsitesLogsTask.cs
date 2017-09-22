using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Data.Entity;
using Netotik.Common.Scheduler;
using Netotik.Domain.Entity;
using Netotik.Services.Identity;
using Netotik.Services.Abstract;
using Netotik.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Netotik.Web.WebTasks
{
    public class WebsitesLogsTask : ScheduledTaskTemplate
    {
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUserMailer _userMailer;
        private readonly IUnitOfWork _uow;
        private readonly IMenuService _menuService;

        public WebsitesLogsTask(
            IMikrotikServices mikrotikservices,
            IApplicationUserManager applicationUserManager,
            IUserMailer userMailer,
            IMenuService menuService,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikservices;
            _userMailer = userMailer;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
            _menuService = menuService;
        }
        /// <summary>
        /// اگر چند جاب در يك زمان مشخص داشتيد، اين خاصيت ترتيب اجراي آن‌ها را مشخص خواهد كرد
        /// </summary>
        public override int Order
        {
            get { return 1; }
        }

        public override bool RunAt(DateTime utcNow)
        {
            if (this.IsShuttingDown || this.Pause)
                return false;

            var now = utcNow.AddHours(3.5);

            // هر چند وقت یکبار اجرا بشه رو اینجا مشخص می کنی
            return now.Minute == 0 || now.Minute == 20 || now.Minute == 40;

        }

        public async override void Run()
        {
            if (this.IsShuttingDown || this.Pause)
                return;
            //--------------------------------------------------------------------
            var HttpFolder = Directory.EnumerateFiles(HostingEnvironment.MapPath("~/WebsitesLogs/Http"));
            foreach (var Httpfile in HttpFolder)
            {
                var CompanyName = Path.GetFileName(Httpfile);
                var UserCompany = _applicationUserManager.FindByCompanyCodeAsync(CompanyName);
                var user = _applicationUserManager.GetUserCompanyMikrotikConf(UserCompany.Id);
                if (UserCompany != null)//&& Usercompany.WebsiteLogs == true
                {
                    var fileContents = System.IO.File.ReadAllText(Httpfile);
                    if (fileContents != null)
                    {
                        foreach (var Line in fileContents.Split('\n'))
                        {
                            try
                            {
                                if (Line != null && Line.Split(' ')[5].Contains("http://"))
                                {
                                    string Date = Line.Split(' ')[0];
                                    string Time = Line.Split(' ')[1];
                                    string ClientIP = Line.Split(' ')[3];
                                    string Url = Line.Split(' ')[5];
                                    string Companyid = UserCompany.Id.ToString();

                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            var HttpsFolder = Directory.EnumerateFiles(HostingEnvironment.MapPath("~/WebsitesLogs/Https"));
            foreach (var Httpsfile in HttpsFolder)
            {
                var CompanyName = Path.GetFileName(Httpsfile);
                var UserCompany = _applicationUserManager.FindByCompanyCodeAsync(CompanyName);
                var user = _applicationUserManager.GetUserCompanyMikrotikConf(UserCompany.Id);
                if (UserCompany != null)//&& Usercompany.WebsiteLogs == true
                {
                    var fileContents = System.IO.File.ReadAllText(Httpsfile);
                    if (fileContents != null)
                    {
                        foreach (var Line in fileContents.Split('\n'))
                        {
                            try
                            {
                                if (Line != null && Line.Split(' ')[5].Contains("http://"))
                                {
                                    string Date = Line.Split(' ')[0];
                                    string Time = Line.Split(' ')[1];
                                    string ClientIP = Line.Split(' ')[3];
                                    string Url = Line.Split(' ')[5];
                                    string Companyid = UserCompany.Id.ToString();

                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            //-------------------------------------------------------------------
            string[] files = System.IO.Directory.GetFiles(HostingEnvironment.MapPath("~/WebsitesLogs/Http"));
            foreach (string file in files)
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            }
            string[] files2 = System.IO.Directory.GetFiles(HostingEnvironment.MapPath("~/WebsitesLogs/Https"));
            foreach (string file in files2)
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            }
            //--------------------------------------------------------------------





            var Users = _applicationUserManager.GetUserCompaniesWebsitesLogsActive();
            foreach (var user in Users)
            {
                if (user.UserCompany.WebsitesLogs)//user.WebsiteLogs == true
                {

                    bool IsFtpEnable = _mikrotikServices.IsIpServiceEnable(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password,"ftp");
                    if (_mikrotikServices.FileExist(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpLogFile.0.txt.cp"))
                    {
                        _mikrotikServices.RemoveFile(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpLogFile.0.txt.cp");
                    }
                    if (_mikrotikServices.FileExist(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpsLogFile.0.txt.cp"))
                    {
                        _mikrotikServices.RemoveFile(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpsLogFile.0.txt.cp");
                    }
                    //-------------------------
                    if (_mikrotikServices.FileExist(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpLogFile.0.txt"))
                    {
                        if(IsFtpEnable)
                        {
                            _mikrotikServices.CopyFileToFtp(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "127.0.0.1", _mikrotikServices.GetIpServicePortNumber(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "ftp").ToString(), user.UserCompany.R_User, user.UserCompany.R_Password, "HttpLogFile.0.txt", "HttpLogFile.0.txt.cp");
                            Thread.Sleep(15000);
                            _mikrotikServices.RemoveFile(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpLogFile.0.txt");
                            _mikrotikServices.CopyFileToFtp(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "netotik.com", "21", "Http", "pass", "HttpLogFile.0.txt.cp", user.UserCompany.CompanyCode);
                        }
                        else
                        {
                            _mikrotikServices.EnableIpService(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "ftp");
                            _mikrotikServices.CopyFileToFtp(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "127.0.0.1", _mikrotikServices.GetIpServicePortNumber(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "ftp").ToString(), user.UserCompany.R_User, user.UserCompany.R_Password, "HttpLogFile.0.txt", "HttpLogFile.0.txt.cp");
                            Thread.Sleep(15000);
                            _mikrotikServices.RemoveFile(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpLogFile.0.txt");
                            _mikrotikServices.CopyFileToFtp(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "netotik.com", "21", "Http", "pass", "HttpLogFile.0.txt.cp", user.UserCompany.CompanyCode);
                            _mikrotikServices.DisableIpService(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "ftp");
                        }
                    }
                    if (_mikrotikServices.FileExist(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpsLogFile.0.txt"))
                    {
                        if (IsFtpEnable)
                        {
                            _mikrotikServices.CopyFileToFtp(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "127.0.0.1", _mikrotikServices.GetIpServicePortNumber(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "ftp").ToString(), user.UserCompany.R_User, user.UserCompany.R_Password, "HttpsLogFile.0.txt", "HttpsLogFile.0.txt.cp");
                            Thread.Sleep(15000);
                            _mikrotikServices.RemoveFile(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpsLogFile.0.txt");
                            _mikrotikServices.CopyFileToFtp(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "netotik.com", "21", "Http", "pass", "HttpsLogFile.0.txt.cp", user.UserCompany.CompanyCode);
                        }
                        else
                        {
                            _mikrotikServices.EnableIpService(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "ftp");
                            _mikrotikServices.CopyFileToFtp(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "127.0.0.1", _mikrotikServices.GetIpServicePortNumber(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "ftp").ToString(), user.UserCompany.R_User, user.UserCompany.R_Password, "HttpsLogFile.0.txt", "HttpsLogFile.0.txt.cp");
                            Thread.Sleep(15000);
                            _mikrotikServices.RemoveFile(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "HttpsLogFile.0.txt");
                            _mikrotikServices.CopyFileToFtp(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "netotik.com", "21", "Http", "pass", "HttpsLogFile.0.txt.cp", user.UserCompany.CompanyCode);
                            _mikrotikServices.DisableIpService(user.UserCompany.R_Host, user.UserCompany.R_Port, user.UserCompany.R_User, user.UserCompany.R_Password, "ftp");
                        }
                    }
                }
            }
            //کدهایی که باید اجرا شه اینجا بنویس
        }

        public override string Name
        {
            get { return "WebsitesLogs"; }
        }
    }

}