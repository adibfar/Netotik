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
using Netotik.Web.Extension;
using System.IO;
using DNTBreadCrumb;
using Netotik.Common.MikrotikAPI;
using Netotik.Services.Identity;
using Netotik.Common.Controller;

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
                this.MessageInformation("توجه", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageInformation("توجه", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
            }
            //-------------------------------
            var mikrotik = new MikrotikAPI();
            mikrotik.MK(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port);
            if (!mikrotik.Login(UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password)) mikrotik.Close();
            //-----------------------------------------------
            mikrotik.Send("/system/resource/print", true);
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    ViewBag.uptime = ColumnList.Any(x => x.Key == "uptime") ? (ColumnList.FirstOrDefault(x => x.Key == "uptime").Value.Replace("w", "هفته").Replace("d", "روز").Replace("h", ":").Replace("m", ":").Replace("s", "")) : "";
                    ViewBag.version = ColumnList.Any(x => x.Key == "version") ? (ColumnList.FirstOrDefault(x => x.Key == "version").Value) : "";
                    ViewBag.build_time = ColumnList.Any(x => x.Key == "build-time") ? (ColumnList.FirstOrDefault(x => x.Key == "build-time").Value) : "";
                    ViewBag.free_memory = ColumnList.Any(x => x.Key == "free-memory") ? (ColumnList.FirstOrDefault(x => x.Key == "free-memory").Value) : "";
                    ViewBag.total_memory = ColumnList.Any(x => x.Key == "total-memory") ? (ColumnList.FirstOrDefault(x => x.Key == "total-memory").Value) : "";
                    ViewBag.cpu = ColumnList.Any(x => x.Key == "cpu") ? (ColumnList.FirstOrDefault(x => x.Key == "cpu").Value) : "";
                    ViewBag.cpu_count = ColumnList.Any(x => x.Key == "cpu-count") ? (ColumnList.FirstOrDefault(x => x.Key == "cpu-count").Value) : "";
                    ViewBag.cpu_frequency = ColumnList.Any(x => x.Key == "cpu-frequency") ? (ColumnList.FirstOrDefault(x => x.Key == "cpu-frequency").Value) : "";
                    ViewBag.cpu_load = ColumnList.Any(x => x.Key == "cpu-load") ? (ColumnList.FirstOrDefault(x => x.Key == "cpu-load").Value) : "";
                    ViewBag.free_hdd_space = ColumnList.Any(x => x.Key == "free-hdd-space") ? (ColumnList.FirstOrDefault(x => x.Key == "free-hdd-space").Value) : "";
                    ViewBag.total_hdd_space = ColumnList.Any(x => x.Key == "total-hdd-space") ? (ColumnList.FirstOrDefault(x => x.Key == "total-hdd-space").Value) : "";
                    ViewBag.architecture_name = ColumnList.Any(x => x.Key == "architecture-name") ? (ColumnList.FirstOrDefault(x => x.Key == "architecture-name").Value) : "";
                    ViewBag.board_name = ColumnList.Any(x => x.Key == "board-name") ? (ColumnList.FirstOrDefault(x => x.Key == "board-name").Value) : "";
                    ViewBag.platform = ColumnList.Any(x => x.Key == "platform") ? (ColumnList.FirstOrDefault(x => x.Key == "platform").Value) : "";
                }
            }
            //--------------------------------------
            mikrotik.Send("/system/identity/print", true);
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    ViewBag.RouterName = ColumnList.Any(x => x.Key == "name") ? (ColumnList.FirstOrDefault(x => x.Key == "name").Value) : "";
                }
            }
            //--------------------------------------
            mikrotik.Send("/system/license/print", true);
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    ViewBag.software_id = ColumnList.Any(x => x.Key == "software-id") ? (ColumnList.FirstOrDefault(x => x.Key == "software-id").Value) : "";
                    ViewBag.nlevel = ColumnList.Any(x => x.Key == "nlevel") ? (ColumnList.FirstOrDefault(x => x.Key == "nlevel").Value) : "";
                }
            }
            //------------------------------------------
            mikrotik.Send("/system/package/update/check-for-updates", true);
            mikrotik.Send("/system/package/update/print", true);
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    ViewBag.channel = ColumnList.Any(x => x.Key == "channel") ? (ColumnList.FirstOrDefault(x => x.Key == "channel").Value) : "";
                    ViewBag.update_status = ColumnList.Any(x => x.Key == "status") ? (ColumnList.FirstOrDefault(x => x.Key == "status").Value) : "";
                    ViewBag.latest_version = ColumnList.Any(x => x.Key == "latest-version") ? (ColumnList.FirstOrDefault(x => x.Key == "latest-version").Value) : "";
                    ViewBag.installed_version = ColumnList.Any(x => x.Key == "installed-version") ? (ColumnList.FirstOrDefault(x => x.Key == "installed-version").Value) : "";
                }
            }
            //------------------------------------------
            mikrotik.Send("/system/clock/print", true);
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    ViewBag.router_time = ColumnList.Any(x => x.Key == "time") ? (ColumnList.FirstOrDefault(x => x.Key == "time").Value) : "";
                    ViewBag.router_date = ColumnList.Any(x => x.Key == "date") ? (ColumnList.FirstOrDefault(x => x.Key == "date").Value) : "";
                }
            }
            //------------------------------------------
            mikrotik.Send("/system/routerboard/print", true);
            foreach (var item in mikrotik.Read())
            {
                if (item != "!done")
                {
                    var cols = item.Split('=');
                    var ColumnList = new Dictionary<string, string>();
                    for (int i = 1; i < cols.Count(); i += 2)
                    {
                        ColumnList.Add(cols[i], cols[i + 1]);
                    }
                    ViewBag.router_model = ColumnList.Any(x => x.Key == "model") ? (ColumnList.FirstOrDefault(x => x.Key == "model").Value) : "";
                    ViewBag.serial_number = ColumnList.Any(x => x.Key == "serial-number") ? (ColumnList.FirstOrDefault(x => x.Key == "serial-number").Value) : "";
                }
            }
            //------------------------------------------
            return View();
        }
        public virtual ActionResult Info_Update()
        {
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageInformation("توجه", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageInformation("توجه", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
            }
            //-------------------------------
            _mikrotikServices.Router_Info_Update(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            this.MessageError("خطا", "بروزرسانی آغاز شد.لطفا به مدت 2الی5دقیقه منتظر بمانید تا روتر بروزرسانی ها را دریافت و نصب نماید.هنگام نصب نیاز به ریبوت روتر می باشد که به صورت خودکار انجام می شود.");
            return RedirectToAction(MVC.Company.Router.ActionNames.Info);
        }
        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult PPP()
        {
            //-------------------------------
            if (!_mikrotikServices.IP_Port_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageInformation("توجه", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageInformation("توجه", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
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
                this.MessageInformation("توجه", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageInformation("توجه", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
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
                this.MessageInformation("توجه", "آدرس IP یا Port دستگاه اشتباه وارد شده است یا دستگاه شما از طریق سرور قابل دسترس نمی باشد.لطفا آدرس IP ویا Port دستگاه را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
            }
            if (!_mikrotikServices.User_Pass_Check(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password))
            {
                this.MessageInformation("توجه", "نام کاربری یا رمز عبور صحیح وارد نشده است.لطفا نام کاربری یا رمز عبور را تصحیح کنید.");
                return RedirectToAction(MVC.Company.Home.ActionNames.MikrotikConf);
            }
            //-------------------------------
            ViewBag.model = _mikrotikServices.Interface(UserLogined.UserCompany.R_Host, UserLogined.UserCompany.R_Port, UserLogined.UserCompany.R_User, UserLogined.UserCompany.R_Password);
            return View();
        }
        #endregion

        [Mvc5Authorize(Roles = "Company")]
        public virtual ActionResult Hotspot_Temp()
        {
            return View();
        }
    }
}