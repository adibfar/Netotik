using System;
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
using Netotik.ViewModels.Identity.Security;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;
using System.Net;
using Netotik.ViewModels.Identity.UserRouter;
using System.Threading.Tasks;
using Netotik.ViewModels.Identity.UserClient;

namespace Netotik.Web.Areas.MyRouter.Controllers
{
    [Mvc5Authorize(Roles = "Router")]
    //   [BreadCrumb(Title = "UserManager", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
    //Order = 0, GlyphIcon = "icon icon-table")]
    public partial class UserManagerController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUserRouterLogClientService _UserRouterlogclientservice;
        private readonly IUnitOfWork _uow;
        private readonly ISmsService _smsService;

        public UserManagerController(
            IMikrotikServices mikrotikServices,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            IUserRouterLogClientService UserRouterlogclientservice,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _pictureService = pictureservice;
            _applicationUserManager = applicationUserManager;
            _UserRouterlogclientservice = UserRouterlogclientservice;
            _smsService = smsService;
            _uow = uow;
        }
        #endregion


        #region Usermanager

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult ResetCounter(string user, string id)
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
            //-------------------------------
            var Users = _mikrotikServices.Usermanager_GetUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, user);
            _mikrotikServices.Usermanager_ResetCounter(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, user);
            if (UserLogined.UserRouter.SmsCharge > 0 && UserLogined.UserRouter.SmsActive && UserLogined.UserRouter.SmsUserAfterResetCounter && Users.FirstOrDefault().phone != null && Users.FirstOrDefault().phone != "")
                _smsService.SendSms(Users.FirstOrDefault().phone, string.Format(Captions.SmsYourAccountCounterReset, Users.FirstOrDefault().username), UserLogined.Id);
            //--------------------------------
            _uow.SaveAllChanges();
            return RedirectToAction(MVC.MyRouter.UserManager.UserList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult CloseSession(string user, string id)
        {
            //-------------------------------
            _mikrotikServices.Usermanager_CloseSession(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, user);
            //--------------------------------
            return RedirectToAction(MVC.MyRouter.UserManager.UserList());
        }

        public virtual ActionResult PackageCreate()
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            ViewBag.limition = _mikrotikServices.Usermanager_GetAllLimition(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            ViewBag.ProfileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult PackageCreate(Netotik.ViewModels.Identity.UserClient.ProfileLimitionCreateModel model, ActionType actionType)
        {

            if (!ModelState.IsValid)
            {
                //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                return View();
            }
            else
            {
                if (Request.Form["onlineusers"].ToString() == "-1")
                    model.profile_override_shared_users = "unlimited";
                else
                    model.profile_override_shared_users = Request.Form["onlineusers"].ToString();
                model.profile_starts_at = Request.Form["startat"].ToString();
                model.profilelimition_from_time = Request.Form["fromtime"].ToString();
                model.profilelimition_till_time = Request.Form["tilltime"].ToString();
                if (Request.Form["weekdays"] != null)
                    foreach (var item in Request.Form["weekdays"])
                    {
                        switch (item)
                        {
                            case '1':
                                model.profilelimition_weekdays += "saturday,";
                                break;
                            case '2':
                                model.profilelimition_weekdays += "sunday,";
                                break;
                            case '3':
                                model.profilelimition_weekdays += "monday,";
                                break;
                            case '4':
                                model.profilelimition_weekdays += "tuesday,";
                                break;
                            case '5':
                                model.profilelimition_weekdays += "wednesday,";
                                break;
                            case '6':
                                model.profilelimition_weekdays += "thursday,";
                                break;
                            case '7':
                                model.profilelimition_weekdays += "friday,";
                                break;
                        }
                    }
                else
                    model.profilelimition_weekdays = null;
                //--------
                string online = null;
                string days = null;
                if (Request.Form["onlinemon"].ToString() != "0")
                    days = (int.Parse(Request.Form["onlinemon"].ToString()) * 30).ToString();
                if (Request.Form["onlineday"].ToString() != "0")
                {
                    if (days == null) days = "0";
                    days = (ulong.Parse(days) + ulong.Parse(Request.Form["onlineday"].ToString())).ToString();
                }
                if (days != null)
                    online += days + "d";
                if (Request.Form["onlinehour"].ToString() != "0")
                    online += Request.Form["onlinehour"].ToString() + "h";
                if (Request.Form["onlinemin"].ToString() != "0")
                    online += Request.Form["onlinemin"].ToString() + "m";
                model.limition_uptime_limit = online;
                days = null;
                string valid = null;
                // if (Request.Form["onlineusermin"].ToString() != "0" && Request.Form["onlineuserhour"].ToString() != "0" && Request.Form["onlineuserday"].ToString() != "0" && Request.Form["onlineusermon"].ToString() != "0") { valid = Request.Form["onlineusermon"].ToString() + "m" + Request.Form["onlineuserday"].ToString() + "d" + Request.Form["onlineuserhour"].ToString() + "h" + Request.Form["onlineusermin"].ToString() + "m"; }
                if (Request.Form["onlineusermon"].ToString() != "0")
                    days = (int.Parse(Request.Form["onlineusermon"].ToString()) * 30).ToString();
                if (Request.Form["onlineuserday"].ToString() != "0")
                {
                    if (days == null) days = "0";
                    days = (ulong.Parse(days) + ulong.Parse(Request.Form["onlineuserday"].ToString())).ToString();
                }
                if (days != null)
                    valid += days + "d";
                if (Request.Form["onlineuserhour"].ToString() != "0")
                    valid += Request.Form["onlineuserhour"].ToString() + "h";
                if (Request.Form["onlineusermin"].ToString() != "0")
                    valid += Request.Form["onlineusermin"].ToString() + "m";

                model.profile_validity = valid;
                //--------
                string downloadlimit = null;
                /*if (Request.Form["downloadlimit"].ToString() != "")
                    switch (Request.Form["downloadlimitB"].ToString())
                    {
                        case "1":
                            downloadlimit = (ulong.Parse(Request.Form["downloadlimit"]) * 1048576).ToString();
                            break;
                        case "2":
                            downloadlimit = (ulong.Parse(Request.Form["downloadlimit"]) * 1073741824).ToString();
                            break;
                    }
                    */
                model.limition_download_limit = downloadlimit;
                //-------------------------
                string uploadlimit = null;
                /*
                if (Request.Form["uploadlimit"].ToString() != "")
                    switch (Request.Form["uploadlimitB"].ToString())
                    {
                        case "1":
                            downloadlimit = (ulong.Parse(Request.Form["uploadlimit"]) * 1048576).ToString();
                            break;
                        case "2":
                            downloadlimit = (ulong.Parse(Request.Form["uploadlimit"]) * 1073741824).ToString();
                            break;
                    }
                    */
                model.limition_upload_limit = uploadlimit;
                //-------------------------
                string downloaduploadlimit = null;
                if (Request.Form["downloaduploadlimit"].ToString() != "")
                    switch (Request.Form["downloaduploadlimitB"].ToString())
                    {
                        case "1":
                            downloaduploadlimit = (ulong.Parse(Request.Form["downloaduploadlimit"]) * 1048576).ToString();
                            break;
                        case "2":
                            downloaduploadlimit = (ulong.Parse(Request.Form["downloaduploadlimit"]) * 1073741824).ToString();
                            break;
                    }
                model.limition_transfer_limit = downloaduploadlimit;
                //-------------------------
                string downloadrate = "";
                if (Request.Form["downloadrate"].ToString() != "")
                    switch (Request.Form["downloadrateB"].ToString())
                    {
                        case "1":
                            downloadrate = (ulong.Parse(Request.Form["downloadrate"]) * 8192).ToString();
                            break;
                        case "2":
                            downloadrate = (ulong.Parse(Request.Form["downloadrate"]) * 1024).ToString();
                            break;
                        case "3":
                            downloadrate = (ulong.Parse(Request.Form["downloadrate"]) * 8388608).ToString();
                            break;
                        case "4":
                            downloadrate = (ulong.Parse(Request.Form["downloadrate"]) * 1048576).ToString();
                            break;
                    }
                model.limition_rate_limit_rx = downloadrate;
                //-------------------------
                string uploadrate = "";
                if (Request.Form["uploadrate"].ToString() != "")
                    switch (Request.Form["uploadrateB"].ToString())
                    {
                        case "1":
                            uploadrate = (ulong.Parse(Request.Form["uploadrate"]) * 8192).ToString();
                            break;
                        case "2":
                            uploadrate = (ulong.Parse(Request.Form["uploadrate"]) * 1024).ToString();
                            break;
                        case "3":
                            uploadrate = (ulong.Parse(Request.Form["uploadrate"]) * 8388608).ToString();
                            break;
                        case "4":
                            uploadrate = (ulong.Parse(Request.Form["uploadrate"]) * 1048576).ToString();
                            break;
                    }
                model.limition_rate_limit_tx = uploadrate;
                //-------------------------
                var UsermanProfile = new Netotik.ViewModels.Identity.UserClient.ProfileLimitionCreateModel()
                {
                    limition_address_list = model.limition_address_list,
                    limition_download_limit = model.limition_download_limit,
                    limition_group_name = model.limition_group_name,
                    limition_ip_pool = model.limition_ip_pool,
                    limition_name = model.limition_name,
                    limition_owner = UserLogined.UserRouter.Userman_Customer,
                    limition_rate_limit_min_tx = model.limition_rate_limit_min_tx,
                    limition_rate_limit_rx = model.limition_rate_limit_rx,
                    limition_rate_limit_tx = model.limition_rate_limit_tx,
                    limition_transfer_limit = model.limition_transfer_limit,
                    limition_upload_limit = model.limition_upload_limit,
                    limition_uptime_limit = model.limition_uptime_limit,
                    profilelimition_from_time = model.profilelimition_from_time,
                    profilelimition_limitation = model.profilelimition_limitation,
                    profilelimition_profile = model.profilelimition_profile,
                    profilelimition_till_time = model.profilelimition_till_time,
                    profilelimition_weekdays = model.profilelimition_weekdays,
                    profile_name = model.profile_name,
                    profile_name_for_users = model.profile_name_for_users,
                    profile_override_shared_users = model.profile_override_shared_users,
                    profile_owner = UserLogined.UserRouter.Userman_Customer,
                    profile_price = model.profile_price,
                    profile_starts_at = model.profile_starts_at,
                    profile_validity = model.profile_validity
                };

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
                if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                    return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
                }
                //-------------------------------
                if (_mikrotikServices.Usermanager_IsProfileExist(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, UsermanProfile))
                {
                    //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                    return RedirectToAction(MVC.MyRouter.UserManager.PackageCreate());
                }
                else
                {

                    _mikrotikServices.Usermanager_ProfileCreate(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, UsermanProfile);
                }
                return RedirectToAction(MVC.MyRouter.UserManager.PackageList());
            }
        }

        public virtual ActionResult UserList()
        {

            //-------------------------------
            //if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            //{
            //    this.MessageError(Captions.Error, Captions.IPPORTClientError);
            //    return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            //}
            //if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            //{
            //    this.MessageError(Captions.Error, Captions.UserPasswordClientError);
            //    return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            //}
            //if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            //{
            //    this.MessageError(Captions.Error, Captions.UsermanagerClientError);
            //    return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            //}


            return View();
        }

        public virtual ActionResult LoadUsers()
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    var userlist = _mikrotikServices.Usermanager_GetAllUsers(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                    var UserListModel = new List<Netotik.ViewModels.Identity.UserClient.UserModel>();
                    foreach (var item in userlist)
                    {
                        if (item.download_used == "0" || item.download_used == "")
                            item.download_used = "0";
                        if (item.upload_used == "0" || item.upload_used == "")
                            item.upload_used = "0";
                        item.download_used = (ulong.Parse(item.download_used) / 1048576).ToString();
                        if (item.last_seen == "never")
                        {
                            item.last_seen = item.last_seen.Replace("never", Captions.NoConnection);
                            item.last_seenT = item.last_seen.Replace("never", Captions.NoConnection);
                        }
                        else
                        {
                            var last_seenT = item.last_seen.Split(' ');
                            var last_seen = item.last_seen.Split(' ');
                            last_seen[0] = Infrastructure.EnglishConvertDate.ConvertToFa(last_seen[0], "d");
                            item.last_seen = last_seen[0] + " " + last_seen[1];


                            last_seenT[0] = Infrastructure.EnglishConvertDate.ConvertToFa(last_seenT[0], "D");
                            item.last_seenT = last_seenT[0] + " " + last_seenT[1];
                        }
                        item.shared_users = item.shared_users.Replace("unlimited", Captions.Unlimited);
                        item.upload_used = (ulong.Parse(item.upload_used) / 1048576).ToString();
                        item.uptime_used = item.uptime_used.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                        UserListModel.Add(item);
                    }
                    ViewBag.Users = UserListModel;
                }

            return PartialView(MVC.MyRouter.UserManager.Views._Users);
        }

        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult Userdisable(string id)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            _mikrotikServices.Usermanager_DisableUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.UserManager.ActionNames.UserList);
        }

        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult Userremove(string id)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            var Users = _mikrotikServices.Usermanager_GetUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            _mikrotikServices.Usermanager_RemoveUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            if (UserLogined.UserRouter.SmsCharge > 0 && UserLogined.UserRouter.SmsActive && UserLogined.UserRouter.SmsUserAfterDelete && Users.FirstOrDefault().phone != null && Users.FirstOrDefault().phone != "")
                _smsService.SendSms(Users.FirstOrDefault().phone, string.Format(Captions.SmsUserAccountRemoved, Users.FirstOrDefault().username), UserLogined.Id);
            _uow.SaveAllChanges();
            return RedirectToAction(MVC.MyRouter.UserManager.ActionNames.UserList);
        }

        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult ProfileRemove(string id)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            _mikrotikServices.Usermanager_RemoveProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.UserManager.ActionNames.PackageList);
        }

        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult Userenable(string id)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            _mikrotikServices.Usermanager_EnableUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            return RedirectToAction(MVC.MyRouter.UserManager.ActionNames.UserList);
        }

        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult PackageDetails(string id)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            var profile = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            var Limition = _mikrotikServices.Usermanager_GetAllLimition(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            var profileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            if (profile == null || Limition == null || profileLimition == null)
                return RedirectToAction(MVC.MyRouter.UserManager.ActionNames.PackageList);
            var resualtmodel = new Netotik.ViewModels.Identity.UserClient.ProfileLimitionView();
            foreach (var item in profile)
                if (item.id == id)
                    resualtmodel.UsermanProfile = item;
            foreach (var item2 in profileLimition)
                if (item2.profile == resualtmodel.UsermanProfile.name)
                {
                    resualtmodel.UsermanProfileLimition = item2;
                    foreach (var item3 in Limition)
                        if (resualtmodel.UsermanProfileLimition.limitation == item3.name)
                            resualtmodel.UsermanLimition = item3;
                }
            if (resualtmodel.UsermanProfile.validity != null)
                resualtmodel.UsermanProfile.validity = resualtmodel.UsermanProfile.validity == "0s" ? Captions.Unlimited : resualtmodel.UsermanProfile.validity.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend);
            if (resualtmodel.UsermanProfile.starts_at != null)
                resualtmodel.UsermanProfile.starts_at = resualtmodel.UsermanProfile.starts_at.Replace("logon", Captions.FirstConnection).Replace("now", Captions.PlanBind);
            if (resualtmodel.UsermanProfile.override_shared_users != null)
                resualtmodel.UsermanProfile.override_shared_users = resualtmodel.UsermanProfile.override_shared_users.Replace("unlimited", Captions.Unlimited);
            if (resualtmodel.UsermanProfileLimition != null)
            {
                if (resualtmodel.UsermanProfileLimition.from_time != null)
                    resualtmodel.UsermanProfileLimition.from_time = resualtmodel.UsermanProfileLimition.from_time.Replace("d", Captions.Day).Replace("s", Captions.Secend).Replace("m", Captions.Minute).Replace("h", Captions.Hour);
                if (resualtmodel.UsermanProfileLimition.till_time != null)
                    resualtmodel.UsermanProfileLimition.till_time = resualtmodel.UsermanProfileLimition.till_time.Replace("d", Captions.Day).Replace("s", Captions.Secend).Replace("m", Captions.Minute).Replace("h", Captions.Hour);
                if (resualtmodel.UsermanProfileLimition.weekdays != null)
                    resualtmodel.UsermanProfileLimition.weekdays = resualtmodel.UsermanProfileLimition.weekdays.Replace("friday", Captions.Friday).Replace("thursday", Captions.Thursday).Replace("wednesday", Captions.Wednesday).Replace("tuesday", Captions.Tuesday).Replace("monday", Captions.Monday).Replace("sunday", Captions.Sunday).Replace("saturday", Captions.Saturday);
            }
            if (resualtmodel.UsermanLimition != null)
                if (resualtmodel.UsermanLimition.uptime_limit != null)
                    resualtmodel.UsermanLimition.uptime_limit = resualtmodel.UsermanLimition.uptime_limit == "0s" ? Captions.Unlimited : resualtmodel.UsermanLimition.uptime_limit.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend);
            if (resualtmodel.UsermanLimition.rate_limit_rx == "")
                resualtmodel.UsermanLimition.rate_limit_rx = "0";
            if (resualtmodel.UsermanLimition.rate_limit_tx == "")
                resualtmodel.UsermanLimition.rate_limit_tx = "0";
            return View(resualtmodel);
        }
        public virtual ActionResult LoadSessions(string userOrid)
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    var session = _mikrotikServices.Usermanager_UserSession(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, userOrid);
                    var UserSession = new List<Netotik.ViewModels.Identity.UserClient.UserSessionModel>();
                    foreach (var SessionItem in session)
                    {
                        var from_time = SessionItem.from_time;
                        var till_time = SessionItem.till_time;
                        SessionItem.from_time = Infrastructure.EnglishConvertDate.ConvertToFa(SessionItem.from_time.Split(' ')[0], "d") + " " + SessionItem.from_time.Split(' ')[1];
                        SessionItem.till_time = Infrastructure.EnglishConvertDate.ConvertToFa(SessionItem.till_time.Split(' ')[0], "d") + " " + SessionItem.till_time.Split(' ')[1];
                        SessionItem.from_timeT = Infrastructure.EnglishConvertDate.ConvertToFa(from_time.Split(' ')[0], "D") + " " + from_time.Split(' ')[1];
                        SessionItem.till_timeT = Infrastructure.EnglishConvertDate.ConvertToFa(till_time.Split(' ')[0], "D") + " " + till_time.Split(' ')[1];
                        UserSession.Add(SessionItem);
                    }
                    ViewBag.session = UserSession;

                }

            return PartialView(MVC.MyRouter.UserManager.Views._UserDetailsSessions);
        }

        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult UserDetails(string id)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            var users = _mikrotikServices.Usermanager_GetUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            foreach (var item in users)
                if (item.id == id)
                {
                    var profile = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                    var Limition = _mikrotikServices.Usermanager_GetAllLimition(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                    var profileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                    var UserProfile = new Netotik.ViewModels.Identity.UserClient.ProfileModel();
                    var UserProfileLimition = new Netotik.ViewModels.Identity.UserClient.ProfileLimitionModel();
                    var UserLimition = new Netotik.ViewModels.Identity.UserClient.LimitionModel();
                    if (profile != null)
                        foreach (var item0 in profile)
                            if (item0.name == item.actual_profile)
                                UserProfile = item0;

                    if (UserProfile != null)
                        foreach (var item2 in profileLimition)
                            if (item2.profile == UserProfile.name)
                            {
                                UserProfileLimition = item2;
                                if (Limition != null)
                                    foreach (var item3 in Limition)
                                        if (UserProfileLimition.limitation == item3.name)
                                            UserLimition = item3;
                            }
                    ViewBag.download_limit = UserLimition.download_limit;
                    ViewBag.upload_limit = UserLimition.upload_limit;
                    ViewBag.transfer_limit = UserLimition.transfer_limit;
                    ViewBag.rate_limit_rx = UserLimition.rate_limit_rx == "" ? "0" : UserLimition.rate_limit_rx;
                    ViewBag.rate_limit_tx = UserLimition.rate_limit_tx == "" ? "0" : UserLimition.rate_limit_tx;
                    decimal Downloadlimit = 0;
                    if (UserLimition.transfer_limit == null) UserLimition.transfer_limit = "0";
                    if (UserLimition.transfer_limit != "0") Downloadlimit += ulong.Parse(UserLimition.transfer_limit);
                    if (UserLimition.upload_limit == null) UserLimition.upload_limit = "0";
                    if (UserLimition.upload_limit != "0") Downloadlimit += ulong.Parse(UserLimition.upload_limit);
                    if (UserLimition.download_limit == null) UserLimition.download_limit = "0";
                    if (UserLimition.download_limit != "0") Downloadlimit += ulong.Parse(UserLimition.download_limit);
                    if (UserLimition.download_limit != "" && item.download_used != "" && Downloadlimit > 0)
                        ViewBag.download_remain = (Downloadlimit - ulong.Parse(item.download_used)).ToString();
                    if (UserLimition.upload_limit != "" && item.upload_used != "")
                        ViewBag.upload_remain = (ulong.Parse(UserLimition.upload_limit) - ulong.Parse(item.upload_used)).ToString();
                    if (UserLimition.uptime_limit != null)
                        ViewBag.uptime_limit = UserLimition.uptime_limit == "0s" ? Captions.Unlimited : UserLimition.uptime_limit.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend);
                    if (UserProfile.validity != null)
                        ViewBag.validity = UserProfile.validity.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend);
                    if (item.shared_users != null)
                        item.shared_users = item.shared_users.Replace("unlimited", Captions.Unlimited);
                    ViewBag.price = UserProfile.price;
                    if (UserProfileLimition.from_time != null)
                        ViewBag.from_time = UserProfileLimition.from_time.Replace("d", Captions.Day).Replace("s", Captions.Secend).Replace("m", Captions.Minute).Replace("h", Captions.Hour);
                    if (UserProfileLimition.till_time != null)
                        ViewBag.till_time = UserProfileLimition.till_time.Replace("d", Captions.Day).Replace("s", Captions.Secend).Replace("m", Captions.Minute).Replace("h", Captions.Hour);
                    if (UserProfileLimition.weekdays != null)
                        ViewBag.weekdays = UserProfileLimition.weekdays.Replace("friday", Captions.Friday).Replace("thursday", Captions.Thursday).Replace("wednesday", Captions.Wednesday).Replace("tuesday", Captions.Tuesday).Replace("monday", Captions.Monday).Replace("sunday", Captions.Sunday).Replace("saturday", Captions.Saturday);
                    if (item.uptime_used != null)
                        item.uptime_used = item.uptime_used.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                    if (item.last_seen != null)
                        if (item.last_seen == "never")
                        {
                            item.last_seen = item.last_seen.Replace("never", Captions.NoConnection);
                            item.last_seenT = item.last_seen.Replace("never", Captions.NoConnection);
                        }
                        else
                        {
                            var last_seenT = item.last_seen.Split(' ');
                            var last_seen = item.last_seen.Split(' ');
                            last_seen[0] = Infrastructure.EnglishConvertDate.ConvertToFa(last_seen[0], "d");
                            item.last_seen = last_seen[0] + " " + last_seen[1];

                            last_seenT[0] = Infrastructure.EnglishConvertDate.ConvertToFa(last_seenT[0], "D");
                            item.last_seenT = last_seenT[0] + " " + last_seenT[1];
                        }
                    if (item.disabled != null)
                        item.disabled = item.disabled.Replace("false", Captions.Enabled).Replace("true", Captions.Disabled);
                    if (item.download_used == "")
                        item.download_used = "0";
                    if (ViewBag.download_remain == null || ViewBag.download_remain == "")
                        ViewBag.download_remain = "0";





                    return View(item);
                }
            return View();
        }
        private static int GetMonth(string monthName)
        {
            switch (monthName)
            {
                case "jan":
                    return 1;
                case "feb":
                    return 2;
                case "mar":
                    return 3;
                case "apr":
                    return 4;
                case "may":
                    return 5;
                case "jun":
                    return 6;
                case "jul":
                    return 7;
                case "aug":
                    return 8;
                case "sep":
                    return 9;
                case "oct":
                    return 10;
                case "nov":
                    return 11;
                case "dec":
                    return 12;

            }
            return 0;
        }

        public virtual ActionResult UserCreate()
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            //            ViewBag.Customers = _mikrotikServices.GetAllCustomers(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult UserCreate(Netotik.ViewModels.Identity.UserClient.UserRegisterModel model, ActionType actionType)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            model.customer = UserLogined.UserRouter.Userman_Customer;
            if (!ModelState.IsValid)
            {
                //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                return RedirectToAction(MVC.MyRouter.UserManager.UserCreate());
            }
            else
            {
                var Usermanuser = new Netotik.ViewModels.Identity.UserClient.UserRegisterModel()
                {
                    username = model.username,
                    email = model.email,
                    phone = model.phone,
                    first_name = model.first_name,
                    last_name = model.last_name,
                    password = model.password,
                    //comment = model.comment,
                    Age = model.Age,
                    Birthday = model.Birthday,
                    CreateDate = DateTime.Now,
                    IsMale = model.IsMale,
                    MarriageDate = model.MarriageDate,
                    NationalCode = model.NationalCode,
                    customer = model.customer,
                    location = model.location,
                    profile = Request.Form["profile"].ToString()
                };
                if (_mikrotikServices.Usermanager_IsUserExist(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, Usermanuser.username))
                {
                    this.MessageError(Captions.Error, Captions.ExistError);
                }
                else
                {
                    _mikrotikServices.Usermanager_UserCreate(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, Usermanuser);
                    if (UserLogined.UserRouter.SmsCharge > 0 && UserLogined.UserRouter.SmsActive && UserLogined.UserRouter.SmsUserAfterCreateWithAdmin)
                        _smsService.SendSms(model.phone, string.Format(Captions.SmsUserAccountCreated, model.username, model.password), UserLogined.Id);
                    else
                    {
                        if (model.SendSmsNow)
                            _smsService.SendSms(model.phone, string.Format(Captions.SmsUserAccountCreated, model.username, model.password), UserLogined.Id);
                    }
                }
                _uow.SaveAllChanges();
                return RedirectToAction(MVC.MyRouter.UserManager.UserList());
            }
        }

        public virtual ActionResult PackageList()
        {

            ////-------------------------------
            //if (!_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            //{
            //    this.MessageError(Captions.Error, Captions.IPPORTClientError);
            //    return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            //}
            //if (!_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            //{
            //    this.MessageError(Captions.Error, Captions.UserPasswordClientError);
            //    return RedirectToAction(MVC.MyRouter.Home.ActionNames.MikrotikConf, MVC.MyRouter.Home.Name, new { area = MVC.MyRouter.Name });
            //}
            //if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            //{
            //    this.MessageError(Captions.Error, Captions.UsermanagerClientError);
            //    return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            //}

            return View();
        }

        public virtual ActionResult LoadPackages()
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    var Profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                    var RouterProfiles = new List<Netotik.ViewModels.Identity.UserClient.ProfileModel>();
                    foreach (var item in Profiles)
                    {
                        if (item.starts_at != null)
                            item.starts_at = item.starts_at.Replace("logon", Captions.FirstConnection).Replace("now", Captions.PlanBind);
                        item.validity = item.validity == "0s" ? Captions.Unlimited : item.validity.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend); ;
                        RouterProfiles.Add(item);
                    }
                    ViewBag.packagelist = RouterProfiles;
                }

            return PartialView(MVC.MyRouter.UserManager.Views._Packages);
        }

        public virtual ActionResult Report()
        {
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult UserEdit(string id)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            var model = _mikrotikServices.Usermanager_GetUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, id);
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            foreach (var item in model)
                if (item.id == id)
                {
                    var editModel = new Netotik.ViewModels.Identity.UserClient.UserEditModel
                    {
                        id = item.id,
                        first_name = item.first_name,
                        //comment = item.comment,
                        Age = item.Age,
                        Birthday = item.Birthday,
                        CreateDate = item.CreateDate,
                        MarriageDate = item.MarriageDate,
                        NationalCode = item.NationalCode,
                        IsMale = item.IsMale,
                        disabled = item.disabled,
                        customer = item.customer,
                        email = item.email,
                        ip_address = item.ip_address,
                        last_name = item.last_name,
                        location = item.location,
                        password = item.password,
                        phone = item.phone,
                        profile = item.actual_profile,
                        shared_users = item.shared_users,
                        username = item.username
                    };
                    return View(editModel);
                }
            //            ViewBag.Customers = _mikrotikServices.GetAllCustomers(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult UserEdit_Save(Netotik.ViewModels.Identity.UserClient.UserEditModel model, ActionType actionType)
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.MyRouter.Home.ActionNames.Index);
            }
            //-------------------------------
            if (!ModelState.IsValid)
            {
                //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                return View();
            }
            else
            {
                model.customer = UserLogined.UserRouter.Userman_Customer;
                model.profile = Request.Form["profile"].ToString();
                var model2 = _mikrotikServices.Usermanager_GetUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, model.username);
                foreach (var item in model2)
                    if (item.id == model.id)
                    {
                        if (model.profile == item.actual_profile)
                            model.profile = "";
                        else
                        if (model.phone != null && model.phone != "")
                            if (UserLogined.UserRouter.SmsCharge > 0 && UserLogined.UserRouter.SmsActive && UserLogined.UserRouter.SmsUserAfterChangePackage)
                            {
                                _smsService.SendSms(model.phone, string.Format(Captions.SmsUserBuyPlan, model.username), UserLogined.Id);
                            }
                        model.CreateDate = item.CreateDate;
                    }
                _mikrotikServices.Usermanager_UserEdit(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, model);
                if (model.password != model2.FirstOrDefault().password)
                {
                    if (UserLogined.UserRouter.SmsCharge > 0 && UserLogined.UserRouter.SmsActive && UserLogined.UserRouter.SmsAdminChangeUserPassword)
                    {
                        if (model.phone != null || model.phone != "")
                            _smsService.SendSms(model.phone, string.Format(Captions.SmsUserPasswordChange, model.username, model.password), UserLogined.Id);
                    }
                }
                _uow.SaveAllChanges();
                return RedirectToAction(MVC.MyRouter.UserManager.UserList());
            }
        }
        #endregion


        [BreadCrumb(Title = "RegisterSetting", Order = 1)]
        public virtual ActionResult RegisterSetting()
        {
            ViewBag.ResellerCode = UserLogined.UserRouter.RouterCode;
            return PartialView(MVC.MyRouter.UserManager.Views.RegisterSetting, _userManager.GetRouterRegisterSetting(UserLogined.Id));
        }


        [BreadCrumb(Title = "RegisterSetting", Order = 1)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> RegisterSetting(RegisterSettingModel model)
        {
            ViewBag.ResellerCode = UserLogined.UserRouter.RouterCode;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Id = UserLogined.Id;

            await _userManager.UpdateRouterRegisterSettingAsync(model);
            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);

            return RedirectToAction(MVC.MyRouter.UserManager.RegisterSetting());
        }



        [NonAction]
        private void PopulatePermissions(params string[] selectedpermissions)
        {
            var permissions = AssignablePermissionToClient.GetAsSelectListItems();

            if (selectedpermissions != null)
            {
                permissions.ForEach(a => a.Selected = selectedpermissions.Any(s => s == a.Value));
            }

            ViewBag.ClientPermissions = permissions;
        }
        public virtual ActionResult ClientArea()
        {
            ViewBag.RouterCode = UserLogined.UserRouter.RouterCode;
            PopulatePermissions(_applicationUserManager.FindClientPermissions(UserLogined.Id).ToArray());
            return View();
        }

        [HttpPost]
        public virtual ActionResult ClientArea(ViewModels.Identity.UserRouter.ProfileModel model)
        {
            PopulatePermissions(model.ClientPermissionNames);

            model.Id = UserLogined.Id;
            _applicationUserManager.UpdateUserClientPermissions(model);
            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.MyRouter.UserManager.ActionNames.ClientArea);
        }

        public virtual ActionResult Online()
        {
            return View();
        }
        public virtual ActionResult LoadOnlines()
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    var OnlineUsers = _mikrotikServices.Usermanager_GetActiveSessions(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                    var NewSessions = new List<Netotik.ViewModels.Identity.UserClient.UserSessionModel>();
                    foreach (var item in OnlineUsers)
                    {
                        item.download = item.download == null || item.download == "" ? "" : (ulong.Parse(item.download) / 1048576).ToString();
                        item.upload = item.upload == null || item.upload == "" ? "" : (ulong.Parse(item.upload) / 1048576).ToString();
                        item.uptime = item.uptime.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                        item.user = item.user == null | item.user == "" ? "-UserError-" : item.user == null || item.user == null | item.user == "" ? "-UserError-" : item.user == "" ? "-UserError-" : item.user == null | item.user == "" ? "-UserError-" : item.user;
                        var from_time = item.from_time;
                        item.from_time = Infrastructure.EnglishConvertDate.ConvertToFa(item.from_time.Split(' ')[0], "d") + " " + item.from_time.Split(' ')[1];
                        item.from_timeT = Infrastructure.EnglishConvertDate.ConvertToFa(from_time.Split(' ')[0], "D") + " " + from_time.Split(' ')[1];

                        NewSessions.Add(item);
                    }
                    ViewBag.Sessions = NewSessions;
                }

            return PartialView(MVC.MyRouter.UserManager.Views._Onlines);
        }
        public virtual ActionResult LoadProfiles()
        {
            if (_mikrotikServices.IP_Port_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                if (_mikrotikServices.User_Pass_Check(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password))
                {
                    var listProfiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password);
                    ViewBag.profiles = new SelectList(listProfiles, "name", "name", UserLogined.UserRouter.UserRouterRegisterSetting.ProfileName);
                }

            return PartialView(MVC.MyRouter.UserManager.Views._Profiles);
        }


        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult GetUserLogRequest(string Id)
        {
            var userclient = _mikrotikServices.Usermanager_GetUser(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, Id).FirstOrDefault();

            System.Globalization.PersianCalendar cal = new System.Globalization.PersianCalendar();
            var year = cal.GetYear(DateTime.Now);
            var month = cal.GetMonth(DateTime.Now);
            var day = cal.GetDayOfMonth(DateTime.Now);

            var yearList = new List<SelectListItem>();
            for (int i = 1396; i <= year; i++)
                yearList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = i == year });

            var monthList = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
                monthList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = i == month });

            var dayList = new List<SelectListItem>();
            for (int i = 1; i <= 31; i++)
                dayList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = i == day });


            ViewBag.Years = yearList;
            ViewBag.Months = monthList;
            ViewBag.Days = dayList;

            var model = new GetUserLogModel()
            {
                Day = day,
                Month = month,
                Year = year,
                Name = userclient.username,
                UserId = Id
            };
            return PartialView(MVC.MyRouter.UserManager.Views._GetUserLog, model);
        }


        [HttpPost]
        public virtual ActionResult GetUserLog(GetUserLogModel model)
        {
            var date = PersianDate.ConvertDate.ToEn(model.Year,model.Month,model.Day);
            var FromTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(model.FromTime.Split(':')[0]), int.Parse(model.FromTime.Split(':')[1]), 0);
            var ToTime = new DateTime(date.Year, date.Month, date.Day, int.Parse(model.ToTime.Split(':')[0]), int.Parse(model.ToTime.Split(':')[1]), 59);
            var Logs = _UserRouterlogclientservice.GetList(UserLogined.Id, FromTime, ToTime);

            var UserSessions = _mikrotikServices.Usermanager_UserSession(UserLogined.UserRouter.R_Host, UserLogined.UserRouter.R_Port, UserLogined.UserRouter.R_User, UserLogined.UserRouter.R_Password, model.Name);
            var UsersLogs = new List<UserWebsiteLogsWithSessionsModel>();
            foreach (var user in UserSessions)
            {
                int month = GetMonth(user.from_time.Split(' ')[0].Split('/')[0]);
                int day = Int32.Parse(user.from_time.Split(' ')[0].Split('/')[1]);
                int year = Int32.Parse(user.from_time.Split(' ')[0].Split('/')[2]);
                int hour = Int32.Parse(user.from_time.Split(' ')[1].Split(':')[0]);
                int min = Int32.Parse(user.from_time.Split(' ')[1].Split(':')[1]);
                int sec = Int32.Parse(user.from_time.Split(' ')[1].Split(':')[2]);
                DateTime UserFromTime = new DateTime(year, month, day, hour, min, sec);
                month = GetMonth(user.till_time.Split(' ')[0].Split('/')[0]);
                day = Int32.Parse(user.till_time.Split(' ')[0].Split('/')[1]);
                year = Int32.Parse(user.till_time.Split(' ')[0].Split('/')[2]);
                hour = Int32.Parse(user.till_time.Split(' ')[1].Split(':')[0]);
                min = Int32.Parse(user.till_time.Split(' ')[1].Split(':')[1]);
                sec = Int32.Parse(user.till_time.Split(' ')[1].Split(':')[2]);
                DateTime UserTillTime = new DateTime(year, month, day, hour, min, sec);

                UsersLogs.AddRange(Logs.Where(x =>
                x.MikrotikCreateDate < UserTillTime &&
                x.MikrotikCreateDate > UserFromTime &&
                x.SrcIp == user.user_ip
                ).Select(x => new UserWebsiteLogsWithSessionsModel
                {
                    DstPort = x.DstPort,
                    SrcPort = x.SrcPort,
                    acct_session_id = user.acct_session_id,
                    active = user.active,
                    calling_station_id = user.calling_station_id,
                    customer = user.customer,
                    download = user.download,
                    DstIp = x.DstIp,
                    from_time = UserFromTime.ToString(),
                    till_time = UserTillTime.ToString(),
                    host_ip = user.host_ip,
                    Method = x.Method,
                    MikrotikCreateDate = x.MikrotikCreateDate,
                    user = user.user,
                    nas_port = user.nas_port,
                    nas_port_id = user.nas_port_id,
                    nas_port_type = user.nas_port_type,
                    SrcIp = x.SrcIp,
                    SrcMac = x.SrcMac,
                    status = user.status,
                    terminate_cause = user.terminate_cause,
                    upload = user.upload,
                    uptime = user.uptime,
                    Url = x.Url,
                    user_ip = user.user_ip
                }).ToList());
            }
            return PartialView();
        }

    }
}