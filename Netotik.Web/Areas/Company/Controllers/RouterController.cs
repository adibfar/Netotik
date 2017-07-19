using System;
using System.Linq;
using System.Web.Mvc;
using Netotik.Common.Filters;
using Netotik.Web.Infrastructure;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.Common;
using Netotik.Resources;
using Netotik.Domain.Entity;
using Netotik.Common.Security;
using Netotik.Web.Infrastructure.Filters;
using System.Web.UI;
using System.Threading.Tasks;
using Netotik.Web;
using System.Data.Entity.Validation;
using System.IO;
using DNTBreadCrumb;
using Netotik.Common.MikrotikAPI;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Netotik.ViewModels.Mikrotik;
using System.Collections.Generic;

namespace Netotik.Web.Areas.Company.Controllers
{
    [Mvc5Authorize(Roles = "Company")]
    [BreadCrumb(Title = "Router", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class RouterController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _uow;

        public RouterController(
            IMikrotikServices mikrotikServices,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _pictureService = pictureservice;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
        }
        #endregion


        #region Router

        public virtual ActionResult Info()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port);
            if (!mikrotik.Login(UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password)) mikrotik.Close();
            //-----------------------------------------------
            var Router_Info = new Router_InfoModel();
            //-----------------------------------------------
            var Router_Resource = _mikrotikServices.Router_Resource(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            Router_Info.Architecture_name = Router_Resource.FirstOrDefault().Architecture_name;
            Router_Info.Board_name = Router_Resource.FirstOrDefault().Board_name;
            Router_Info.Build_time = Router_Resource.FirstOrDefault().Build_time;
            Router_Info.Cpu = Router_Resource.FirstOrDefault().Cpu;
            Router_Info.Cpu_count = Router_Resource.FirstOrDefault().Cpu_count;
            Router_Info.Cpu_frequency = Router_Resource.FirstOrDefault().Cpu_frequency;
            Router_Info.Cpu_load = Router_Resource.FirstOrDefault().Cpu_load;
            Router_Info.Free_hdd_space = (ulong.Parse(Router_Resource.FirstOrDefault().Free_hdd_space) / 1048576).ToString();
            Router_Info.Free_memory = (ulong.Parse(Router_Resource.FirstOrDefault().Free_memory) / 1048576).ToString();
            Router_Info.Platform = Router_Resource.FirstOrDefault().Platform;
            Router_Info.Total_hdd_space = (ulong.Parse(Router_Resource.FirstOrDefault().Total_hdd_space) / 1048576).ToString();
            Router_Info.Total_memory = (ulong.Parse(Router_Resource.FirstOrDefault().Total_memory) / 1048576).ToString();
            Router_Info.Uptime = Router_Resource.FirstOrDefault().Uptime.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend);
            Router_Info.Version = Router_Resource.FirstOrDefault().Version;
            //--------------------------------------
            var Router_Identity = _mikrotikServices.Router_Identity(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            Router_Info.RouterName = Router_Identity.FirstOrDefault().RouterName;
            //--------------------------------------
            var Router_License = _mikrotikServices.Router_License(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            Router_Info.Software_id = Router_License.FirstOrDefault().Software_id;
            Router_Info.nlevel = Router_License.FirstOrDefault().nlevel;
            //------------------------------------------
            var Router_PackageUpdate = _mikrotikServices.Router_PackageUpdate(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            Router_Info.Channel = Router_PackageUpdate.FirstOrDefault().Channel;
            Router_Info.Installed_version = Router_PackageUpdate.FirstOrDefault().Installed_version;
            Router_Info.Latest_version = Router_PackageUpdate.FirstOrDefault().Latest_version;
            Router_Info.Update_status = Router_PackageUpdate.FirstOrDefault().Update_status;
            //------------------------------------------
            var Router_Clock = _mikrotikServices.Router_Clock(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            ViewBag.Router_date = Infrastructure.EnglishConvertDate.ConvertToFa(Router_Clock.FirstOrDefault().Router_date, "D");
            Router_Info.Router_date = Infrastructure.EnglishConvertDate.ConvertToFa(Router_Clock.FirstOrDefault().Router_date, "d");
            ViewBag.Server_Date = PersianDate.ConvertDate.ToFa(DateTime.Now, "d");
            ViewBag.Server_DateT = PersianDate.ConvertDate.ToFa(DateTime.Now, "D");
            //PersianDate.ConvertDate.ToFa();
            Router_Info.Router_time = Router_Clock.FirstOrDefault().Router_time;
            //------------------------------------------
            var Router_Routerboard = _mikrotikServices.Router_Routerboard(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            Router_Info.Serial_number = Router_Routerboard.FirstOrDefault().Serial_number;
            Router_Info.Router_model = Router_Routerboard.FirstOrDefault().Router_model;
            //------------------------------------------
            ViewBag.UsedMemory = ulong.Parse(Router_Info.Total_memory) - ulong.Parse(Router_Info.Free_memory);
            ViewBag.UsedMemoryPercent = ((ulong.Parse(Router_Info.Total_memory) - ulong.Parse(Router_Info.Free_memory)) * 100) / ulong.Parse(Router_Info.Total_memory);
            ViewBag.UsedHDD = ulong.Parse(Router_Info.Total_hdd_space) - ulong.Parse(Router_Info.Free_hdd_space);
            ViewBag.UsedHDDPercent = ((ulong.Parse(Router_Info.Total_hdd_space) - ulong.Parse(Router_Info.Free_hdd_space)) * 100) / ulong.Parse(Router_Info.Total_hdd_space);
            //------------------------------------------
            return View(Router_Info);
        }
        public virtual ActionResult UpdateRouter(string ReturnURL)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_Info_Update(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageInformation(Captions.Attention, Captions.MikrotikUpdateStartMessage);
            if (Url.IsLocalUrl(ReturnURL))
            {
                return Redirect(ReturnURL);
            }
            return RedirectToAction(MVC.Company.Router.ActionNames.Info);
        }
        public virtual ActionResult UpdateRouterCheck(string ReturnURL)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port);
            if (!mikrotik.Login(UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password)) mikrotik.Close();
            mikrotik.Send("/system/package/update/check-for-updates", true);
            if (Url.IsLocalUrl(ReturnURL))
            {
                return Redirect(ReturnURL);
            }
            return RedirectToAction(MVC.Company.Router.ActionNames.Info);
        }

        public virtual ActionResult PPP()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }

        public virtual ActionResult Interfaces()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }

        public virtual ActionResult Wireless()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }

