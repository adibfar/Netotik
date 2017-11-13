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
using Microsoft.AspNet.Identity;
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.CMS.IndexSection;
using Netotik.ViewModels.CMS.ContentCategory;
using Netotik.Services.Identity;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "IndexSections", UseDefaultRouteUrl = true,Order = 0, GlyphIcon = "icon icon-table")]
    public partial class IndexSectionController : BasePanelController
    {

        #region ctor
        private readonly ILanguageService _languageService;
        private readonly IIndexSectionService _indexSectionService;
        private readonly IUnitOfWork _uow;

        public IndexSectionController(
            ILanguageService languageService,
            IIndexSectionService indexSectionService,
            IUnitOfWork uow)
        {
            _indexSectionService = indexSectionService;
            _languageService = languageService;
            _uow = uow;
        }
        #endregion

        #region Index
        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContent)]
        public virtual ActionResult Index()
        {
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContent)]
        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _indexSectionService.GetList(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateContent)]
        [BreadCrumb(Title = "NewIndexSectoin", Order = 1)]
        public virtual ActionResult Create()
        {
            PopulateLangauges();
            return View();
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanCreateContent)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(IndexSectionModel model, ActionType actionType)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.IndexSection.Create());
            }

            #region Initial Section
            var now = DateTime.Now;

            var section = new Netotik.Domain.Entity.IndexSection()
            {
                Html = model.Html,
                Title = model.Title,
                LanguageId = model.LanguageId,
                Order = model.Order
            };

            #endregion

            _indexSectionService.Add(section);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.IndexSection.Create());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.IndexSection.Index());
        }
        #endregion


        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanDeleteContent)]
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id)
        {
            var section = _indexSectionService.SingleOrDefault(id);
            if (section == null) return HttpNotFound();
            _indexSectionService.Remove(section);
            await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.IndexSection.Index());
        }

        #region Edit

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditContent)]
        [BreadCrumb(Title = "EditIndexSectoin", Order = 1)]
        public virtual ActionResult Edit(int id)
        {
            var model = _indexSectionService.SingleOrDefault(id);
            PopulateLangauges(model.LanguageId);

            return View(new IndexSectionModel
            {
                Id = model.Id,
                Html = model.Html,
                Title = model.Title,
                LanguageId = model.LanguageId,
                Order = model.Order
            });
        }

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanEditContent)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(IndexSectionModel model, ActionType actionType)
        {
            var entity = _indexSectionService.SingleOrDefault(model.Id);
            if (entity == null) return HttpNotFound();

            PopulateLangauges(model.LanguageId);
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return View();
            }

            #region Update
            var now = DateTime.Now;

            entity.Html = model.Html;
            entity.Title = model.Title;
            entity.Order = model.Order;
            entity.LanguageId = model.LanguageId;

            #endregion

            _indexSectionService.Update(entity);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return View();
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.IndexSection.Index());
        }
        #endregion

        private void PopulateLangauges(int? selectedId = null)
        {
            var list = _languageService.All().Where(x => x.Published).ToList();
            ViewBag.Languages = new SelectList(list, "Id", "Name", selectedId);
        }


    }
}