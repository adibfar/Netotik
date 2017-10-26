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

namespace Netotik.Web.Areas.MyRouter.Controllers
{
     [Mvc5Authorize(Roles = "Router")]
    [BreadCrumb(Title = "Router", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class RouterController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _uow;
        private readonly IUserRouterLogClientService _UserRouterlogclientservice;

        public RouterController(
            IMikrotikServices mikrotikServices,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            IUserRouterLogClientService UserRouterlogclientservice,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _pictureService = pictureservice;
            _applicationUserManager = applicationUserManager;
            _UserRouterlogclientservice = UserRouterlogclientservice;
            _uow = uow;
        }
        #endregion


        #region Router

        public virtual ActionResult Info()
        {
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }

            return View();
        }
        public virtual ActionResult UpdateRouter(string ReturnURL)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_Info_Update(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            this.MessageInformation(Captions.Attention, Captions.MikrotikUpdateStartMessage);
            if (Url.IsLocalUrl(ReturnURL))
            {
                return Redirect(ReturnURL);
            }
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Info);
        }
        public virtual ActionResult UpdateRouterCheck(string ReturnURL)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port);
            if (!mikrotik.Login(UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password)) mikrotik.Close();
            mikrotik.Send("/system/package/update/check-for-updates", true);
            if (Url.IsLocalUrl(ReturnURL))
            {
                return Redirect(ReturnURL);
            }
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Info);
        }

        public virtual ActionResult PPP()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return View();
        }

        public virtual ActionResult Interfaces()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return View();
        }

        public virtual ActionResult Wireless()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return View();
        }

        public virtual ActionResult WirelessDetails(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            var Wireless = _mikrotikServices.GetWirelessDetails(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return View(Wireless);
        }

        public virtual ActionResult InterfaceDisable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_InterfaceDisable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Interfaces);
        }

        public virtual ActionResult WirelessEnable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_InterfaceEnable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Wireless);
        }

        public virtual ActionResult WirelessDisable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_InterfaceDisable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Wireless);
        }

        public virtual ActionResult InterfaceEnable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_InterfaceEnable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Interfaces);
        }

        public virtual ActionResult InterfaceDetails(string id)
        {
            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            var Ethernet = _mikrotikServices.GetEthernetDetails(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return View(Ethernet);
        }
        #endregion

        public virtual ActionResult RouterSetting()
        {
            ViewBag.ReturnURL = "/Fa/Router/Router/RouterSetting";
            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            var filelist = _mikrotikServices.GetBackupRouterList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
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
            var fileUlist = _mikrotikServices.GetBackupUsermanagerList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
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
            var Router_PackageUpdate = _mikrotikServices.Router_PackageUpdate(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            ViewBag.Channel = Router_PackageUpdate.FirstOrDefault().Channel;
            ViewBag.Installed_version = Router_PackageUpdate.FirstOrDefault().Installed_version;
            ViewBag.Latest_version = Router_PackageUpdate.FirstOrDefault().Latest_version;
            ViewBag.Update_status = Router_PackageUpdate.FirstOrDefault().Update_status;

            return View();
        }
        public virtual ActionResult Reboot()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.RebootRouter(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            this.MessageSuccess(Captions.Reboot, Captions.MikrotikRebootMessage);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult ResetRouter()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }

            //-------------------------------
            if (Request.Form["reset"] != null)
                if (Request.Form["reset"].ToString() == "yes")
                {
                    bool nosetting = false;
                    if (Request.Form["nodefualt"] != null) { nosetting = true; }
                    _mikrotikServices.ResetRouter(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, true, nosetting);
                    this.MessageSuccess(Captions.MikrotikReset, Captions.MikrotikResetMessage);
                }
                else
                {
                    this.MessageInformation(Captions.Error, Captions.MikrotikResetUsermanagerMessage);
                }
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult RestoreUsermanager()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }

            //-------------------------------
            if (true)
            {
                _mikrotikServices.RestoreUsermanager(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, Request.Form["RestoreUsermanager"].ToString());
                this.MessageSuccess(Captions.MikrotikUsermanagerRestore, Captions.MikrotikRestoreUsermanagerMessage);
            }
            // else
            // {
            //     this.MessageInformation(Captions.Error, Captions.ValidateError);
            // }
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult RestoreRouter()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }

            //-------------------------------
            _mikrotikServices.RestoreRouter(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, Request.Form["RestoreRouter"].ToString());
            this.MessageSuccess(Captions.MikrotikRestore, Captions.MikrotikRestoreMessage);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult ResetUsermanager()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
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
                        _mikrotikServices.ResetUsermanager(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, users, logs, session, history, packages, db);
                        this.MessageSuccess(Captions.MikrotikResetUsermanager, Captions.MikrotikResetUsermanagerMessage);
                    }
                }
                else
                {
                    this.MessageInformation(Captions.Error, Captions.ValidateError);
                }
            else
            {
                this.MessageInformation(Captions.Error, Captions.ValidateError);
            }
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult RemoveLogs()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }

            //-------------------------------
            _mikrotikServices.RemoveLogs(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            this.MessageSuccess(Captions.RemoveLogs, Captions.MikrotikRemoveLogsMessage);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult BackupUsermanager()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }

            //-------------------------------
            _mikrotikServices.BackupUsermanager(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            this.MessageSuccess(Captions.MikrotikUsermanagerBackup, Captions.MikrotikUsermanagerBackupMessage);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult BackupRouter()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }

            //-------------------------------
            _mikrotikServices.BackupRouter(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            this.MessageSuccess(Captions.MikrotikBackup, Captions.MikrotikBackupMessage);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult Nat(Router_NatModel model)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------

            _mikrotikServices.Router_NatAdd(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, model);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Nat);
        }
        public virtual ActionResult Nat()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            ViewBag.Interfaces = _mikrotikServices.Interface(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            ViewBag.NatList = _mikrotikServices.Router_NatList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return View();
        }
        [HttpPost]
        public virtual ActionResult NatRemove(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_NatRemove(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Nat);
        }
        [HttpPost]
        public virtual ActionResult NatEnable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_NatEnable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Nat);
        }
        [HttpPost]
        public virtual ActionResult NatDisable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            _mikrotikServices.Router_NatDisable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Nat);
        }
        public virtual ActionResult WebSitesLogs()
        {

            //-------------------------------

            if (!UserLogined.UserRouter.WebsitesLogs)
            {
                this.MessageError(Captions.Error, "شما مجوز لازم را ندارید");
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            ViewBag.Model = _UserRouterlogclientservice.GetList(UserLogined.Id);
            return View();
        }

        public virtual JsonResult GetRouterResource()
        {
            var Router_Resource = _mikrotikServices.Router_Resource(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return Json(new
            {
                Cpu = Router_Resource.FirstOrDefault().Cpu,
                Cpu_count = Router_Resource.FirstOrDefault().Cpu_count,
                Cpu_frequency = Router_Resource.FirstOrDefault().Cpu_frequency + "MHz",
                Cpu_load = Router_Resource.FirstOrDefault().Cpu_load,
                Free_hdd_space = (ulong.Parse(Router_Resource.FirstOrDefault().Free_hdd_space) / 1048576).ToString() + " MB",
                Free_memory = (ulong.Parse(Router_Resource.FirstOrDefault().Free_memory) / 1048576).ToString() + " MB",
                Total_hdd_space = (ulong.Parse(Router_Resource.FirstOrDefault().Total_hdd_space) / 1048576).ToString() + " MB",
                Total_memory = (ulong.Parse(Router_Resource.FirstOrDefault().Total_memory) / 1048576).ToString() + " MB",
                Uptime = Router_Resource.FirstOrDefault().Uptime.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend),
                Version = Router_Resource.FirstOrDefault().Version,
                UsedMemory = (ulong.Parse(Router_Resource.FirstOrDefault().Total_memory) - ulong.Parse(Router_Resource.FirstOrDefault().Free_memory)) / 1048576 + " MB",
                UsedMemoryPercent = ((ulong.Parse(Router_Resource.FirstOrDefault().Total_memory) - ulong.Parse(Router_Resource.FirstOrDefault().Free_memory)) * 100) / ulong.Parse(Router_Resource.FirstOrDefault().Total_memory),
                UsedHDD = (ulong.Parse(Router_Resource.FirstOrDefault().Total_hdd_space) - ulong.Parse(Router_Resource.FirstOrDefault().Free_hdd_space)) / 1048576 + " MB",
                UsedHDDPercent = ((ulong.Parse(Router_Resource.FirstOrDefault().Total_hdd_space) - ulong.Parse(Router_Resource.FirstOrDefault().Free_hdd_space)) * 100) / ulong.Parse(Router_Resource.FirstOrDefault().Total_hdd_space),

            }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult GetRouterIdentity()
        {
            var Router_Identity = _mikrotikServices.Router_Identity(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return Json(new
            {
                RouterName = Router_Identity.FirstOrDefault().RouterName

            }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult GetRouterLicense()
        {
            var Router_License = _mikrotikServices.Router_License(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return Json(new
            {
                Software_id = Router_License.FirstOrDefault().Software_id,
                nlevel = Router_License.FirstOrDefault().nlevel

            }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult GetRouterPackageUpdate()
        {
            var Router_PackageUpdate = _mikrotikServices.Router_PackageUpdate(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return Json(new
            {
                Latest_version = Router_PackageUpdate.FirstOrDefault().Latest_version,
                Update_status = Router_PackageUpdate.FirstOrDefault().Update_status

            }, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetRouterClock()
        {
            var Router_Clock = _mikrotikServices.Router_Clock(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);

            return Json(new
            {
                Router_date = Infrastructure.EnglishConvertDate.ConvertToFa(Router_Clock.FirstOrDefault().Router_date, "d"),
                Server_Date = PersianDate.ConvertDate.ToFa(DateTime.Now, "d"),
                Router_time = Router_Clock.FirstOrDefault().Router_time,

            }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult GetRouterBoard()
        {
            var Router_Routerboard = _mikrotikServices.Router_Routerboard(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return Json(new
            {
                Router_model = Router_Routerboard.FirstOrDefault().Router_model =="" ? Captions.NotDetect : Router_Routerboard.FirstOrDefault().Router_model,

            }, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Access()
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            

            return View();
        }
        [HttpPost]
        public virtual ActionResult AccessDisable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Access);
        }
        [HttpPost]
        public virtual ActionResult AccessEnable(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Access);
        }
        [HttpPost]
        public virtual ActionResult AccessRemove(string id)
        {

            //-------------------------------

            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            }
            //-------------------------------
            return RedirectToAction(MVC.MyRouter.Router.ActionNames.Access);
        }
    }
}