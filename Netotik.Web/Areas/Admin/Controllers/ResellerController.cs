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
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Identity.UserReseller;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست شرکت ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ResellerController : BaseController
    {

        #region ctor
        //private readonly IUserResellerService _resellerService;
        private readonly IPictureService _pictureService;
        //private readonly IRoleService _RoleService;
        private readonly IUnitOfWork _uow;

        public ResellerController(
            IPictureService pictureservice,
            //  IUserResellerService resellerService,
            //IRoleService RoleService,
            IUnitOfWork uow)
        {
            _pictureService = pictureservice;
            //_RoleService = RoleService;
            //_resellerService = resellerService;
            _uow = uow;
        }
        #endregion





        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessUser)]
        public virtual ActionResult Index(string Search, int Page = 1, int PageSize = 10)
        {
            var pageList = new List<TableResellerModel>(); //_resellerService.GetDataTableResellerAccounts(Search).ToPagedList<TableResellerModel>(Page, PageSize);

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.Reseller.Views._Table, pageList);
            else
                return View(MVC.Admin.Reseller.ActionNames.Index, pageList);
        }
        #endregion

        #region Create


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateUser)]
        [BreadCrumb(Title = "شرکت جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            return View();
        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateUser)]
        [AllowUploadSpecialFilesOnly(".jpg,.png,.gif", true)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ResellerAddModel model, ActionType actionType)
        {

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            else
            {

                //#region Initil User
                var now = DateTime.Now;
                //var company = new Domain.Entity.Reseller()
                //{
                //    UserName = model.Email,
                //    Email = model.Email,
                //    MobileNumber = model.MobileNumber,
                //    Name = model.Name,
                //    PostalCode = long.Parse(model.PostalCode),
                //    PersonCode = long.Parse(model.PersonCode),
                //    CompanyName = model.CompanyName,
                //    Address = model.Address,
                //    Password = Encryption.EncryptingPassword(model.Password),
                //    IsActive = true,
                //    CreateDate = now
                //};
                //#endregion

                //#region SaveChanges
                //_resellerService.Add(company);
                //if (result.Status)
                //{
                //    try
                //    {
                //        await _uow.SaveChangesAsync();
                //    }
                //    catch (DbEntityValidationException ex)
                //    {
                //        this.MessageError(Messages.MissionFail, Messages.AddError);
                //    }
                //    catch (Exception ex)
                //    {
                //        this.MessageError(Messages.MissionFail, Messages.AddError);
                //    }
                //}
                //#endregion

                //SetResultMessage(result);
                //if (!result.Status) return View();

                //if (actionType == ActionType.SaveContinue) return RedirectToAction(MVC.Admin.Reseller.Edit(company.Id));
                return RedirectToAction(MVC.Admin.Reseller.Index());
            }

        }

        #endregion

        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessUser)]
        public virtual ActionResult Detail(int id)
        {
            //var user = _resellerService.SingleOrDefault(id);
            //if (user == null)
            //    return RedirectToAction(MVC.Admin.User.ActionNames.Index);

            //return View(user);

            return View();
        }

        #endregion

        #region Edit
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [BreadCrumb(Title = "ویرایش", Order = 1)]
        public virtual ActionResult Edit(int id)
        {
            //var model = _resellerService.SingleOrDefault(id);
            //if (model == null)
            //    return RedirectToAction(MVC.Admin.Reseller.ActionNames.Index);

            //var editModel = new AdminEditModel
            //{
            //    Email = model.Email,
            //    Id = model.Id,
            //    Address = model.Address,
            //    CompanyName = model.CompanyName,
            //    PersonCode = model.PersonCode.ToString(),
            //    PostalCode = model.PostalCode.ToString(),
            //    MobileNumber = model.MobileNumber,
            //    Name = model.Name
            //};


            //return View(editModel);


            return View();

        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Edit(ResellerEditModel model, ActionType actionType)
        {
            //var company = _resellerService.SingleOrDefault(model.Id);
            //if (company == null)
            //    return RedirectToAction(MVC.Admin.User.ActionNames.Index);

            //if (ModelState.IsValid)
            //{
            //    company.Address = model.Address;
            //    company.UserName = model.Email;
            //    company.PostalCode = Int64.Parse(model.PostalCode);
            //    company.PersonCode = Int64.Parse(model.PersonCode);
            //    company.Name = model.Name;
            //    company.CompanyName = model.CompanyName;
            //    company.Email = model.Email;
            //    company.MobileNumber = model.MobileNumber;

            //    _resellerService.Update(company);

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

            //    if (actionType == ActionType.SaveContinue) return RedirectToAction(MVC.Admin.Reseller.Edit(company.Id));
            //    return RedirectToAction(MVC.Admin.Reseller.Index());
            //}
            //else
            //{
            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
            //    return View();
            //}

            return View();

        }


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        public virtual ActionResult ChangePassword(int id)
        {
            //var company = _resellerService.SingleOrDefault(id);
            //return View(new ChangePasswordModel() { Id = company.Id });
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditUser)]
        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            //if (!ModelState.IsValid)
            //{

            //    this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
            //}
            //else
            //{
            //    var result = await _resellerService.ChangePasswordAsync(model.Id.Value, Encryption.EncryptingPassword(model.Password));
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
            //return View(model);

            return View();
        }

        #endregion

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Disable(int id = 0)
        {
            //var model = _resellerService.SingleOrDefault(id);
            //if (model == null)
            //    return RedirectToAction(MVC.Admin.Reseller.ActionNames.Index);
            //model.IsActive = false;
            //await _uow.SaveAllChangesAsync();

            //return RedirectToAction(MVC.Admin.Reseller.ActionNames.Index);

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Enable(int id = 0)
        {
            //var model = _resellerService.SingleOrDefault(id);
            //if (model == null)
            //    return RedirectToAction(MVC.Admin.Reseller.ActionNames.Index);
            //model.IsActive = true;
            //await _uow.SaveAllChangesAsync();

            //return RedirectToAction(MVC.Admin.Reseller.ActionNames.Index);
            return View();
        }
        


    }

}
