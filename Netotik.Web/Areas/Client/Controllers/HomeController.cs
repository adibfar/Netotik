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

namespace Netotik.Web.Areas.Client.Controllers
{
    [Mvc5Authorize(Roles = "Client")]
    [BreadCrumb(Title = "کاربر", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class HomeController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _uow;

        public HomeController(
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

        #region Index

        public virtual ActionResult Index()
        {
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای عدم دسترسی یا IP ویا Port");
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای نام کاربری و رمز عبور");
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای یوزرمنیجر");
            }
            return View();
        }

        #endregion
        public virtual ActionResult ChangePassword()
        {
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای عدم دسترسی یا IP ویا Port");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای نام کاربری و رمز عبور");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای یوزرمنیجر");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            return View();
        }
        [HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای عدم دسترسی یا IP ویا Port");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای نام کاربری و رمز عبور");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای یوزرمنیجر");
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

            _mikrotikServices.Usermanager_UserChangePassword("", 0, "", "", model, "");
            return View();
        }

        public virtual ActionResult Edit()
        {

            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای عدم دسترسی یا IP ویا Port");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای نام کاربری و رمز عبور");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای یوزرمنیجر");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            var model = _mikrotikServices.Usermanager_GetUser(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, "");
            foreach (var item in model)
            //if (item.id == UserLogined.Id)
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
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای عدم دسترسی یا IP ویا Port");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای نام کاربری و رمز عبور");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای یوزرمنیجر");
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
                var UsermanagerUser = new List<UserModel>();//_mikrotikServices.Usermanager_GetUser((UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, UserLogined.Id);
                model.customer = UserLogined.UserCompany.Userman_Customer;
                model.profile = "";
                model.password = UsermanagerUser.FirstOrDefault().password;
                _mikrotikServices.Usermanager_UserEdit(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, model);

                return RedirectToAction(MVC.Client.Home.Index());
            }
        }

        public virtual ActionResult Details()
        {

            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای عدم دسترسی یا IP ویا Port");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای نام کاربری و رمز عبور");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            if (!_mikrotikServices.Usermanager_IsInstall(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "لطفا با مدیرشبکه تماس بگیرید.خطای یوزرمنیجر");
                return RedirectToAction(MVC.Client.Home.ActionNames.Index, MVC.Client.Home.Name, new { area = MVC.Client.Name });
            }
            //-------------------------------
            var users = new List<UserModel>();//_mikrotikServices.Usermanager_GetUser(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            foreach (var item in users)
                if (item.id == "0") // *********************************** check it
                {
                    var session = _mikrotikServices.Usermanager_UserSession(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, item.username);
                    var profile = _mikrotikServices.Usermanager_GetAllProfile(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
                    var Limition = _mikrotikServices.Usermanager_GetAllLimition(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
                    var profileLimition = _mikrotikServices.Usermanager_GetAllProfileLimition(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
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

    }
}