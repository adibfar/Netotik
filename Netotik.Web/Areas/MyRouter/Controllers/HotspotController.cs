﻿using System;
using System.Linq;
using System.Web.Mvc;
using Netotik.Web.Infrastructure;
using Netotik.Services.Abstract;
using Netotik.Resources;
using DNTBreadCrumb;
using Netotik.ViewModels.Mikrotik;
using Netotik.Services.Identity;
using Netotik.Data;
using Netotik.Common.Filters;
using Netotik.Common.Controller;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Web.Hosting;

namespace Netotik.Web.Areas.MyRouter.Controllers
{
    [Mvc5Authorize(Roles = "Router")]
    [BreadCrumb(Title = "Hotspot", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
Order = 0, GlyphIcon = "icon icon-table")]
    public partial class HotspotController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _uow;

        public HotspotController(
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

        public virtual ActionResult CreateIpWalledGarden()
        {
            ViewBag.servers = _mikrotikServices.Hotspot_ServersList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return PartialView(MVC.MyRouter.Hotspot.Views._CreateWebsiteAccess);
        }
        [HttpPost]
        public virtual ActionResult AddIpWalledGarden()
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
            if (Request.Form["DstHost"] == null) Request.Form["DstHost"] = "";
            if (Request.Form["DstPort"] == null) Request.Form["DstPort"] = "";
            if (Request.Form["Portocol"] == null)
            {
                Request.Form["Portocol"] = "";
            }
            //if (Request.Form["Comment"] == null) Request.Form["Comment"] = "";
            if (Request.Form["Server"] == null) Request.Form["Server"] = "";
            if (Request.Form["Action"] == null) Request.Form["Action"] = "";
            Hotspot_IPWalledGardenModel temp = new Hotspot_IPWalledGardenModel()
            {
                dst_host = Request.Form["DstHost"].ToString(),
                dst_port = Request.Form["DstPort"].ToString(),
                protocol = Request.Form["Portocol"].ToString(),
                //comment = Request.Form["Comment"].ToString(),
                disabled = "no",
                server = Request.Form["Server"].ToString(),
                action = Request.Form["Action"].ToString(),
            };

            _mikrotikServices.Hotspot_IpWalledGardenAdd(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, temp);
            this.MessageSuccess(Captions.UpdateSuccess, Captions.HotspotAddressesAccessAdd);
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Access);
        }

        public virtual ActionResult CreateIpBindings()
        {
            ViewBag.servers = _mikrotikServices.Hotspot_ServersList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return PartialView(MVC.MyRouter.Hotspot.Views._CreateUserAccess);
        }
        [HttpPost]
        public virtual ActionResult AddIpBindings()
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
            if (Request.Form["IpAddress"] == null) Request.Form["IpAddress"] = "";
            if (Request.Form["MacAddress"] == null) Request.Form["MacAddress"] = "";
            if (Request.Form["NatIpAddress"] == null) Request.Form["NatIpAddress"] = "";
            // if (Request.Form["Comment"] == null) Request.Form["Comment"] = "";
            if (Request.Form["Server"] == null) Request.Form["Server"] = "";
            if (Request.Form["Type"] == null) Request.Form["Type"] = "";
            Hotspot_IPBindingsModel temp = new Hotspot_IPBindingsModel()
            {
                address = Request.Form["IpAddress"].ToString(),
                mac_address = Request.Form["MacAddress"].ToString(),
                to_address = Request.Form["NatIpAddress"].ToString(),
                // comment = Request.Form["Comment"].ToString(),
                disabled = "no",
                server = Request.Form["Server"].ToString(),
                type = Request.Form["Type"].ToString(),
            };

            _mikrotikServices.Hotspot_IpBindingsAdd(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, temp);
            this.MessageSuccess(Captions.UpdateSuccess, Captions.HotspotUserAccessAdd);
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Access);
        }

        public virtual ActionResult Servers()
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
            ViewBag.servers = _mikrotikServices.Hotspot_ServersList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);

            return View();
        }

        public virtual ActionResult LoadOnlines()
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    var ActiveList = _mikrotikServices.Hotspot_ActiveList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                    var Active = new List<Hotspot_ActiveModel>();
                    foreach (var item in ActiveList)
                    {
                        item.Flags = item.Flags.Replace("Active", "A").Replace("Host", "");
                        if (item.Flags != "")
                        {
                            item.radius = item.radius.Replace("true", "R").Replace("flase", "");
                        }
                        item.uptime = item.uptime.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                        item.session_time_left = item.session_time_left.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                        item.keepalive_timeout = item.keepalive_timeout.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                        if (item.limit_bytes_in != null)
                            if (item.limit_bytes_in != "")
                                item.limit_bytes_in = (ulong.Parse(item.limit_bytes_in) / 1048576).ToString();
                        if (item.limit_bytes_out != null)
                            if (item.limit_bytes_out != "")
                                item.limit_bytes_out = (ulong.Parse(item.limit_bytes_out) / 1048576).ToString();
                        if (item.limit_bytes_total != null)
                            if (item.limit_bytes_total != "")
                                item.limit_bytes_total = (ulong.Parse(item.limit_bytes_total) / 1048576).ToString();
                        Active.Add(item);
                    }
                    ViewBag.servers = Active;
                }

            return PartialView(MVC.MyRouter.Hotspot.Views._Onlines);
        }
        public virtual ActionResult Active()
        {
            return View();
        }

        public virtual ActionResult Users()
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
            var UsersList = _mikrotikServices.Hotspot_UsersList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            var Users = new List<Hotspot_UsersModel>();
            foreach (var item in UsersList)
            {
                item.uptime = item.uptime.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                item.limit_uptime = item.limit_uptime.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                if (item.limit_bytes_in != null)
                    if (item.limit_bytes_in != "")
                        item.limit_bytes_in = (ulong.Parse(item.limit_bytes_in) / 1048576).ToString();
                if (item.limit_bytes_out != null)
                    if (item.limit_bytes_out != "")
                        item.limit_bytes_out = (ulong.Parse(item.limit_bytes_out) / 1048576).ToString();
                if (item.limit_bytes_total != null)
                    if (item.limit_bytes_total != "")
                        item.limit_bytes_total = (ulong.Parse(item.limit_bytes_total) / 1048576).ToString();
                if (item.bytes_in != null)
                    if (item.bytes_in != "")
                        item.bytes_in = (ulong.Parse(item.bytes_in) / 1048576).ToString();
                if (item.bytes_out != null)
                    if (item.bytes_out != "")
                        item.bytes_out = (ulong.Parse(item.bytes_out) / 1048576).ToString();
                Users.Add(item);
            }
            ViewBag.servers = Users;
            return View();
        }

        public virtual ActionResult Access()
        {
            ViewBag.servers = _mikrotikServices.Hotspot_ServersList(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return View();
        }

        public virtual ActionResult LoadUsersAccess()
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    ViewBag.ipbindings = _mikrotikServices.Hotspot_IpBindings(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                }

            return PartialView(MVC.MyRouter.Hotspot.Views._UserAccess);
        }
        public virtual ActionResult LoadWebsitesAccess()
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    ViewBag.ipwalledgarden = _mikrotikServices.Hotspot_IpWalledGarden(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                }

            return PartialView(MVC.MyRouter.Hotspot.Views._WebsiteAccess);
        }

        [ValidateInput(false)]
        public virtual ActionResult IpBindigsRemove(string id)
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
            _mikrotikServices.Hotspot_IpBindingsRemove(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Access);
        }


        [ValidateInput(false)]
        public virtual ActionResult IpWalledGardenRemove(string id)
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
            _mikrotikServices.Hotspot_IpWalledGardenRemove(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Access);
        }


        [ValidateInput(false)]
        public virtual ActionResult IpBindigsEnable(string id)
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
            _mikrotikServices.Hotspot_IpBindingsEnable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Access);
        }

        [ValidateInput(false)]
        public virtual ActionResult IpBindigsDisable(string id)
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
            _mikrotikServices.Hotspot_IpBindingsDisable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Access);
        }

        [ValidateInput(false)]
        public virtual ActionResult IpWalledGardenEnable(string id)
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
            _mikrotikServices.Hotspot_IpWalledGardenEnable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Access);
        }

        [ValidateInput(false)]
        public virtual ActionResult IpWalledGardenDisable(string id)
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
            _mikrotikServices.Hotspot_IpWalledGardenDisable(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Access);
        }

        public virtual ActionResult Template()
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
        public virtual ActionResult LoadTemplateSetting()
        {
            ViewBag.Users = _mikrotikServices.Usermanager_GetAllUsers(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return PartialView(MVC.MyRouter.Hotspot.Views._TemplateSetting);
        }

        [HttpPost]
        public virtual ActionResult ActiveTemplate(ViewModels.Template.TemplateSettings setting)
        {
            //------------Save To Xml---------------------------
            var xml = new XDocument(new XElement("TemplateSetting",
                new XElement("AutoLogin", setting.AutoLogin),
                new XElement("AutoLoginAfterSec", setting.AutoLoginAfterSec??1),
                new XElement("AutoLoginUser", setting.AutoLoginUser),
                new XElement("CustomButton", setting.CustomButton),
                new XElement("CustomButtonLink", setting.CustomButtonLink),
                new XElement("CustomButtonText", setting.CustomButtonText),
                new XElement("InstagramButton", setting.InstagramButton),
                new XElement("InstagramButtonLink", setting.InstagramButtonLink),
                new XElement("PanelButton", setting.PanelButton),
                new XElement("PanelButtonText", setting.PanelButtonText),
                new XElement("Redirect", setting.Redirect),
                new XElement("RedirectToInstagram", setting.RedirectToInstagram),
                new XElement("RedirectToTelegram", setting.RedirectToTelegram),
                new XElement("RedirectToUrl", setting.RedirectToUrl),
                new XElement("RegisterButton", setting.RegisterButton),
                new XElement("RegisterButtonText", setting.RegisterButtonText),
                new XElement("ShowTrialButton", setting.ShowTrialButton),
                new XElement("TelegramButton", setting.TelegramButton),
                new XElement("TelegramButtonLink", setting.TelegramButtonLink)
                ));

            xml.Save(HostingEnvironment.MapPath(@"~\Content\Upload\TemplateSettingsXML\"+UserLogined.Id));

            //------------Connect To Router To Get---------------------------


            this.MessageSuccess(Captions.MissionSuccess,"قالب با موفقیت نصب گردید.");
            return RedirectToAction(MVC.MyRouter.Hotspot.ActionNames.Template);
        }
    }
}