        public virtual ActionResult WirelessDetails(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            var Wireless = _mikrotikServices.GetWirelessDetails(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return View(Wireless);
        }

        public virtual ActionResult InterfaceDisable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_InterfaceDisable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Interfaces);
        }

        public virtual ActionResult WirelessEnable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_InterfaceEnable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Wireless);
        }

        public virtual ActionResult WirelessDisable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_InterfaceDisable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Wireless);
        }

        public virtual ActionResult InterfaceEnable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_InterfaceEnable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Interfaces);
        }

        public virtual ActionResult InterfaceDetails(string id)
        {
            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            var Ethernet = _mikrotikServices.GetEthernetDetails(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return View(Ethernet);
        }
        #endregion

        public virtual ActionResult RouterSetting()
        {
            ViewBag.ReturnURL = "/Fa/Company/Router/RouterSetting";
            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            var filelist = _mikrotikServices.GetBackupRouterList(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            if (filelist == null)
            {
                filelist = new List<Router_FileModel>();
                filelist.Add(new Router_FileModel()
                {
                    Name = Captions.NothingFound,
                    CreateTime = "",
                    Size = "",
                    Type = ""
                });
            }
            ViewBag.RestoreRouter = filelist;
            //-------------------------------
            var fileUlist = _mikrotikServices.GetBackupUsermanagerList(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            if (fileUlist == null)
            {
                fileUlist = new List<Router_FileModel>();
                fileUlist.Add(new Router_FileModel()
                {
                    Name = Captions.NothingFound,
                    CreateTime = "",
                    Size = "",
                    Type = ""
                });
            }
            ViewBag.RestoreUsermanager = fileUlist;
            //-------------------------------
            var Router_PackageUpdate = _mikrotikServices.Router_PackageUpdate(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            ViewBag.Channel = Router_PackageUpdate.FirstOrDefault().Channel;
            ViewBag.Installed_version = Router_PackageUpdate.FirstOrDefault().Installed_version;
            ViewBag.Latest_version = Router_PackageUpdate.FirstOrDefault().Latest_version;
            ViewBag.Update_status = Router_PackageUpdate.FirstOrDefault().Update_status;

            return View();
        }
        public virtual ActionResult Reboot()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.RebootRouter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageSuccess(Captions.Reboot, Captions.MikrotikRebootMessage);
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult ResetRouter()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }

            //-------------------------------
            if (Request.Form["reset"] != null)
                if (Request.Form["reset"].ToString() == "yes")
                {
                    bool nosetting = false;
                    if (Request.Form["nodefualt"] != null) { nosetting = true; }
                    _mikrotikServices.ResetRouter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, true, nosetting);
                    this.MessageSuccess(Captions.MikrotikReset, Captions.MikrotikResetMessage);
                }
                else
                {
                    this.MessageInformation(Captions.Error, Captions.MikrotikResetUsermanagerMessage);
                }
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult RestoreUsermanager()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }

            //-------------------------------
            if (true)
            {
                _mikrotikServices.RestoreUsermanager(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, Request.Form["RestoreUsermanager"].ToString());
                this.MessageSuccess(Captions.MikrotikUsermanagerRestore, Captions.MikrotikRestoreUsermanagerMessage);
            }
            else
            {
                this.MessageInformation(Captions.Error, Captions.ValidateError);
            }
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult RestoreRouter()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }

            //-------------------------------
            _mikrotikServices.RestoreRouter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, Request.Form["RestoreRouter"].ToString());
            this.MessageSuccess(Captions.MikrotikRestore, Captions.MikrotikRestoreMessage);
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult ResetUsermanager()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (Request.Form["reset"] != null)
                if (Request.Form["reset"].ToString() == "yes")
                {
                    bool logs = true; bool users = true; bool packages = true; bool history = true; bool session = true; bool db = true;
                    if (Request.Form["logs"] == null)
                        logs = false;
                    if (Request.Form["users"] == null)
                        users = false;
                    if (Request.Form["packages"] == null)
                        packages = false;
                    if (Request.Form["history"] == null)
                        history = false;
                    if (Request.Form["logs"] == null)
                        session = false;
                    if (Request.Form["logs"] == null)
                        db = false;
                    //-------------------------------
                    if (logs == false && false == users && false == packages && false == history && false == session && db == false)
                    {
                        this.MessageInformation(Captions.Error, Captions.ValidateError);
                    }
                    else
                    {
                        _mikrotikServices.ResetUsermanager(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, users, logs, session, history, packages, db);
                        this.MessageSuccess(Captions.MikrotikResetUsermanager, Captions.MikrotikResetUsermanagerMessage);
                    }
                }
                else
                {
                    this.MessageInformation(Captions.Error,Captions.ValidateError);
                }
            else
            {
                this.MessageInformation(Captions.Error, Captions.ValidateError);
            }
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult RemoveLogs()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }

            //-------------------------------
            _mikrotikServices.RemoveLogs(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageSuccess(Captions.RemoveLogs, Captions.MikrotikRemoveLogsMessage);
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult BackupUsermanager()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }

            //-------------------------------
            _mikrotikServices.BackupUsermanager(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageSuccess(Captions.MikrotikUsermanagerBackup, Captions.MikrotikUsermanagerBackupMessage);
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult BackupRouter()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }

            //-------------------------------
            _mikrotikServices.BackupRouter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageSuccess(Captions.MikrotikBackup, Captions.MikrotikBackupMessage);
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult Nat(Router_NatModel model)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------

            _mikrotikServices.Router_NatAdd(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, model);
            return RedirectToAction(MVC.Company.Router.ActionNames.Nat);
        }
        public virtual ActionResult Nat()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            ViewBag.Interfaces = _mikrotikServices.Interface(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            ViewBag.NatList = _mikrotikServices.Router_NatList(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }
        [HttpPost]
        public virtual ActionResult NatRemove(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_NatRemove(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Nat);
        }
        [HttpPost]
        public virtual ActionResult NatEnable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_NatEnable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Nat);
        }
        [HttpPost]
        public virtual ActionResult NatDisable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_NatDisable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Nat);
        }
    }
}