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
using Netotik.ViewModels.Shop.Tax;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessTax)]
    [BreadCrumb(Title = "لیست مالیات", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ShopTaxController : BaseController
    {

        #region ctor
        private readonly ITaxService _taxService;
        private readonly IUnitOfWork _uow;

        public ShopTaxController(
            ITaxService taxService,
            IUnitOfWork uow)
        {
            _taxService = taxService;
            _uow = uow;
        }
        #endregion

        #region Index
        public virtual ActionResult Index()
        {

            var list = _taxService.All().OrderBy(x => x.Percentage).ToList();
            return View(list);

        }
        #endregion

        #region Create
        [BreadCrumb(Title = "مالیات جدید", Order = 1)]
        public virtual ActionResult Create()
        {

            return View(MVC.Admin.ShopTax.ActionNames.Create, new TaxModel { Percent = 0 });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(TaxModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            #region Initial Content
            var tax = new Tax()
            {
                Name = model.Name,
                Percentage = model.Percent,
            };

            #endregion

            _taxService.Add(tax);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.ShopTax.Index());

        }
        #endregion

        #region Edit
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var link = _taxService.SingleOrDefault(id);
            if (link != null)
            {
                _taxService.Remove(link);
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [BreadCrumb(Title = "ویرایش مالیات", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = await _taxService.All().FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return RedirectToAction("Index");


            var editModel = new TaxModel
            {
                Id = model.Id,
                Name = model.Name,
                Percent = model.Percentage
            };

            return View(MVC.Admin.ShopTax.ActionNames.Edit, editModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(TaxModel model, ActionType actionType)
        {
            var tax = _taxService.SingleOrDefault(model.Id);
            if (tax == null)
                return RedirectToAction("Index");


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }
            tax.Name = model.Name;
            tax.Percentage = model.Percent;

            _taxService.Update(tax);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.ShopTax.Index());

        }

        #endregion

    }
}