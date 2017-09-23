using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.Resources;
using Netotik.Domain.Entity;
using Netotik.Common.Security;
using Netotik.Common.Filters;
using Netotik.ViewModels;
using Netotik.Services.Enums;
using System.Threading.Tasks;
using Netotik.Web.Infrastructure.Filters;
using Netotik.Web.Infrastructure;
using Netotik.Common;
using CaptchaMvc.Attributes;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.UI;
using Netotik.ViewModels.Identity.UserReseller;
using Netotik.Services.Identity;
using Netotik.Services.Implement;
using Mvc.Mailer;
using Netotik.ViewModels.Identity.Account;
using Netotik.Common.Controller;
using System.IO;
//Test Comment
namespace Netotik.Web.Controllers
{
    public partial class WebsitesLogsController : BaseController
    {
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUserMailer _userMailer;
        private readonly IUnitOfWork _uow;
        private readonly IUserCompanyLogClientService _usercompanylogclientservice;

        public WebsitesLogsController(
            IMikrotikServices mikrotikservices,
            IApplicationUserManager applicationUserManager,
            IUserMailer userMailer,
            IUserCompanyLogClientService usercompanylogclientservice,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikservices;
            _userMailer = userMailer;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
            _usercompanylogclientservice = usercompanylogclientservice;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{lang}/WebsitesLogs/http/{CompanyCode}")]
        public virtual ActionResult Http(HttpPostedFileBase txtFile, string CompanyName)
        {
            var Company = _applicationUserManager.FindByCompanyCodeAsync(CompanyName);
            if (Company == null) return HttpNotFound();
            if (txtFile != null && txtFile.ContentLength > 0)
            {//---------------Codes
                var fileName = Path.GetFileName(txtFile.FileName);
                var path = Path.Combine(Server.MapPath("~/WebsitesLogs/Http"), fileName);
                txtFile.SaveAs(path);
                if (System.IO.File.Exists(path))
                {
                    var LogFileData = System.IO.File.ReadAllLines(path);
                    if(LogFileData!=null)
                    {
                        foreach(var Line in LogFileData)
                        {
                            try
                            {
                                if (Line != null && Line.Split(' ')[5].Contains("http://"))
                                {
                                    string Date = Line.Split(' ')[0];
                                    string Time = Line.Split(' ')[1];
                                    string ClientIP = Line.Split(' ')[3];
                                    string Url = Line.Split(' ')[5];
                                    string Companyid = Company.Id.ToString();
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{lang}/WebsitesLogs/https/{CompanyCode}")]
        public virtual ActionResult Https(HttpPostedFileBase txtFile, string CompanyName)
        {
            var Company = _applicationUserManager.FindByCompanyCodeAsync(CompanyName);
            if (Company == null) return HttpNotFound();
            if (txtFile != null && txtFile.ContentLength > 0)
            {//---------------Codes
                var fileName = Path.GetFileName(txtFile.FileName);
                var path = Path.Combine(Server.MapPath("~/WebsitesLogs/Https"), fileName);
                txtFile.SaveAs(path);
                if (System.IO.File.Exists(path))
                {
                    var LogFileData = System.IO.File.ReadAllLines(path);
                    if (LogFileData != null)
                    {
                        foreach (var Line in LogFileData)
                        {
                            try
                            {
                                if (Line != null && Line.Split(' ')[11].Contains("->"))
                                {
                                    string Date = Line.Split(' ')[0];
                                    string Time = Line.Split(' ')[1];
                                    string MacAddress = Line.Split(' ')[6];
                                    string ClientIP = Line.Split(' ')[10].Split(char.Parse("->"))[0];
                                    string HttpsServerIp = Line.Split(' ')[10].Split(char.Parse("->"))[1];
                                    string Companyid = Company.Id.ToString();

                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            return View();
        }


    }
}