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
using Netotik.ViewModels.Identity.UserClient;

namespace Netotik.Web.Areas.Company.Controllers
{
    [Mvc5Authorize(Roles = "Company")]
    [BreadCrumb(Title = "Reports", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ChartsController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUserCompanyLogClientService _usercompanylogclientservice;
        private readonly IUnitOfWork _uow;

        public ChartsController(
            IMikrotikServices mikrotikServices,
            IPictureService pictureservice,
            IApplicationUserManager applicationUserManager,
            IUserCompanyLogClientService usercompanylogclientservice,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _pictureService = pictureservice;
            _applicationUserManager = applicationUserManager;
            _usercompanylogclientservice = usercompanylogclientservice;
            _uow = uow;
        }
        #endregion

        public virtual ActionResult MaxTimeUsage()
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            return View();
        }
        public virtual ActionResult MaxTrafficUsage()
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            var UsermanagerSessions = _mikrotikServices.Usermanager_GetAllUsersSessions(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            var UsermanagerUsers = _mikrotikServices.Usermanager_GetAllUsers(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            Dictionary<string, ulong> UserListDownloadBySessions = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListDownloadBySessions7 = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListDownloadBySessions30 = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListUploadBySessions = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListUploadBySessions7 = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListUploadBySessions30 = new Dictionary<string, ulong>();

            Dictionary<string, ulong> UserListDownloadByUsers = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListUploadByUsers = new Dictionary<string, ulong>();

            foreach (var item in UsermanagerSessions)
            {
                item.from_time = Infrastructure.EnglishConvertDate.ConvertToFa(item.from_time.Split(' ')[0], "d") + " " + item.from_time.Split(' ')[1];

                if (UserListDownloadBySessions.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                    UserListDownloadBySessions[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576);
                else
                    UserListDownloadBySessions.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576));
                //---------------
                if (UserListUploadBySessions.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                    UserListUploadBySessions[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576);
                else
                    UserListUploadBySessions.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576));

                if (PersianDate.ConvertDate.ToEn(item.from_time.Split(' ')[0]).AddDays(7) >= DateTime.Now)
                {
                    if (UserListDownloadBySessions7.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                        UserListDownloadBySessions7[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576);
                    else
                        UserListDownloadBySessions7.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576));
                    //---------------
                    if (UserListUploadBySessions7.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                        UserListUploadBySessions7[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576);
                    else
                        UserListUploadBySessions7.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576));
                }
                if (PersianDate.ConvertDate.ToEn(item.from_time.Split(' ')[0]).AddDays(30) >= DateTime.Now)
                {
                    if (UserListDownloadBySessions30.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                        UserListDownloadBySessions30[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576);
                    else
                        UserListDownloadBySessions30.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576));
                    //---------------
                    if (UserListUploadBySessions30.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                        UserListUploadBySessions30[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576);
                    else
                        UserListUploadBySessions30.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576));
                }
            }

            foreach (var item in UsermanagerUsers)
            {
                UserListDownloadByUsers.Add(item.username == null | item.username == "" ? "-UserError-" : item.username, item.download_used == null || item.download_used == "" ? 0 : ulong.Parse(item.download_used) / 1048576);
                UserListUploadByUsers.Add(item.username == null | item.username == "" ? "-UserError-" : item.username, item.upload_used == null || item.upload_used == "" ? 0 : ulong.Parse(item.upload_used) / 1048576);
            }


            ViewBag.UserListDownloadBySessions = UserListDownloadBySessions.OrderByDescending(pair => pair.Value).Take(10);
            ViewBag.UserListDownloadBySessions7 = UserListDownloadBySessions7.OrderByDescending(pair => pair.Value).Take(10);
            ViewBag.UserListDownloadBySessions30 = UserListDownloadBySessions30.OrderByDescending(pair => pair.Value).Take(10);
            ViewBag.UserListUploadBySessions = UserListUploadBySessions.OrderByDescending(pair => pair.Value).Take(10);
            ViewBag.UserListUploadBySessions7 = UserListUploadBySessions7.OrderByDescending(pair => pair.Value).Take(10);
            ViewBag.UserListUploadBySessions30 = UserListUploadBySessions30.OrderByDescending(pair => pair.Value).Take(10);
            ViewBag.UserListDownloadByUsers = UserListDownloadByUsers.OrderByDescending(pair => pair.Value).Take(10);
            ViewBag.UserListUploadByUsers = UserListUploadByUsers.OrderByDescending(pair => pair.Value).Take(10);
            return View();
        }
        public virtual ActionResult MinTrafficUsage()
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            var UsermanagerSessions = _mikrotikServices.Usermanager_GetAllUsersSessions(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            var UsermanagerUsers = _mikrotikServices.Usermanager_GetAllUsers(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            Dictionary<string, ulong> UserListDownloadBySessions = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListDownloadBySessions7 = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListDownloadBySessions30 = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListUploadBySessions = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListUploadBySessions7 = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListUploadBySessions30 = new Dictionary<string, ulong>();

            Dictionary<string, ulong> UserListDownloadByUsers = new Dictionary<string, ulong>();
            Dictionary<string, ulong> UserListUploadByUsers = new Dictionary<string, ulong>();

            foreach (var item in UsermanagerSessions)
            {
                item.from_time = Infrastructure.EnglishConvertDate.ConvertToFa(item.from_time.Split(' ')[0], "d") + " " + item.from_time.Split(' ')[1];

                if (UserListDownloadBySessions.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                    UserListDownloadBySessions[item.user == null | item.user == "" ? "-UserError-" : item.user == null | item.user == null | item.user == "" ? "-UserError-" : item.user == "" ? "-UserError-" : item.user == null | item.user == "" ? "-UserError-" : item.user] += item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576);
                else
                    UserListDownloadBySessions.Add(item.user == null | item.user == "" ? "-UserError-" : item.user == null | item.user == null | item.user == "" ? "-UserError-" : item.user == "" ? "-UserError-" : item.user == null | item.user == "" ? "-UserError-" : item.user, item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576));
                //---------------
                if (UserListUploadBySessions.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                    UserListUploadBySessions[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576);
                else
                    UserListUploadBySessions.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576));

                if (PersianDate.ConvertDate.ToEn(item.from_time.Split(' ')[0]).AddDays(7) >= DateTime.Now)
                {
                    if (UserListDownloadBySessions7.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                        UserListDownloadBySessions7[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576);
                    else
                        UserListDownloadBySessions7.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576));
                    //---------------
                    if (UserListUploadBySessions7.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                        UserListUploadBySessions7[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576);
                    else
                        UserListUploadBySessions7.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576));
                }
                if (PersianDate.ConvertDate.ToEn(item.from_time.Split(' ')[0]).AddDays(30) >= DateTime.Now)
                {
                    if (UserListDownloadBySessions30.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                        UserListDownloadBySessions30[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576);
                    else
                        UserListDownloadBySessions30.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.download == null || item.download == "" ? 0 : (ulong.Parse(item.download) / 1048576));
                    //---------------
                    if (UserListUploadBySessions30.ContainsKey(item.user == null | item.user == "" ? "-UserError-" : item.user))
                        UserListUploadBySessions30[item.user == null | item.user == "" ? "-UserError-" : item.user] += item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576);
                    else
                        UserListUploadBySessions30.Add(item.user == null | item.user == "" ? "-UserError-" : item.user, item.upload == null || item.upload == "" ? 0 : (ulong.Parse(item.upload) / 1048576));
                }
            }

            foreach (var item in UsermanagerUsers)
            {
                UserListDownloadByUsers.Add(item.username == null | item.username == "" ? "-UserError-" : item.username, item.download_used == null || item.download_used == "" ? 0 : ulong.Parse(item.download_used) / 1048576);
                UserListUploadByUsers.Add(item.username == null | item.username == "" ? "-UserError-" : item.username, item.upload_used == null || item.upload_used == "" ? 0 : ulong.Parse(item.upload_used) / 1048576);
            }

            ViewBag.UserListDownloadBySessions = UserListDownloadBySessions.OrderBy(pair => pair.Value).Take(10);
            ViewBag.UserListDownloadBySessions7 = UserListDownloadBySessions7.OrderBy(pair => pair.Value).Take(10);
            ViewBag.UserListDownloadBySessions30 = UserListDownloadBySessions30.OrderBy(pair => pair.Value).Take(10);
            ViewBag.UserListUploadBySessions = UserListUploadBySessions.OrderBy(pair => pair.Value).Take(10);
            ViewBag.UserListUploadBySessions7 = UserListUploadBySessions7.OrderBy(pair => pair.Value).Take(10);
            ViewBag.UserListUploadBySessions30 = UserListUploadBySessions30.OrderBy(pair => pair.Value).Take(10);
            ViewBag.UserListDownloadByUsers = UserListDownloadByUsers.OrderBy(pair => pair.Value).Take(10);
            ViewBag.UserListUploadByUsers = UserListUploadByUsers.OrderBy(pair => pair.Value).Take(10);

            return View();
        }
        public virtual ActionResult UsermanagerUsage()
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            var UsermanSessions = _mikrotikServices.Usermanager_GetAllUsersSessions(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            var UsermanUsers = _mikrotikServices.Usermanager_GetAllUsers(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            ulong UsermanagerSessionsDownload = 0;
            ulong UsermanagerSessionsUpload = 0;
            ulong UsermanagerSessionsDownload30 = 0;
            ulong UsermanagerSessionsUpload30 = 0;
            ulong UsermanagerSessionsDownload7 = 0;
            ulong UsermanagerSessionsUpload7 = 0;
            foreach (var SessionItem in UsermanSessions)
            {
                SessionItem.from_time = Infrastructure.EnglishConvertDate.ConvertToFa(SessionItem.from_time.Split(' ')[0], "d") + " " + SessionItem.from_time.Split(' ')[1];
                UsermanagerSessionsDownload += SessionItem.download == null || SessionItem.download == "" ? 0 : ulong.Parse(SessionItem.download.ToString()) / 1048576;
                UsermanagerSessionsUpload += SessionItem.upload == null || SessionItem.upload == "" ? 0 : ulong.Parse(SessionItem.upload.ToString()) / 1048576;
                if (PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]).AddDays(30) >= DateTime.Now)
                {
                    UsermanagerSessionsDownload30 += SessionItem.download == null || SessionItem.download == "" ? 0 : ulong.Parse(SessionItem.download.ToString()) / 1048576;
                    UsermanagerSessionsUpload30 += SessionItem.upload == null || SessionItem.upload == "" ? 0 : ulong.Parse(SessionItem.upload.ToString()) / 1048576;
                }
                if (PersianDate.ConvertDate.ToEn(SessionItem.from_time.Split(' ')[0]).AddDays(7) >= DateTime.Now)
                {
                    UsermanagerSessionsDownload7 += SessionItem.download == null || SessionItem.download == "" ? 0 : ulong.Parse(SessionItem.download.ToString()) / 1048576;
                    UsermanagerSessionsUpload7 += SessionItem.upload == null || SessionItem.upload == "" ? 0 : ulong.Parse(SessionItem.upload.ToString()) / 1048576;
                }
            }
            ViewBag.UsermanagerSessionsDownload = UsermanagerSessionsDownload;
            ViewBag.UsermanagerSessionsDownload30 = UsermanagerSessionsDownload30;
            ViewBag.UsermanagerSessionsDownload7 = UsermanagerSessionsDownload7;
            ViewBag.UsermanagerSessionsUpload = UsermanagerSessionsUpload;
            ViewBag.UsermanagerSessionsUpload30 = UsermanagerSessionsUpload30;
            ViewBag.UsermanagerSessionsUpload7 = UsermanagerSessionsUpload7;
            //--------------------------------------------------------------------------------------------
            ulong UsermanagerUsersDownload = 0;
            ulong UsermanagerUsersUpload = 0;
            foreach (var UsersItem in UsermanUsers)
            {
                UsermanagerUsersDownload += UsersItem.download_used == null || UsersItem.download_used == "" ? 0 : ulong.Parse(UsersItem.download_used.ToString()) / 1048576;
                UsermanagerUsersUpload += UsersItem.upload_used == null || UsersItem.upload_used == "" ? 0 : ulong.Parse(UsersItem.upload_used.ToString()) / 1048576;
            }
            ViewBag.UsermanagerUsersDownload = UsermanagerUsersDownload;
            ViewBag.UsermanagerUsersUpload = UsermanagerUsersUpload;

            return View();
        }
        public virtual ActionResult Sessions()
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            var temp = _mikrotikServices.Usermanager_GetAllUsersSessions(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            var NewSessions = new List<Netotik.ViewModels.Identity.UserClient.UserSessionModel>();
            foreach (var item in temp)
            {
                item.download = item.download == null || item.download == "" ? "" : (ulong.Parse(item.download) / 1048576).ToString();
                item.upload = item.upload == null || item.upload == "" ? "" : (ulong.Parse(item.upload) / 1048576).ToString();
                item.uptime = item.uptime.Replace("d", Captions.Day).Replace("w", Captions.Week).Replace("h", Captions.Hour).Replace("m", Captions.Minute).Replace("s", Captions.Secend).Replace("never", Captions.NoConnection);
                item.user = item.user == null | item.user == "" ? "-UserError-" : item.user == null || item.user == null | item.user == "" ? "-UserError-" : item.user == "" ? "-UserError-" : item.user == null | item.user == "" ? "-UserError-" : item.user;
                NewSessions.Add(item);
            }
            ViewBag.Model = NewSessions;
            return View();
        }
        public virtual ActionResult Logs()
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
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError(Captions.Error, Captions.UsermanagerClientError);
                return RedirectToAction(MVC.Company.Home.ActionNames.Index);
            }
            //-------------------------------
            ViewBag.Model = _mikrotikServices.Usermanager_GetAllLogs(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }

        public virtual ActionResult WebSitesLogs()
        {

            //-------------------------------

            if (UserLogined.UserCompany.WebsitesLogs)
            {
                this.MessageError(Captions.Error, "شما مجوز لازم را ندارید");
                return RedirectToAction(MVC.Company.Home.ActionNames.Index, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            var Logs = _usercompanylogclientservice.GetList(UserLogined.Id);
            var Users = _mikrotikServices.Usermanager_GetAllUsersSessions(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            var UsersLogs = new List<UserWebsiteLogsWithSessionsModel>();
            foreach (var user in Users)
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
                x.SrcIp.Split(':')[0] == user.user_ip
                ).Select(x => new UserWebsiteLogsWithSessionsModel
                {
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
                    Protocol = x.Protocol,
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

            return View(UsersLogs);
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
    }
}