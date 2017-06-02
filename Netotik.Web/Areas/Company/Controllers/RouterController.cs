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
    [BreadCrumb(Title = "روتر", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
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
            Router_Info.Free_hdd_space = (ulong.Parse(Router_Resource.FirstOrDefault().Free_hdd_space) / 1048576).ToString();
            Router_Info.Free_memory = (ulong.Parse(Router_Resource.FirstOrDefault().Free_memory) / 1048576).ToString();
            Router_Info.Platform = Router_Resource.FirstOrDefault().Platform;
            Router_Info.Total_hdd_space = (ulong.Parse(Router_Resource.FirstOrDefault().Total_hdd_space) / 1048576).ToString();
            Router_Info.Total_memory = (ulong.Parse(Router_Resource.FirstOrDefault().Total_memory) / 1048576).ToString();
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
            Router_Info.Router_date = Infrastructure.EnglishConvertDate.ConvertToFa(Router_Clock.FirstOrDefault().Router_date, "d");
            ViewBag.Server_Date = PersianDate.ConvertDate.ToFa(DateTime.Now,"d");
            //PersianDate.ConvertDate.ToFa();
            Infrastructure.EnglishConvertDate.ConvertToFa(Router_Clock.FirstOrDefault().Router_date, "d");
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
            if (Url.IsLocalUrl(ReturnURL))
            {
                return Redirect(ReturnURL);
            }
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
        public virtual ActionResult RouterSetting()
        {
            ViewBag.ReturnURL = "/Fa/Company/Router/RouterSetting";
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
            var filelist = _mikrotikServices.GetBackupRouterList(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            if (filelist == null)
            {
                filelist = new List<Router_FileModel>();
                filelist.Add(new Router_FileModel()
                {
                    Name = "هیچ موردی پیدا نشد",
                    CreateTime = "",
                    Size = "",
                    Type =""
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
                    Name = "هیچ موردی پیدا نشد",
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
                this.MessageError("خطا", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageError("خطا", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf, MVC.Company.Home.Name, new { area = MVC.Company.Name });
            }
            //-------------------------------
            _mikrotikServices.RebootRouter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageSuccess("ریبوت","روتر در حال راه اندازی مجدد می باشد.لطفا یک دقیقه منتظر بمانید.");
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult ResetRouter()
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
            if (Request.Form["reset"]!=null)
            if (Request.Form["reset"].ToString() == "yes")
            {
                bool nosetting = false;
                if (Request.Form["nodefualt"] !=null) { nosetting = true; }
                _mikrotikServices.ResetRouter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, true, nosetting);
                this.MessageSuccess("Reset", "روتر در حال ریست شدن است.ممکن است سامانه دیگر قادر به اتصال به روتر نباشد.با پشتیبان خود تماس بگیرید.");
            }
            else
            {
                this.MessageWarning("Reset", "متن مورد نظر را به درستی وارد کنید.");
            }
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult RestoreUsermanager()
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
            if (true)
            {
                _mikrotikServices.RestoreUsermanager(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password, Request.Form["RestoreUsermanager"].ToString());
                this.MessageSuccess("Restore Usermanager", "بک آپ مورد نظر درحال بازگردانی می باشد.");
            }
            else
            {
                this.MessageSuccess("Restore Usermanager", "متن مورد نظر را به درستی وارد کنید.");
            }
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        [HttpPost]
        public virtual ActionResult RestoreRouter()
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
                _mikrotikServices.RestoreRouter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password,Request.Form["RestoreRouter"].ToString());
                this.MessageSuccess("Restore", "بک آپ مورد نظر در حال بازگردانی می باشد.ممکن است سامانه دیگر قادر به اتصال به روتر نباشد.لطفا در صورت بروز هرگونه مسئله با ریسلر خود تماس بگیرید.");
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult ResetUsermanager()
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
            if(Request.Form["reset"] !=null)
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
                if (logs==false && false == users && false == packages && false == history && false== session &&db == false)
                {
                    this.MessageInformation("انتخاب مقادیر", "هیچ مقداری انتخاب نشده است.");
                }
                else
                {
                    _mikrotikServices.ResetUsermanager(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password,users,logs,session,history,packages,db );
                    this.MessageSuccess("Reset Usermanager", "یوزرمنیجر با تنظیمات انتخابی ریست شد.بک آپ کلی از یوزرمنیجر به صورت خودکار گرفته شد.");
                }
                }
                else
                {
                    this.MessageInformation("مقدار تایید", "مقدار تاییدیه به درستی پر نشده است..");
                }
            else
            {
                this.MessageInformation("مقدار تایید", "مقدار تاییدیه به درستی پر نشده است..");
            }
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult RemoveLogs()
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
            _mikrotikServices.RemoveLogs(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageSuccess("Logs", "لاگ ها پاک شدند.این عملیات غیره قابل برگشت می باشد.");
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult BackupUsermanager()
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
            _mikrotikServices.BackupUsermanager(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageSuccess("Backup Usermanager", "از دیتابیس یوزرمنیجر و دیتابیس لاگ یوزرمنیجر به درستی بک آپ گرفته شد.");
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
        public virtual ActionResult BackupRouter()
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
            _mikrotikServices.BackupRouter(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageSuccess("Backup", "بک آپ گیری با موفقیت انجام شد.");
            return RedirectToAction(MVC.Company.Router.ActionNames.RouterSetting);
        }
    }
}