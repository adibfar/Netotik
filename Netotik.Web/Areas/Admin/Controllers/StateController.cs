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
using Netotik.ViewModels.Common.State;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{

    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessState)]
    [BreadCrumb(Title = "لیست استان ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class StateController : BaseController
    {

        #region ctor
        private readonly IStateService _stateService;
        private readonly IUnitOfWork _uow;

        public StateController(
            IStateService addressStateService,
            IUnitOfWork uow)
        {
            _stateService = addressStateService;
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

            var result = _stateService.GetList(model, out totalCount, out showCount);

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
            return PartialView(MVC.Admin.State.Views._Create, new StateModel { IsActive = true });
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(StateModel model, ActionType actionType = ActionType.Save)
        {

            var a = actionType;
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.State.Index());
            }
            var state = new State()
            {
                Name = model.Name,
                IsDefault = model.IsDefault,
                IsActive = model.IsActive,
            };

            _stateService.Add(state);
            try
            {
                await _uow.SaveChangesAsync();
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return View();
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.State.Index());
        }
        #endregion


        #region Detail

        public virtual ActionResult Detail(int id)
        {
            var state = _stateService.SingleOrDefault(id);
            if (state == null) return HttpNotFound();
            return View(state);
        }

        #endregion


        #region Edit
        [HttpPost]
        [BreadCrumb(Title = "ویرایش استان", Order = 1)]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var state = new State { Id = id };
            _stateService.Remove(state);
            await _uow.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.State.Index());
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _stateService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.State.Index());

            return PartialView(MVC.Admin.State.Views._Edit,
                new StateModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsActive = model.IsActive,
                    IsDefault = model.IsDefault
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(StateModel model, ActionType actionType = ActionType.Save)
        {

            var state = _stateService.SingleOrDefault(model.Id);
            if (state == null)
                return RedirectToAction(MVC.Admin.State.Index());

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.State.Index());
            }

            state.Name = model.Name;
            state.IsDefault = model.IsDefault;
            state.IsActive = model.IsActive;
            _stateService.Update(state);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return RedirectToAction(MVC.Admin.State.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.State.Index());
        }

        #endregion



        #region private

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<JsonResult> IsNameExist(string name, int? id)
        {
            return await _stateService.ExistsByNameAsync(name, id) ? Json(false) : Json(true);
        }
        #endregion



    }
}