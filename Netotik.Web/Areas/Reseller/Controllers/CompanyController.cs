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
using Netotik.ViewModels.Identity.UserCompany;
using Netotik.Services.Identity;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Reseller.Controllers
{
    [BreadCrumb(Title = "لیست کاربران شرکت ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class CompanyController : BaseController
    {
        #region ctor
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IPictureService _pictureService;
        private readonly IApplicationRoleManager _applicationRoleManager;
        private readonly IUnitOfWork _uow;

        public CompanyController(
            IApplicationUserManager applicationUserManager,
            IPictureService pictureservice,
            IApplicationRoleManager applicationRoleManager,
            IUnitOfWork uow)
        {
            _pictureService = pictureservice;
            _applicationRoleManager = applicationRoleManager;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
        }
        #endregion

        #region Index
        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {
            var pageList = new List<object>(); //_applicationUserManager.GetDataTableCompanyAccounts(Search, User.UserId).ToPagedList(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Reseller.Company.Views._Table, pageList);
            else
                return View(MVC.Reseller.Company.ActionNames.Index, pageList);
        }
        [Mvc5Authorize(Roles = "Reseller")]
        public virtual ActionResult CompanyLoginURL()
        {
            //var logined_user = _ResellerService.SingleOrDefault(User.UserId);
            //ViewBag.CompanyName = logined_user.CompanyName;
            //return View();
            return View();
        }
        #endregion

        #region Create

        [Mvc5Authorize(Roles = "Reseller")]
        [BreadCrumb(Title = "کاربر جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            return View();
        }


        [Mvc5Authorize(Roles = "Reseller")]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(CompanyAddModel model, ActionType actionType)
        {

            //if (!ModelState.IsValid)
            //{
            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
            //    return View();
            //}
            //else
            //{

            //    #region Initil User
            //    var now = DateTime.Now;
            //    var companyuser = new Domain.Entity.Company()
            //    {

            //        Userman_Customer = "admin",
            //        Reseller_Id = base.User.UserId,
            //        UserName = model.Username,
            //        Email = model.Email,
            //        MobileNumber = model.MobileNumber,
            //        Name = model.Name,
            //        PostalCode = model.PostalCode,
            //        Address = model.Address,
            //        Password = Encryption.EncryptingPassword(model.Password),
            //        CompanyName = model.CompanyName,
            //        PersonalCode = model.PersonCode,
            //        R_Host = model.R_Host,
            //        R_User = model.R_User,
            //        R_Password = model.R_Password,
            //        R_Port = model.R_Port,
            //        IsActive = true,
            //        CreateDate = now


            //    };
            //    #endregion

            //    #region SaveChanges
            //    _applicationUserManager.Add(companyuser);
            //    if (result.Status)
            //    {
            //        try
            //        {
            //            await _uow.SaveChangesAsync();
            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //        catch (Exception ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //    }
            //    #endregion

            //    SetResultMessage(result);
            //    if (!result.Status) return View();

            //    if (actionType == ActionType.SaveContinue) return RedirectToAction(MVC.Reseller.Company.Edit(companyuser.Id));
            //    return RedirectToAction(MVC.Reseller.Company.Index());
            //}

            return View();

        }


        #endregion

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

            //    _applicationUserManager.Update(companyuser);

            //    if (result.Status)
            //    {
            //        try
            //        {
            //            _uow.SaveChanges();
            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //        catch (Exception ex)
            //        {
            //            this.MessageError(Messages.MissionFail, Messages.AddError);
            //        }
            //    }
            //    SetResultMessage(result);
            //    if (!result.Status) return View();

            //    //if (actionType == ActionType.SaveContinue) return RedirectToAction(MVC.Reseller.Company.Edit(companyuser.Id));
            //    return RedirectToAction(MVC.Reseller.Company.Index());
            //}
            //else
            //{
            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
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
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            //if (!ModelState.IsValid)
            //{

            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
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
            //            this.MessageError(Messages.MissionFail, Messages.UpdateError);
            //        }
            //    }
            //    SetResultMessage(result);
            //}
            //return RedirectToAction(MVC.Reseller.Company.ActionNames.Index, MVC.Reseller.Company.Name, new { area = "Reseller" });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Disable(int id = 0)
        {
            //var model = _applicationUserManager.SingleOrDefault(id);
            //if (model == null)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
            //model.IsActive = false;
            //await _uow.SaveAllChangesAsync();

            //return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Enable(int id = 0)
        {
            //var model = _applicationUserManager.SingleOrDefault(id);
            //if (model == null)
            //    return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);
            //model.IsActive = true;
            //await _uow.SaveAllChangesAsync();

            //return RedirectToAction(MVC.Reseller.Company.ActionNames.Index);

            return View();
        }
        #endregion
        
        

    }
}