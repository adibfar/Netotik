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
using Netotik.ViewModels.CMS.ContentTag;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using Netotik.Web.Infrastructure.Caching;

namespace Netotik.Web.Areas.Admin.Controllers
{

    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessTag)]
    [BreadCrumb(Title = "TagsList", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon-th-large")]
    public partial class ContentTagController : BaseController
    {


        #region ctor
        private readonly ILanguageService _languageService;
        private readonly IContentTagService _contentTagService;
        private readonly IUnitOfWork _uow;

        public ContentTagController(
            ILanguageService languageService,
        IContentTagService contentTagService,
            IUnitOfWork uow)
        {
            _languageService = languageService;
            _contentTagService = contentTagService;
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

            var result = _contentTagService.GetList(model, out totalCount, out showCount);

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

        [HttpGet]
        public virtual ActionResult Create()
        {
            PopulateLangauges();
            return PartialView(MVC.Admin.ContentTag.Views._Create, new ContentTagModel { LanguageId = LanguageCache.GetLanguage(HttpContext).Id });
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ContentTagModel model, ActionType actionType = ActionType.Save)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.ContentTag.Index());
            }
            var tag = new ContentTag()
            {
                LanguageId = model.LanguageId,
                Name = model.Name,
            };

            _contentTagService.Add(tag);
            try
            {
                await _uow.SaveChangesAsync();
                ModelState.Clear();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.ContentTag.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.ContentTag.Index());
        }
        #endregion


        #region Edit
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var tag = _contentTagService.SingleOrDefault(id);
            if (tag != null)
            {
                _contentTagService.Remove(tag);
                await _uow.SaveChangesAsync();
                this.MessageInformation(Captions.MissionSuccess, Captions.RemoveSuccess);
            }

            return RedirectToAction(MVC.Admin.ContentTag.Index());
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _contentTagService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.ContentTag.Index());

            PopulateLangauges(model.LanguageId);
            return PartialView(MVC.Admin.ContentTag.Views._Edit,
                new ContentTagModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    LanguageId = model.LanguageId
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ContentTagModel model, ActionType actionType = ActionType.Save)
        {

            var tag = _contentTagService.SingleOrDefault(model.Id);
            if (tag == null)
                return RedirectToAction(MVC.Admin.ContentTag.Index());

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.ContentTag.Index());
            }

            tag.Name = model.Name;
            tag.LanguageId = model.LanguageId;

            _contentTagService.Update(tag);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return RedirectToAction(MVC.Admin.ContentTag.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.ContentTag.Index());
        }

        #endregion



        #region remote

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsTagNameExist(string name, int? id)
        {
            return await _contentTagService.IsExistByName(name, id) ? Json(false) : Json(true);
        }
        #endregion


        private void PopulateLangauges(int? selectedId = null)
        {
            var list = _languageService.All().Where(x => x.Published).ToList();
            ViewBag.Languages = new SelectList(list, "Id", "Name", selectedId);
        }


    }
}