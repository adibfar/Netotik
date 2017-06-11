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
using Netotik.ViewModels.Identity.UserCompany;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Microsoft.AspNet.Identity;
using Netotik.Services.Implement;
using Mvc.Mailer;
using Netotik.ViewModels.Identity.Account;

namespace Netotik.Web.Areas.Reseller.Controllers
{
    [BreadCrumb(Title = "لیست کاربران شرکت ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class CompanyController : BasePanelController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IPictureService _pictureService;
        private readonly IUserMailer _userMailer;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUnitOfWork _uow;

        public CompanyController(
            IApplicationUserManager applicationUserManager,
            IPictureService pictureservice,
            IUserMailer userMailer,
            IApplicationRoleManager applicationRoleManager,
            IMikrotikServices mikrotikServices,
            IUnitOfWork uow)
        {
            _pictureService = pictureservice;
            _userMailer = userMailer;
            _applicationRoleManager = applicationRoleManager;
            _applicationUserManager = applicationUserManager;
            _mikrotikServices = mikrotikServices;
            _uow = uow;
        }
        #endregion

        #region Index
        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult Index()
        {       
            var model = _applicationUserManager.GetListUserCompany(UserLogined.UserReseller.Id);
            return View(model);

        }
        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult CompanyLoginURL()
        {
            ViewBag.ResellerCode = UserLogined.UserReseller.ResellerCode;
            return View();
        }
        #endregion

        [Mvc5Authorize(Roles = "Reseller")]
        [BreadCrumb(Title = "کاربر جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            IUserMailer mailer = new UserMailer();
            return View();
        }


        [Mvc5Authorize(Roles = "Reseller")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(Register model)
        {
            if (_applicationUserManager.CheckResellerEmailExist(model.Email, null))
                ModelState.AddModelError("Email", "این ایمیل قبلا در سیستم ثبت شده است");

            if (_applicationUserManager.CheckUserNameExist(model.UserName, null))
                ModelState.AddModelError("UserName", "این نام کاربری قبلا در سیستم ثبت شده است");

            if (!model.Password.IsSafePasword())
                ModelState.AddModelError("Password", "این کلمه عبور به راحتی قابل تشخیص است");

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View(model);
            }
            model.UserResellerId = UserLogined.UserReseller.Id;
            if (model.cloud)
            {
                model.R_Host = _mikrotikServices.EnableAndGetCloud(model.R_Host, model.R_Port, model.R_User, model.R_Password);
            }
            var userId = await _applicationUserManager.AddCompany(model);

            await SendConfirmationEmail(model.Email, userId);


            var Success = "حساب کاربری شما با موفقیت ایجاد شد. برای فعال سازی " +
                              "حساب خود به ایمیل خود مراجعه کنید";

            ModelState.Clear();

            if (!_mikrotikServices.IP_Port_Check(model.R_Host,model.R_Port,model.R_User,model.R_Password)) {
                this.MessageWarning("اتصال به روتر انجام نشد.", "پس از فعال سازی اکانت وارد پنل شده و آدرس روتر و پورت روتر را بررسی کنید یا از طریق ویرایش نسبت به اصلاح اقدام کنید.");
                if (!_mikrotikServices.User_Pass_Check(model.R_Host, model.R_Port, model.R_User, model.R_Password))
                {
                    this.MessageWarning("نام کاربری یا رمز عبور روتر اشتباه می باشد.", "لطفا پس از فعال سازی اکانت وارد پنل شده و نام کاربری و رمز عبور صحیح را وارد کنید یا از طریق ویرایش نسبت به اصلاح اقدام کنید.");
                    if (!_mikrotikServices.Usermanager_IsInstall(model.R_Host, model.R_Port, model.R_User, model.R_Password))
                        this.MessageWarning("پکیج یوزرمنیجر میکروتیک نصب نمی باشد.", "پکیج یوزرمنیجر بر روی روتر شما نصب نمی باشد.لطفا مراحل نصب را طی نمایید.");
                }
            }
            this.MessageSuccess("فعال سازی!.", Success);
            this.MessageSuccess("ثبت موفق!.", "اطلاعات شما با موفقیت ثبت شد.");
            return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);

        }

        #region Detail

        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult Detail(int id)
        {
            //var user = _applicationUserManager.SingleOrDefault(id);
            //if(user.Reseller_Id != User.UserId)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
            //if (user == null)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);

            //var logmodel = new List<Netotik.ViewModels.Mikrotik.Log>();
            //var mikrotik = new MikrotikAPI();
            //try
            //{
            //    mikrotik.MK(user.R_Host, user.R_Port);

            //    if (!mikrotik.Login(user.R_User,user.R_Password))
            //    {
            //        logmodel.Add(new Model.Mikrotik.Log() { message = "User Or Password is Invalid to Connect!", time = DateTime.Now.ToString(), topics = "Server Error" });
            //        mikrotik.Close();
            //    }
            //    else
            //    {
            //        mikrotik.Send("/log/print", true);

            //        foreach (var item in mikrotik.Read())
            //        {
            //            if (item != "!done")
            //            {
            //                var cols = item.Split('=');
            //                var ColumnList = new Dictionary<string, string>();
            //                for (int i = 1; i < cols.Count(); i += 2)
            //                {
            //                    ColumnList.Add(cols[i], cols[i + 1]);
            //                }

            //                logmodel.Add(new Model.Mikrotik.Log()
            //                {
            //                    id = ColumnList.Any(x => x.Key == ".id") ? (ColumnList.FirstOrDefault(x => x.Key == ".id").Value) : "",
            //                    message = ColumnList.Any(x => x.Key == "message") ? (ColumnList.FirstOrDefault(x => x.Key == "message").Value) : "",
            //                    time = ColumnList.Any(x => x.Key == "time") ? (ColumnList.FirstOrDefault(x => x.Key == "time").Value) : "",
            //                    topics = ColumnList.Any(x => x.Key == "topics") ? (ColumnList.FirstOrDefault(x => x.Key == "topics").Value) : "",
            //                });
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    logmodel.Add(new Model.Mikrotik.Log() { message = "!IP or Port is Invalid to Connect OR Host is Shutdown!", time = DateTime.Now.ToString(),topics="Server Error" });
            //}
            //ViewBag.router_response = logmodel;
            //user.R_Password = "**********";
            ////ViewBag.logins = user.LoginHistories.OrderByDescending(x => x.RegisterDate).Take(10).ToList();
            //return View(user);

            return View();
        }

        #endregion

        #region Edit
        [Mvc5Authorize(Roles = "Reseller")]
        [BreadCrumb(Title = "ویرایش", Order = 1)]
        public virtual ActionResult Edit(int id)
        {
            //var model = _applicationUserManager.SingleOrDefault(id);
            //if(model.Reseller_Id != User.UserId)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
            //if (model == null)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);

            //var editModel = new UserEditModel
            //{
            //    Id=model.Id,
            //    Username = model.UserName,
            //    Email = model.Email,
            //    MobileNumber = model.MobileNumber,
            //    Name = model.Name,
            //    PostalCode = model.PostalCode,
            //    Address = model.Address,
            //    CompanyName = model.CompanyName,
            //    PersonCode = model.PersonalCode,
            //    R_Host = model.R_Host,
            //    R_User = model.R_User,
            //    R_Password = model.R_Password,
            //    R_Port = model.R_Port
            //};


            //return View(editModel);

            return View();

        }

        [Mvc5Authorize(Roles = "Reseller")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Edit(CompanyEditModel model)
        {
            //var companyuser = _applicationUserManager.SingleOrDefault(model.Id);
            //if(companyuser.Reseller_Id != User.UserId)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
            //if (companyuser == null)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);

            //if (ModelState.IsValid)
            //{
            //    companyuser.Address = model.Address;
            //    companyuser.UserName = model.Email;
            //    companyuser.PostalCode = model.PostalCode;
            //    companyuser.PersonalCode = model.PersonCode;
            //    companyuser.Name = model.Name;
            //    companyuser.CompanyName = model.CompanyName;
            //    companyuser.Email = model.Email;
            //    companyuser.MobileNumber = model.MobileNumber;
            //    companyuser.R_User = model.R_User;
            //    companyuser.R_Host = model.R_Host;
            //    companyuser.R_Password = model.R_Password;
            //    companyuser.R_Port = model.R_Port;

            //if (model.cloud)
            //{
            //    model.R_Host = _mikrotikServices.EnableAndGetCloud(model.R_Host, model.R_Port, model.R_User, model.R_Password);
            //}
            //    _applicationUserManager.Update(companyuser);

            //    if (result.Status)
            //    {
            //        try
            //        {
            //            _uow.SaveChanges();
            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            this.MessageError(Captions.MissionFail, Captions.AddError);
            //        }
            //        catch (Exception ex)
            //        {
            //            this.MessageError(Captions.MissionFail, Captions.AddError);
            //        }
            //    }
            //    SetResultMessage(result);
            //    if (!result.Status) return View();

            //    //if (actionType == ActionType.SaveContinue) return RedirectToAction(MVC.Reseller.Company.Edit(companyuser.Id));
            //    return RedirectToAction(MVC.Reseller.Company.Index());
            //}
            //else
            //{
            //    this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
            //    return View();
            //}

            return View();

        }


        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult ChangePassword(int id)
        {
            //var company = _applicationUserManager.SingleOrDefault(id);
            //if(company.Reseller_Id != User.UserId)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
            //return View(new ChangePasswordModel() { Id = company.Id });

            return View();
        }

        [Mvc5Authorize(Roles = "Reseller")]
        [HttpPost]
        public virtual ActionResult ChangePassword(ChangePasswordModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
            //}
            //else
            //{
            //    var result = await _applicationUserManager.ChangePasswordAsync(model.Id.Value, Encryption.EncryptingPassword(model.Password));
            //    if (result.Status)
            //    {
            //        try
            //        {
            //            await _uow.SaveChangesAsync();
            //        }
            //        catch (Exception ex)
            //        {
            //            this.MessageError(Captions.MissionFail, Captions.UpdateError);
            //        }
            //    }
            //    SetResultMessage(result);
            //}
            //return RedirectToAction(MVC.Reseller.Company.ActionNames.Index, MVC.Reseller.Company.Name, new { area = "Reseller" });

            return View();
        }

        [Mvc5Authorize(Roles = "Reseller")]
        public virtual async Task<ActionResult> Disable(int id = 0)
        {
            var model = _userManager.FindUserById(id);
            if (model == null || model.UserCompany.UserResellerId != UserLogined.Id)
            {
                this.MessageError("خطا", "هیچ مقداری ارسال نشده یا مجوز ویرایش را ندارید.");
                return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
            }
            model.IsBanned = true;
            await _uow.SaveAllChangesAsync();
            this.MessageSuccess("موفق! ", "کاربر غیره فعال شد.");
            return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
        }

        [Mvc5Authorize(Roles = "Reseller")]
        public virtual async Task<ActionResult> Enable(int id = 0)
        {
            var model = _userManager.FindUserById(id);
            if (model == null || model.UserCompany.UserResellerId != UserLogined.Id)
            {
                this.MessageError("خطا", "هیچ مقداری ارسال نشده یا مجوز ویرایش را ندارید.");
                return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
            }
            model.IsBanned = false;
            await _userManager.UpdateAsync(model);
            this.MessageSuccess("موفق! ", "کاربر فعال شد.");
            return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
        }
        #endregion

        public async Task SendConfirmationEmail(string email, long userId)
        {
            var code = await _applicationUserManager.GenerateEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Abs(Url.Action(MVC.Account.ActionNames.ConfirmEmail, MVC.Account.Name,
                new { userId, code, area = "" }, protocol: Request.Url.Scheme));

            _userMailer.ConfirmAccount(new EmailViewModel
            {
                Message = "با سلام کاربر گرامی.برای فعال سازی حساب خود لازم است بر روی لینک مقابل کلیک کنید",
                To = email,
                Url = callbackUrl,
                UrlText = "فعال سازی",
                Subject = "فعال سازی اکانت کاربری",
                ViewName = MVC.UserMailer.Views.ViewNames.ConfirmAccount
            }).Send();

        }

    }
}