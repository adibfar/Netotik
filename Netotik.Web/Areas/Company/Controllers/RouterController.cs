﻿using System;
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
using Netotik.Web.Extension;
using System.IO;
using DNTBreadCrumb;
using Netotik.Common.MikrotikAPI;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Netotik.ViewModels.Mikrotik;

namespace Netotik.Web.Areas.Company.Controllers
{
    [BreadCrumb(Title = "کاربر", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
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
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult Info()
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
            Router_Info.Free_hdd_space = (Int32.Parse(Router_Resource.FirstOrDefault().Free_hdd_space) / 1048576).ToString();
            Router_Info.Free_memory = (Int32.Parse(Router_Resource.FirstOrDefault().Free_memory) / 1048576).ToString();
            Router_Info.Platform = Router_Resource.FirstOrDefault().Platform;
            Router_Info.Total_hdd_space = (Int32.Parse(Router_Resource.FirstOrDefault().Total_hdd_space) / 1048576).ToString();
            Router_Info.Total_memory = (Int32.Parse(Router_Resource.FirstOrDefault().Total_memory) / 1048576).ToString();
            Router_Info.Uptime = Router_Resource.FirstOrDefault().Uptime;
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
            Router_Info.Router_date = Router_Clock.FirstOrDefault().Router_date;
            Router_Info.Router_time = Router_Clock.FirstOrDefault().Router_time;
            //------------------------------------------
            var Router_Routerboard = _mikrotikServices.Router_Routerboard(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            Router_Info.Serial_number = Router_Routerboard.FirstOrDefault().Serial_number;
            Router_Info.Router_model = Router_Routerboard.FirstOrDefault().Router_model;
            //------------------------------------------
            ViewBag.UsedMemory = Int32.Parse(Router_Info.Total_memory) - Int32.Parse(Router_Info.Free_memory);
            ViewBag.UsedMemoryPercent = ((Int32.Parse(Router_Info.Total_memory) - Int32.Parse(Router_Info.Free_memory)) * 100) / Int32.Parse(Router_Info.Total_memory);
            ViewBag.UsedHDD = Int32.Parse(Router_Info.Total_hdd_space) - Int32.Parse(Router_Info.Free_hdd_space);
            ViewBag.UsedHDDPercent = ((Int32.Parse(Router_Info.Total_hdd_space) - Int32.Parse(Router_Info.Free_hdd_space)) * 100) / Int32.Parse(Router_Info.Total_hdd_space);
            //------------------------------------------
            return View(Router_Info);
        }
        public virtual ActionResult Info_Update()
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
            //-------------------------------
            _mikrotikServices.Router_Info_Update(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageInformation("توجه", "بروزرسانی آغاز شد.لطفا به مدت 2الی5دقیقه منتظر بمانید تا روتر بروزرسانی ها را دریافت و نصب نماید.هنگام نصب نیاز به ریبوت روتر می باشد که به صورت خودکار انجام می شود.");
            return RedirectToAction(MVC.Company.Router.ActionNames.Info);
        }
        public virtual ActionResult Check_Update()
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
            //-------------------------------
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port);
            if (!mikrotik.Login(UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password)) mikrotik.Close();
            mikrotik.Send("/system/package/update/check-for-updates", true);
            return RedirectToAction(MVC.Company.Router.ActionNames.Info);
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult PPP()
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
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult Interfaces()
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
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult Wireless()
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
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult WirelessDetails(string id)
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
            //-------------------------------
            var Wireless = _mikrotikServices.GetWirelessDetails(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return View(Wireless);
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult InterfaceDisable(string id)
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
            //-------------------------------
            _mikrotikServices.Router_InterfaceDisable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Interfaces);
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult WirelessEnable(string id)
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
            //-------------------------------
            _mikrotikServices.Router_InterfaceEnable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Wireless);
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult WirelessDisable(string id)
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
            //-------------------------------
            _mikrotikServices.Router_InterfaceDisable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Wireless);
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult InterfaceEnable(string id)
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
            //-------------------------------
            _mikrotikServices.Router_InterfaceEnable(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return RedirectToAction(MVC.Company.Router.ActionNames.Interfaces);
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult InterfaceDetails(string id)
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
            //-------------------------------
            var Ethernet = _mikrotikServices.GetEthernetDetails(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, id);
            return View(Ethernet);
        }
        #endregion

        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult Hotspot_Temp()
        {
            return View();
        }
    }
}