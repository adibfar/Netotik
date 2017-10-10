using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MvcPaging;
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
using Netotik.ViewModels.Identity.UserClient;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Microsoft.AspNet.Identity;
using Netotik.ViewModels.Identity.Security;

namespace Netotik.Web.Areas.Client.Controllers
{
    [ClientAuthorize()]
    [BreadCrumb(Title = "کاربر", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class HomeController : BaseController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _uow;
        private readonly ISmsService _smsService;

        public HomeController(
            IMikrotikServices mikrotikServices,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            ISmsService smsService,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _pictureService = pictureservice;
            _applicationUserManager = applicationUserManager;
            _smsService = smsService;
            _uow = uow;
        }
        #endregion

        #region Index
        public virtual ActionResult Index()
        {
            var loginedUser = Session["Client"] as User;

            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;


            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
            }
            var users = _mikrotikServices.Usermanager_GetUser(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, loginedUser.UserName);
            foreach (var item in users)
                if (item.id == loginedUser.UserName)
                {
                    var session = _mikrotikServices.Usermanager_UserSession(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, item.username);
                    var profile = _mikrotikServices.Usermanager_GetAllProfile(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
                    var Limition = _mikrotikServices.Usermanager_GetAllLimition(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
                    var profileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
                    var UserProfile = new Netotik.ViewModels.Identity.UserClient.ProfileModel();
                    var UserProfileLimition = new Netotik.ViewModels.Identity.UserClient.ProfileLimitionModel();
                    var UserLimition = new Netotik.ViewModels.Identity.UserClient.LimitionModel();
                    var UserSession = new List<Netotik.ViewModels.Identity.UserClient.UserSessionModel>();
                    Dictionary<DateTime, ulong> DownloadChartValues = new Dictionary<DateTime, ulong>();
                    Dictionary<DateTime, ulong> UploadChartValues = new Dictionary<DateTime, ulong>();
                    foreach (var SessionItem in session)
                    {
                        var from_time = SessionItem.from_time;
                        var till_time = SessionItem.till_time;
                        SessionItem.from_time = Infrastructure.EnglishConvertDate.ConvertToFa(SessionItem.from_time.Split(' ')[0], "d") + " " + SessionItem.from_time.Split(' ')[1];
                        SessionItem.till_time = Infrastructure.EnglishConvertDate.ConvertToFa(SessionItem.till_time.Split(' ')[0], "d") + " " + SessionItem.till_time.Split(' ')[1];
                        SessionItem.from_timeT = Infrastructure.EnglishConvertDate.ConvertToFa(from_time.Split(' ')[0], "D") + " " + from_time.Split(' ')[1];
                        SessionItem.till_timeT = Infrastructure.EnglishConvertDate.ConvertToFa(till_time.Split(' ')[0], "D") + " " + till_time.Split(' ')[1];
                        UserSession.Add(SessionItem);

                        if (PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]).AddDays(7) >= DateTime.Now)
                        {
                            if (DownloadChartValues.ContainsKey(PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0])))
                                DownloadChartValues[PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0])] += (ulong.Parse(SessionItem.download) / 1048576);
                            else
                                DownloadChartValues.Add(PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]), (ulong.Parse(SessionItem.download) / 1048576));
                            //---------------
                            if (UploadChartValues.ContainsKey(PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0])))
                                UploadChartValues[PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0])] += (ulong.Parse(SessionItem.upload) / 1048576);
                            else
                                UploadChartValues.Add(PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]), (ulong.Parse(SessionItem.upload) / 1048576));
                        }
                    }

                    ViewBag.DownloadChartValues = DownloadChartValues;
                    ViewBag.UploadChartValues = UploadChartValues;
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
                    ulong out_temp;
                    UserLimition.download_limit = UserLimition.download_limit == null || UserLimition.download_limit == "" || !ulong.TryParse(UserLimition.download_limit, out out_temp) ? "0" : UserLimition.download_limit;
                    UserLimition.upload_limit = UserLimition.upload_limit == null || UserLimition.upload_limit == "" || !ulong.TryParse(UserLimition.upload_limit, out out_temp) ? "0" : UserLimition.upload_limit;
                    UserLimition.transfer_limit = UserLimition.transfer_limit == null || UserLimition.transfer_limit == "" || !ulong.TryParse(UserLimition.transfer_limit, out out_temp) ? "0" : UserLimition.transfer_limit;
                    item.download_used = item.download_used == null || item.download_used == "" || !ulong.TryParse(item.download_used, out out_temp) ? "0" : item.download_used;
                    item.upload_used = item.upload_used == null || item.upload_used == "" || !ulong.TryParse(item.upload_used, out out_temp) ? "0" : item.upload_used;

                    ViewBag.TransferLimit = ulong.Parse(UserLimition.transfer_limit) + ulong.Parse(UserLimition.upload_limit) + ulong.Parse(UserLimition.download_limit);
                    UserLimition.rate_limit_rx = UserLimition.rate_limit_rx == null ? "0" : UserLimition.rate_limit_rx;
                    UserLimition.rate_limit_tx = UserLimition.rate_limit_tx == null ? "0" : UserLimition.rate_limit_tx;

                    ViewBag.TransferRemain = ulong.Parse(UserLimition.transfer_limit) == 0 ? 0 : ulong.Parse(UserLimition.transfer_limit) - (ulong.Parse(item.download_used) + ulong.Parse(item.upload_used));

                    ViewBag.TransferUsed = (ulong.Parse(item.download_used) + ulong.Parse(item.upload_used));

                    //--------------------------------------------------------------------
                    string uptime_used = item.uptime_used;
                    string validity = UserProfile.validity;
                    string uptime_limit = UserLimition.uptime_limit;
                    ulong UpTimeSec = 0;
                    ulong ValidSec = 0;
                    ulong UpTimeLimSec = 0;
                    if (item.uptime_used != null)
                    {
                        UpTimeSec += uptime_used.Contains('w') ? (ulong.Parse(uptime_used.Split('w')[0]) * 604800) : 0;
                        if (uptime_used.Contains('w')) uptime_used = uptime_used.Split('w')[1];
                        UpTimeSec += uptime_used.Contains('d') ? (ulong.Parse(uptime_used.Split('d')[0]) * 86400) : 0;
                        if (uptime_used.Contains('d')) uptime_used = uptime_used.Split('d')[1];
                        UpTimeSec += uptime_used.Contains('h') ? (ulong.Parse(uptime_used.Split('h')[0]) * 3600) : 0;
                        if (uptime_used.Contains('h')) uptime_used = uptime_used.Split('h')[1];
                        UpTimeSec += uptime_used.Contains('m') ? (ulong.Parse(uptime_used.Split('m')[0]) * 60) : 0;
                        if (uptime_used.Contains('m')) uptime_used = uptime_used.Split('m')[1];
                        UpTimeSec += uptime_used.Contains('s') ? ulong.Parse(uptime_used.Split('s')[0]) : 0;
                    }

                    //****
                    if (UserProfile.validity != null)
                    {
                        ValidSec += validity.Contains('w') ? (ulong.Parse(validity.Split('w')[0]) * 604800) : 0;
                        if (validity.Contains('w')) validity = validity.Split('w')[1];
                        ValidSec += validity.Contains('d') ? (ulong.Parse(validity.Split('d')[0]) * 86400) : 0;
                        if (validity.Contains('d')) validity = validity.Split('d')[1];
                        ValidSec += validity.Contains('h') ? (ulong.Parse(validity.Split('h')[0]) * 3600) : 0;
                        if (validity.Contains('h')) validity = validity.Split('h')[1];
                        ValidSec += validity.Contains('m') ? (ulong.Parse(validity.Split('m')[0]) * 60) : 0;
                        if (validity.Contains('m')) validity = validity.Split('m')[1];
                        ValidSec += validity.Contains('s') ? ulong.Parse(validity.Split('s')[0]) : 0;
                    }

                    //****
                    if (UserLimition.uptime_limit != null)
                    {
                        UpTimeLimSec += uptime_limit.Contains('w') ? (ulong.Parse(uptime_limit.Split('w')[0]) * 604800) : 0;
                        if (uptime_limit.Contains('w')) uptime_limit = uptime_limit.Split('w')[1];
                        UpTimeLimSec += uptime_limit.Contains('d') ? (ulong.Parse(uptime_limit.Split('d')[0]) * 8644) : 0;
                        if (uptime_limit.Contains('d')) uptime_limit = uptime_limit.Split('d')[1];
                        UpTimeLimSec += uptime_limit.Contains('h') ? (ulong.Parse(uptime_limit.Split('h')[0]) * 3600) : 0;
                        if (uptime_limit.Contains('h')) uptime_limit = uptime_limit.Split('h')[1];
                        UpTimeLimSec += uptime_limit.Contains('m') ? (ulong.Parse(uptime_limit.Split('m')[0]) * 60) : 0;
                        if (uptime_limit.Contains('m')) uptime_limit = uptime_limit.Split('m')[1];
                        UpTimeLimSec += uptime_limit.Contains('s') ? ulong.Parse(uptime_limit.Split('s')[0]) : 0;
                    }
                    ulong days = 0; ulong weeks = 0; ulong hours = 0; ulong mins = 0; ulong secs = 0;
                    //*************

                    //ulong resultt = (ValidSec > UpTimeSec)? ValidSec - UpTimeSec :0;
                    //weeks = resultt / 604800;
                    //days = (resultt % 604800) / 86400;
                    //hours = ((resultt % 604800) % 86400) / 3600;
                    //mins = (((resultt % 604800) % 86400) % 3600) / 60;
                    //secs = ((((resultt % 604800) % 86400) % 3600) % 60);
                    ////****
                    //ViewBag.RemianTime += weeks != 0 ? weeks + "هفته " : "";
                    //ViewBag.RemianTime += days != 0 ? days + "روز " : "";
                    //ViewBag.RemianTime += hours != 0 ? hours + "ساعت " : "";
                    //ViewBag.RemianTime += mins != 0 ? mins + "دقیقه " : "";

                    ulong resultt = 0;
                    resultt = (UpTimeLimSec > UpTimeSec) ? UpTimeLimSec - UpTimeSec : 0;
                    weeks = resultt / 604800;
                    days = (resultt % 604800) / 86400;
                    hours = ((resultt % 604800) % 86400) / 3600;
                    mins = (((resultt % 604800) % 86400) % 3600) / 60;
                    secs = ((((resultt % 604800) % 86400) % 3600) % 60);
                    //****
                    ViewBag.RemianUpTime += weeks != 0 ? weeks + Captions.Week : "";
                    ViewBag.RemianUpTime += days != 0 ? days + Captions.Day : "";
                    ViewBag.RemianUpTime += hours != 0 ? hours + Captions.Hour : "";
                    ViewBag.RemianUpTime += mins != 0 ? mins + Captions.Minute : "";
                    if (ViewBag.RemianUpTime == null || ViewBag.RemianUpTime == "")
                        ViewBag.RemianUpTime = Captions.Inaccessible;
                    //--------------------------------------------------------------------
                    var time = _mikrotikServices.Usermanager_Payment(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, item.username);
                    var LastTime = time.LastOrDefault();
                    ViewBag.StartTime = (LastTime.trans_end == null || LastTime.trans_end == "") ? Captions.Inaccessible : Infrastructure.EnglishConvertDate.ConvertToFa(LastTime.trans_end.Split(' ')[0], "") + " " + LastTime.trans_end.Split(' ')[1];
                    days = ValidSec / 86400;
                    ViewBag.RemianTime = (LastTime.trans_end == null || LastTime.trans_end == "") ? Captions.Inaccessible : PersianDate.ConvertDate.ToFa(PersianDate.ConvertDate.ToEn(Infrastructure.EnglishConvertDate.ConvertToFa(LastTime.trans_end.Split(' ')[0], "")).AddDays(Int32.Parse(days.ToString())), "d").ToString();
                    if (ValidSec == 0) ViewBag.RemianTime = Captions.Unlimited;
                    if (UserProfile.starts_at == "logon" && (LastTime.trans_end == null || LastTime.trans_end == "")) ViewBag.StartTime += " " + Captions.Approximate;
                    //-------------***-----------

                    if (item.uptime_used != null)
                        item.uptime_used = item.uptime_used.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                    if (UserLimition.uptime_limit != null)
                        ViewBag.uptime_limit = UserLimition.uptime_limit.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend);
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
                        item.disabled = item.disabled.Replace("false", Captions.Enable).Replace("true", Captions.Disable);

                    return View(item);
                }
            return View();
        }
        #endregion

        public virtual ActionResult ChangePassword()
        {

            var loginedUser = Session["Client"] as User;

            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;
            if (!Permissions.Any(x => x == AssignablePermissionToClient.ChangePassword))
            {
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }

            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            return View();
        }
        [HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordModel model)
        {
            var loginedUser = Session["Client"] as User;
            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;
            if (!Permissions.Any(x => x == AssignablePermissionToClient.ChangePassword))
            {
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Client.Home.ActionNames.ChangePassword);
            }
            //var temp = await _applicationUserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.Password);
            //if (temp.Succeeded)
            //   this.MessageInformation(Captions.MissionSuccess, Captions.UpdateSuccess);
            //else
            //    this.MessageError(Captions.MissionFail, Captions.UpdateError);
            var User = _mikrotikServices.Usermanager_GetUser(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, loginedUser.UserName);
            if (_mikrotikServices.Usermanager_UserChangePassword(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, model, loginedUser.UserName) && loginedUser.UserCompany.SmsCharge > 0 && loginedUser.UserCompany.SmsActive && loginedUser.UserCompany.SmsUserhangeUserPassword && User.FirstOrDefault().phone != null && User.FirstOrDefault().phone != null)
                _smsService.SendSms(User.FirstOrDefault().phone, string.Format(Captions.SmsUserPasswordChange,User.FirstOrDefault().username,model.Password),loginedUser.UserCompany.Id);
            _uow.SaveAllChanges();
            return View();
        }
        public virtual ActionResult Edit()
        {
            var loginedUser = Session["Client"] as User;

            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;
            if (!Permissions.Any(x => x == AssignablePermissionToClient.Edit))
            {
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            var model = _mikrotikServices.Usermanager_GetUser(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, loginedUser.UserName);
            foreach (var item in model)
                if (item.id == loginedUser.UserName)
                {
                    var editModel = new Netotik.ViewModels.Identity.UserClient.UserEditModel
                    {
                        id = item.id,
                        first_name = item.first_name,
                        comment = item.comment,
                        email = item.email,
                        last_name = item.last_name,
                        location = item.location,
                        phone = item.phone,
                        profile = item.actual_profile,
                        shared_users = item.shared_users,
                        username = item.username
                    };
                    return View(editModel);
                }
            //            ViewBag.Customers = _mikrotikServices.GetAllCustomers(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult UserEdit_Save(Netotik.ViewModels.Identity.UserClient.UserEditModel model, ActionType actionType)
        {
            var loginedUser = Session["Client"] as User;
            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;
            if (!Permissions.Any(x => x == AssignablePermissionToClient.Edit))
            {
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            if (!ModelState.IsValid)
            {
                //SetResultMessage(false, MessageColor.Danger, Captions.InvalidDataError, Captions.MissionFail);
                return View();
            }
            else
            {
                var UsermanagerUser = _mikrotikServices.Usermanager_GetUser(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, loginedUser.UserName);
                model.customer = loginedUser.UserCompany.Userman_Customer;
                model.profile = "";
                model.password = UsermanagerUser.FirstOrDefault().password;
                _mikrotikServices.Usermanager_UserEdit(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, model);

                return RedirectToAction(MVC.Client.Home.Index());
            }
        }
        public virtual ActionResult Details()
        {
            var loginedUser = Session["Client"] as User;
            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;
            if (!Permissions.Any(x => x == AssignablePermissionToClient.Details))
            {
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            var users = _mikrotikServices.Usermanager_GetUser(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, loginedUser.UserName);
            foreach (var item in users)
                if (item.id == loginedUser.UserName)
                {
                    var session = _mikrotikServices.Usermanager_UserSession(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, item.username);
                    var profile = _mikrotikServices.Usermanager_GetAllProfile(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
                    var Limition = _mikrotikServices.Usermanager_GetAllLimition(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
                    var profileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
                    var UserProfile = new Netotik.ViewModels.Identity.UserClient.ProfileModel();
                    var UserProfileLimition = new Netotik.ViewModels.Identity.UserClient.ProfileLimitionModel();
                    var UserLimition = new Netotik.ViewModels.Identity.UserClient.LimitionModel();
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
                    ulong out_temp;
                    if (UserLimition.transfer_limit == null || !ulong.TryParse(UserLimition.transfer_limit, out out_temp)) UserLimition.transfer_limit = "0";
                    if (UserLimition.transfer_limit != "0") Downloadlimit += ulong.Parse(UserLimition.transfer_limit);
                    if (UserLimition.upload_limit == null) UserLimition.upload_limit = "0";
                    if (UserLimition.upload_limit != "0") Downloadlimit += ulong.Parse(UserLimition.upload_limit);
                    if (UserLimition.download_limit == null) UserLimition.download_limit = "0";
                    if (UserLimition.download_limit != "0") Downloadlimit += ulong.Parse(UserLimition.download_limit);
                    if (UserLimition.download_limit != "" && item.download_used != "" && Downloadlimit > 0)
                        ViewBag.download_remain = (Downloadlimit - ulong.Parse(item.download_used)).ToString();
                    else
                    {
                        ViewBag.download_remain = 0;
                    }
                    if (UserLimition.upload_limit != "" && item.upload_used != "")
                        ViewBag.upload_remain = (ulong.Parse(UserLimition.upload_limit) - ulong.Parse(item.upload_used)).ToString();
                    if (UserLimition.uptime_limit != null)
                        ViewBag.uptime_limit = UserLimition.uptime_limit.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend);
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
                        item.disabled = item.disabled.Replace("false", Captions.Enable).Replace("true", Captions.Disable);
                    if (item.download_used == "")
                        item.download_used = "0";
                    if (ViewBag.download_remain == null || ViewBag.download_remain.ToString() == "")
                        ViewBag.download_remain = "0";
                    return View(item);
                }
            return View();
        }


        [HttpPost]
        public virtual ActionResult BuyPackage(string pname)
        {
            var loginedUser = Session["Client"] as User;
            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;
            if (!Permissions.Any(x => x == AssignablePermissionToClient.BuyPackage))
            {
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
            ViewBag.limitions = _mikrotikServices.Usermanager_GetAllLimition(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
            var model = new UserEditModel();
            var UsermanagerUser = _mikrotikServices.Usermanager_GetUser(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, loginedUser.UserName);
            model.customer = loginedUser.UserCompany.Userman_Customer;
            model.id = UsermanagerUser.FirstOrDefault().id;
            model.username = UsermanagerUser.FirstOrDefault().username;
            model.shared_users = UsermanagerUser.FirstOrDefault().shared_users;
            model.password = UsermanagerUser.FirstOrDefault().password;
            model.location = UsermanagerUser.FirstOrDefault().location;
            model.comment = UsermanagerUser.FirstOrDefault().comment;
            model.email = UsermanagerUser.FirstOrDefault().email;
            model.first_name = UsermanagerUser.FirstOrDefault().first_name;
            model.last_name = UsermanagerUser.FirstOrDefault().last_name;
            model.phone = UsermanagerUser.FirstOrDefault().phone;
            model.profile = pname;
            _mikrotikServices.Usermanager_UserEdit(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, model);
            if(model.phone != null && model.phone!= "")
            if (loginedUser.UserCompany.SmsCharge > 0 && loginedUser.UserCompany.SmsActive && loginedUser.UserCompany.SmsUserAfterChangePackage)
            {
                _smsService.SendSms(model.phone, string.Format(Captions.SmsUserBuyPackage,model.username), loginedUser.Id);
            }
            _uow.SaveAllChanges();
            return RedirectToAction(MVC.Client.Home.Index());
        }
        public virtual ActionResult BuyPackage()
        {
            var loginedUser = Session["Client"] as User;
            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;
            if (!Permissions.Any(x => x == AssignablePermissionToClient.BuyPackage))
            {
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            ViewBag.profiles = _mikrotikServices.Usermanager_GetAllProfile(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
            ViewBag.limitions = _mikrotikServices.Usermanager_GetAllLimition(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password);
            return View();
        }
        public virtual ActionResult Charts()
        {
            var loginedUser = Session["Client"] as User;
            var Permissions = _applicationUserManager.FindClientPermissions(loginedUser.Id);
            ViewBag.ClientPermissions = Permissions;
            if (!Permissions.Any(x => x == AssignablePermissionToClient.Charts))
            {
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.IPPORTClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UserPasswordClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            var usermanagerUser = _mikrotikServices.Usermanager_GetUser(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, loginedUser.UserName);
            var session = _mikrotikServices.Usermanager_UserSession(loginedUser.UserCompany.R_Host, loginedUser.UserCompany.R_Port, loginedUser.UserCompany.R_User, loginedUser.UserCompany.R_Password, usermanagerUser.FirstOrDefault().username);
            var UserSession = new List<Netotik.ViewModels.Identity.UserClient.UserSessionModel>();
            Dictionary<DateTime, ulong> DownloadMonth = new Dictionary<DateTime, ulong>();
            Dictionary<DateTime, ulong> UploadMonth = new Dictionary<DateTime, ulong>();
            //-------------Year
            Dictionary<string, ulong> DownloadYear = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UploadYear = new Dictionary<string, ulong>();
            //-------------30Session
            Dictionary<string, ulong> Download30Session = new Dictionary<string, ulong>();
            Dictionary<string, ulong> Upload30Session = new Dictionary<string, ulong>();

            int Counter = 0;
            foreach (var SessionItem in session)
            {
                Counter++;
                var from_time = SessionItem.from_time;
                var till_time = SessionItem.till_time;
                SessionItem.from_time = Infrastructure.EnglishConvertDate.ConvertToFa(SessionItem.from_time.Split(' ')[0], "d") + " " + SessionItem.from_time.Split(' ')[1];
                SessionItem.till_time = Infrastructure.EnglishConvertDate.ConvertToFa(SessionItem.till_time.Split(' ')[0], "d") + " " + SessionItem.till_time.Split(' ')[1];
                SessionItem.from_timeT = Infrastructure.EnglishConvertDate.ConvertToFa(from_time.Split(' ')[0], "D") + " " + from_time.Split(' ')[1];
                SessionItem.till_timeT = Infrastructure.EnglishConvertDate.ConvertToFa(till_time.Split(' ')[0], "D") + " " + till_time.Split(' ')[1];
                UserSession.Add(SessionItem);

                if (PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]).AddDays(30) >= DateTime.Now)
                {
                    if (DownloadMonth.ContainsKey(PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0])))
                        DownloadMonth[PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0])] += (ulong.Parse(SessionItem.download) / 1048576);
                    else
                        DownloadMonth.Add(PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]), (ulong.Parse(SessionItem.download) / 1048576));
                    //---------------
                    if (UploadMonth.ContainsKey(PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0])))
                        UploadMonth[PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0])] += (ulong.Parse(SessionItem.upload) / 1048576);
                    else
                        UploadMonth.Add(PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]), (ulong.Parse(SessionItem.upload) / 1048576));
                }

                //--------------Year
                if (PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]).AddDays(365) >= DateTime.Now)
                {
                    if (DownloadYear.ContainsKey(SessionItem.from_time.Split(' ')[0].Split('/')[1].ToString()))
                        DownloadYear[SessionItem.from_time.Split(' ')[0].Split('/')[1].ToString()] += (ulong.Parse(SessionItem.download) / 1048576);
                    else
                        DownloadYear.Add(SessionItem.from_time.Split(' ')[0].Split('/')[1].ToString(), (ulong.Parse(SessionItem.download) / 1048576));
                    //---------------
                    if (UploadYear.ContainsKey(SessionItem.from_time.Split(' ')[0].Split('/')[1].ToString()))
                        UploadYear[SessionItem.from_time.Split(' ')[0].Split('/')[1].ToString()] += (ulong.Parse(SessionItem.upload) / 1048576);
                    else
                        UploadYear.Add(SessionItem.from_time.Split(' ')[0].Split('/')[1].ToString(), (ulong.Parse(SessionItem.upload) / 1048576));
                }

                //-----------30

                if ((session.Count() - Counter) <= 30)
                {
                    if (Download30Session.ContainsKey(SessionItem.from_time.ToString()))
                        Download30Session[SessionItem.from_time.ToString()] += (ulong.Parse(SessionItem.download) / 1048576);
                    else
                        Download30Session.Add(SessionItem.from_time.ToString(), (ulong.Parse(SessionItem.download) / 1048576));
                    //---------------
                    if (Upload30Session.ContainsKey(SessionItem.from_time.ToString()))
                        Upload30Session[SessionItem.from_time.ToString()] += (ulong.Parse(SessionItem.upload) / 1048576);
                    else
                        Upload30Session.Add(SessionItem.from_time.ToString(), (ulong.Parse(SessionItem.upload) / 1048576));
                }
            }


            ViewBag.DownloadMonth = DownloadMonth;
            ViewBag.UploadMonth = UploadMonth;
            ViewBag.DownloadYear = DownloadYear;
            ViewBag.UploadYear = UploadYear;
            ViewBag.Download30Session = Download30Session;
            ViewBag.Upload30Session = Upload30Session;
            return View();
        }



        public virtual ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction(MVC.Home.Index());
        }
    }
}