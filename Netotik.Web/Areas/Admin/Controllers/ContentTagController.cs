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
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.CMS.ContentTag;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{

    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessTag)]
    [BreadCrumb(Title = "لیست برچسب ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class ContentTagController : BaseController
    {


        #region ctor
        private readonly IContentTagService _contentTagService;
        private readonly IUnitOfWork _uow;

        public ContentTagController(
            IContentTagService contentTagService,
            IUnitOfWork uow)
        {
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
            return PartialView(MVC.Admin.ContentTag.Views._Create);
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(ContentTagModel model, ActionType actionType = ActionType.Save)
        {

            var a = actionType;
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.ContentTag.Index());
            }
            var tag = new ContentTag()
            {
                Name = model.Name,
            };

            _contentTagService.Add(tag);
            try
            {
                await _uow.SaveChangesAsync();
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return RedirectToAction(MVC.Admin.ContentTag.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.ContentTag.Index());
        }
        #endregion


        #region Edit
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var tag = _contentTagService.SingleOrDefault(id);
            if (tag != null)
            {
                _contentTagService.Remove(tag);
                await _uow.SaveChangesAsync();
                this.MessageInformation(Messages.MissionSuccess, Messages.RemoveSuccess);
            }

            return RedirectToAction(MVC.Admin.ContentTag.Index());
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _contentTagService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.ContentTag.Index());

            return PartialView(MVC.Admin.ContentTag.Views._Edit,
                new ContentTagModel
                {
                    Id = model.Id,
                    Name = model.Name
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
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.ContentTag.Index());
            }

            tag.Name = model.Name;

            _contentTagService.Update(tag);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return RedirectToAction(MVC.Admin.ContentTag.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.ContentTag.Index());
        }

        #endregion



        #region private

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsTagNameExist(string name, int? id)
        {
            return await _contentTagService.IsExistByName(name, id) ? Json(false) : Json(true);
        }
        #endregion



    }
}