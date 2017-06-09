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

using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.CMS.ContentCategory;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessContentCategory)]
    [BreadCrumb(Title = "SubjectsList", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon-th-large")]
    public partial class ContentCategoryController : BaseController
    {

        #region ctor
        private readonly IContentCategoryService _contentCategoryService;
        private readonly ILanguageService _languagesService;
        private readonly IUnitOfWork _uow;

        public ContentCategoryController(
            IContentCategoryService contentCategoryService,
            ILanguageService languagesService,
            IUnitOfWork uow)
        {
            _languagesService = languagesService;
            _contentCategoryService = contentCategoryService;
            _uow = uow;
        }
        #endregion


        #region Index
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _contentCategoryService.GetList(model, out totalCount, out showCount);

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
        public virtual ActionResult Create()
        {
            PopulateCategories();
            PopulateLangauges();
            return PartialView(MVC.Admin.ContentCategory.Views._Create);
        }

        [ValidateAntiForgeryToken]
        [HttpPost,]
        public virtual async Task<ActionResult> Create(ContentCategoryModel model, ActionType actionType = ActionType.Save)
        {
            PopulateCategories(model.ParentId);
            PopulateLangauges(model.LanguageId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.ContentCategory.Index());
            }

            var subject = new ContentCategory()
            {
                Name = model.Name,
                ParentId = model.ParentId,
                LanguageId = model.LanguageId
            };

            _contentCategoryService.Add(subject);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return View();
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.ContentCategory.Index());
        }
        #endregion


        #region Edit
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {

            var subject = _contentCategoryService.SingleOrDefault(id);
            if (subject != null)
            {
                subject.IsDeleted = true;
                await _uow.SaveChangesAsync();
                this.MessageSuccess(Captions.MissionSuccess, Captions.RemoveSuccess);
                return RedirectToAction(MVC.Admin.ContentCategory.ActionNames.Index);
            }
            this.MessageError(Captions.MissionFail, Captions.RemoveError);
            return RedirectToAction(MVC.Admin.ContentCategory.ActionNames.Index);

        }


        public virtual ActionResult Edit(int id)
        {
            var model = _contentCategoryService.SingleOrDefault(id);
            if (model == null) return RedirectToAction(MVC.Admin.ContentCategory.Index());
            PopulateCategories(model.ParentId, model.Id);
            PopulateLangauges(model.LanguageId);

            return PartialView(MVC.Admin.ContentCategory.Views._Edit
                , new ContentCategoryModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    ParentId = model.ParentId,
                    LanguageId = model.LanguageId
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ContentCategoryModel model, ActionType actionType = ActionType.Save)
        {
            var cat = _contentCategoryService.SingleOrDefault(model.Id);
            if (cat == null) return RedirectToAction(MVC.Admin.ContentCategory.ActionNames.Index);

            PopulateCategories(model.Id, model.ParentId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.ContentCategory.Index());
            }

            cat.Name = model.Name;
            cat.ParentId = model.ParentId;
            cat.LanguageId = model.LanguageId;
            _contentCategoryService.Update(cat);
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateSuccess);
                return View();
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.ContentCategory.Index());
        }

        #endregion



        #region Private

        private void PopulateCategories(int? selectedId = null, int? catId = null)
        {
            var list = _contentCategoryService.All().Where(x => !x.IsDeleted).ToList();
            if (catId.HasValue) list = list.Where(x => x.Id != catId.Value).ToList();
            ViewBag.Categories = new SelectList(list, "Id", "Name", selectedId);
        }

        private void PopulateLangauges(int? selectedId = null)
        {
            var list = _languagesService.All().Where(x => x.Published).ToList();
            ViewBag.Languages = new SelectList(list, "Id", "Name", selectedId);
        }

        #endregion

        #region RemoteValidations

        [HttpPost]
        [AjaxOnly]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsSubjectNameExist(string name, int? id)
        {
            return Json(!await _contentCategoryService.ExistsByNameAsync(name, id));
        }

        #endregion



    }
}