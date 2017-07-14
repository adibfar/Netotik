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

namespace Netotik.Web.Areas.Company.Controllers
{
    [Mvc5Authorize(Roles = "Company")]
    [BreadCrumb(Title = "یوزرمنیجر", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class UserManagerController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _uow;

        public UserManagerController(
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


        #region Usermanager
        
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult ResetCounter(string user, string id)
        {
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.Usermanager_ResetCounter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, user);
            //--------------------------------
            return RedirectToAction(MVC.Company.UserManager.UserList());
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult CloseSession(string user, string id)
        {
            //-------------------------------
            _mikrotikServices.Usermanager_CloseSession(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, user);
            //--------------------------------
            return RedirectToAction(MVC.Company.UserManager.UserList());
        }
        
        public virtual ActionResult PackageCreate()
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            ViewBag.limition = _mikrotikServices.Usermanager_GetAllLimition(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            ViewBag.ProfileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
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
                if (Request.Form["downloadlimit"].ToString() != "")
                    switch (Request.Form["downloadlimitB"].ToString())
                    {
                        case "1":
                            downloadlimit = (ulong.Parse(Request.Form["downloadlimit"]) * 1048576).ToString();
                            break;
                        case "2":
                            downloadlimit = (ulong.Parse(Request.Form["downloadlimit"]) * 1073741824).ToString();
                            break;
                    }
                model.limition_download_limit = downloadlimit;
                //-------------------------
                string uploadlimit = null;
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
                    limition_owner = UserLogined.UserCompany.Userman_Customer,
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
                    profile_owner = UserLogined.UserCompany.Userman_Customer,
                    profile_price = model.profile_price,
                    profile_starts_at = model.profile_starts_at,
                    profile_validity = model.profile_validity
                };

                //-------------------------------
                if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
                {
                    this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                    return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
                }
                if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
                {
                    this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                    return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
                }
                if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
                {
                    this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                    return RedirectToAction(MVC.Company.Home.ActionNames.Index);
                }
                //-------------------------------
                if (_mikrotikServices.Usermanager_IsProfileExist(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, UsermanProfile))
                {
                    //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                    return RedirectToAction(MVC.Company.UserManager.PackageCreate());
                }
                else
                {

                    _mikrotikServices.Usermanager_ProfileCreate(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, UsermanProfile);
                }
                return RedirectToAction(MVC.Company.UserManager.PackageList());
            }
        }
        
        public virtual ActionResult UserList()
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            if (_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                var userlist = _mikrotikServices.Usermanager_GetAllUsers(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
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
                        item.last_seen = item.last_seen.Replace("never", " بدون اتصال ");
                        item.last_seenT = item.last_seen.Replace("never", " بدون اتصال ");
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
                    item.shared_users = item.shared_users.Replace("unlimited", " بدون محدودیت ");
                    item.upload_used = (ulong.Parse(item.upload_used)/ 1048576).ToString();
                    item.uptime_used= item.uptime_used.Replace("d", " روز ").Replace("w", " هفته ").Replace("h", " ساعت ").Replace("m", " دقیقه ").Replace("s", " ثانیه ").Replace("never", " بدون اتصال ");
                    UserListModel.Add(item);
                }
                ViewBag.userlist = UserListModel;
            }
                  
            /*
            var mikrotik = new MikrotikAPI();
            mikrotik.MK("192.168.216.128", 8728);
            if (!mikrotik.Login("admin", "")) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/tool/user-manager/profile/add", true);
            ViewBag.test = mikrotik.Read();*/
            return View();
        }
        
        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult Userdisable(string id)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            _mikrotikServices.Usermanager_DisableUser(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.UserManager.ActionNames.UserList);
        }
        
        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult Userremove(string id)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            _mikrotikServices.Usermanager_RemoveUser(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.UserManager.ActionNames.UserList);
        }
        
        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult ProfileRemove(string id)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            _mikrotikServices.Usermanager_RemoveProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.UserManager.ActionNames.PackageList);
        }
        
        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult Userenable(string id)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            _mikrotikServices.Usermanager_EnableUser(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.UserManager.ActionNames.UserList);
        }
        
        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult PackageDetails(string id)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            var profile = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            var Limition = _mikrotikServices.Usermanager_GetAllLimition(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            var profileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            if (profile == null || Limition == null || profileLimition == null)
                return RedirectToAction(MVC.Company.UserManager.ActionNames.PackageList);
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
                resualtmodel.UsermanProfile.validity = resualtmodel.UsermanProfile.validity.Replace("d", " روز ").Replace("w", " هفته ").Replace("h", " ساعت ").Replace("m", " دقیقه ").Replace("s", " ثانیه ");
            if (resualtmodel.UsermanProfile.starts_at != null)
                resualtmodel.UsermanProfile.starts_at = resualtmodel.UsermanProfile.starts_at.Replace("logon", " زمان اولین اتصال ").Replace("now", " زمان انتساب پکیج ");
            if (resualtmodel.UsermanProfile.override_shared_users != null)
                resualtmodel.UsermanProfile.override_shared_users = resualtmodel.UsermanProfile.override_shared_users.Replace("unlimited", " بدون محدودیت ");
            if (resualtmodel.UsermanProfileLimition != null)
            {
                if (resualtmodel.UsermanProfileLimition.from_time != null)
                    resualtmodel.UsermanProfileLimition.from_time = resualtmodel.UsermanProfileLimition.from_time.Replace("d", " روز ").Replace("s", " ثانیه ").Replace("m", " دقیقه ").Replace("h", " ساعت ");
                if (resualtmodel.UsermanProfileLimition.till_time != null)
                    resualtmodel.UsermanProfileLimition.till_time = resualtmodel.UsermanProfileLimition.till_time.Replace("d", " روز ").Replace("s", " ثانیه ").Replace("m", " دقیقه ").Replace("h", " ساعت ");
                if (resualtmodel.UsermanProfileLimition.weekdays != null)
                    resualtmodel.UsermanProfileLimition.weekdays = resualtmodel.UsermanProfileLimition.weekdays.Replace("friday", " جمعه ").Replace("thursday", " پنجشنبه ").Replace("wednesday", " چهارشنبه ").Replace("tuesday", " سه شنبه ").Replace("monday", " دوشنبه ").Replace("sunday", " یکشنبه ").Replace("saturday", "شنبه ");
            }
            if (resualtmodel.UsermanLimition != null)
                if (resualtmodel.UsermanLimition.uptime_limit != null)
                    resualtmodel.UsermanLimition.uptime_limit = resualtmodel.UsermanLimition.uptime_limit.Replace("d", " روز ").Replace("w", " هفته ").Replace("h", " ساعت ").Replace("m", " دقیقه ").Replace("s", " ثانیه ");
            return View(resualtmodel);
        }
        
        [ValidateInput(false)]
        [HttpPost]
        public virtual ActionResult UserDetails(string id)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            var users = _mikrotikServices.Usermanager_GetUser(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password,id);
            foreach (var item in users)
                if (item.id == id)
                {
                    var session = _mikrotikServices.Usermanager_UserSession(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, item.username);
                    var profile = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
                    var Limition = _mikrotikServices.Usermanager_GetAllLimition(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
                    var profileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
                    var UserProfile = new Netotik.ViewModels.Identity.UserClient.ProfileModel();
                    var UserProfileLimition = new Netotik.ViewModels.Identity.UserClient.ProfileLimitionModel();
                    var UserLimition = new Netotik.ViewModels.Identity.UserClient.LimitionModel();
                    var UserSession = new List<Netotik.ViewModels.Identity.UserClient.UserSessionModel>();
                    foreach(var SessionItem in session)
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
                    ViewBag.rate_limit_rx = UserLimition.rate_limit_rx;
                    ViewBag.rate_limit_tx = UserLimition.rate_limit_tx;
                    decimal Downloadlimit = 0;
                    if (UserLimition.transfer_limit == null) UserLimition.transfer_limit = "0";
                    if (UserLimition.transfer_limit != "0") Downloadlimit += ulong.Parse(UserLimition.transfer_limit);
                    if (UserLimition.upload_limit == null) UserLimition.upload_limit = "0";
                    if (UserLimition.upload_limit != "0") Downloadlimit += ulong.Parse(UserLimition.upload_limit);
                    if (UserLimition.download_limit == null) UserLimition.download_limit = "0";
                    if (UserLimition.download_limit != "0") Downloadlimit += ulong.Parse(UserLimition.download_limit);
                    if (UserLimition.download_limit != "" && item.download_used != "")
                        ViewBag.download_remain = (Downloadlimit - ulong.Parse(item.download_used)).ToString();
                    if (UserLimition.upload_limit != "" && item.upload_used != "")
                        ViewBag.upload_remain = (ulong.Parse(UserLimition.upload_limit) - ulong.Parse(item.upload_used)).ToString();
                    if (UserLimition.uptime_limit != null)
                        ViewBag.uptime_limit = UserLimition.uptime_limit.Replace("d", " روز ").Replace("w", " هفته ").Replace("h", " ساعت ").Replace("m", " دقیقه ").Replace("s", " ثانیه ");
                    if (UserProfile.validity != null)
                        ViewBag.validity = UserProfile.validity.Replace("d", " روز ").Replace("w", " هفته ").Replace("h", " ساعت ").Replace("m", " دقیقه ").Replace("s", " ثانیه ");
                    if (item.shared_users != null)
                        item.shared_users = item.shared_users.Replace("unlimited", " بدون محدودیت ");
                    ViewBag.price = UserProfile.price;
                    if (UserProfileLimition.from_time != null)
                        ViewBag.from_time = UserProfileLimition.from_time.Replace("d", " روز ").Replace("s", " ثانیه ").Replace("m", " دقیقه ").Replace("h", " ساعت ");
                    if (UserProfileLimition.till_time != null)
                        ViewBag.till_time = UserProfileLimition.till_time.Replace("d", " روز ").Replace("s", " ثانیه ").Replace("m", " دقیقه ").Replace("h", " ساعت ");
                    if (UserProfileLimition.weekdays != null)
                        ViewBag.weekdays = UserProfileLimition.weekdays.Replace("friday", " جمعه ").Replace("thursday", " پنجشنبه ").Replace("wednesday", " چهارشنبه ").Replace("tuesday", " سه شنبه ").Replace("monday", " دوشنبه ").Replace("sunday", " یکشنبه ").Replace("saturday", "شنبه ");
                    if (item.uptime_used != null)
                        item.uptime_used = item.uptime_used.Replace("d", " روز ").Replace("w", " هفته ").Replace("h", " ساعت ").Replace("m", " دقیقه ").Replace("s", " ثانیه ").Replace("never", " بدون اتصال ");
                    if (item.last_seen != null)
                        if (item.last_seen == "never")
                        {
                            item.last_seen = item.last_seen.Replace("never", " بدون اتصال ");
                            item.last_seenT = item.last_seen.Replace("never", " بدون اتصال ");
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
                        item.disabled = item.disabled.Replace("false", "فعال").Replace("true", "غیره فعال");
                    if (item.download_used == "")
                        item.download_used = "0";
                    if (ViewBag.download_remain == null || ViewBag.download_remain == "")
                        ViewBag.download_remain = "0";
                    return View(item);
                }
            return View();
        }
        
        public virtual ActionResult UserCreate()
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            //            ViewBag.Customers = _mikrotikServices.GetAllCustomers(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult UserCreate(Netotik.ViewModels.Identity.UserClient.UserRegisterModel model, ActionType actionType)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            model.customer = UserLogined.UserCompany.Userman_Customer;
            if (!ModelState.IsValid)
            {
                //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                return RedirectToAction(MVC.Company.UserManager.UserCreate());
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
                    comment = model.comment,
                    customer = model.customer,
                    location = model.location,
                    profile = Request.Form["profile"].ToString()
                };
                if (_mikrotikServices.Usermanager_IsUserExist(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, Usermanuser.username))
                {
                    //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                }
                else
                {
                    _mikrotikServices.Usermanager_UserCreate(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, Usermanuser);
                }
                return RedirectToAction(MVC.Company.UserManager.UserList());
            }
        }
        
        public virtual ActionResult PackageList()
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            if (_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                var Profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
                var RouterProfiles = new List<Netotik.ViewModels.Identity.UserClient.ProfileModel>();
                foreach(var item in Profiles)
                {
                    item.starts_at = item.starts_at.Replace("logon", " زمان اولین اتصال ").Replace("now", " زمان انتساب پکیج ");
                    item.validity = item.validity.Replace("d", " روز ").Replace("w", " هفته ").Replace("h", " ساعت ").Replace("m", " دقیقه ").Replace("s", " ثانیه "); ;
                    RouterProfiles.Add(item);
                }
                ViewBag.userlist = RouterProfiles;
            }
            return View();
        }
        
        public virtual ActionResult Report()
        {
            return View();
        }
        
        
        public virtual ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult UserEdit(string id)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            var model = _mikrotikServices.Usermanager_GetAllUsers(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            foreach (var item in model)
                if (item.id == id)
                {
                    var editModel = new Netotik.ViewModels.Identity.UserClient.UserEditModel
                    {
                        id = item.id,
                        first_name = item.first_name,
                        comment = item.comment,
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
            //            ViewBag.Customers = _mikrotikServices.GetAllCustomers(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult UserEdit_Save(Netotik.ViewModels.Identity.UserClient.UserEditModel model, ActionType actionType)
        {
            
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "UserManager نصب نمی باشد.لطفا پس از نصب دوباره امتحان کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            if (!ModelState.IsValid)
            {
                //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                return View();
            }
            else
            {
                model.customer = UserLogined.UserCompany.Userman_Customer;
                model.profile = Request.Form["profile"].ToString();
                var model2 = _mikrotikServices.Usermanager_GetAllUsers(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
                foreach (var item in model2)
                    if (item.id == model.id)
                    {
                        if (model.profile == item.actual_profile)
                            model.profile = "";
                    }
                _mikrotikServices.Usermanager_UserEdit(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, model);

                return RedirectToAction(MVC.Company.UserManager.UserList());
            }
        }
        #endregion

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
            
            PopulatePermissions(_applicationUserManager.FindClientPermissions(UserLogined.Id).ToArray());
            return View();
        }
       
        [HttpPost]
        public virtual ActionResult ClientArea(ViewModels.Identity.UserCompany.ProfileModel model)
        {
            PopulatePermissions(model.ClientPermissionNames);
            this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            _applicationUserManager.UpdateUserCompanyProfile(model);
            return RedirectToAction(MVC.Company.UserManager.ActionNames.ClientArea);
        }




    }
}