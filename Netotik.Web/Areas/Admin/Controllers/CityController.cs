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
using System.Web.UI;
using System.Threading.Tasks;
using Netotik.Web;
using System.Data.Entity.Validation;

using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.Common.City;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessCity)]
    [BreadCrumb(Title = "لیست شهرها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
    Order = 0, GlyphIcon = "icon icon-table")]
    public partial class CityController : BaseController
    {

        #region ctor
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IUnitOfWork _uow;

        public CityController(
            IStateService stateService,
            ICityService cityService,
            IUnitOfWork uow)
        {
            _cityService = cityService;
            _stateService = stateService;
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

            var result = _cityService.GetList(model, out totalCount, out showCount);

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
        [BreadCrumb(Title = "شهر جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            LoadState();
            return PartialView(MVC.Admin.City.Views._Create, new CityModel { IsActive = true });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(CityModel model, ActionType actionType = ActionType.Save)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.City.Index());
            }

            var city = new City()
            {
                Name = model.Name,
                IsActive = model.IsActive,
                StateId = model.StateId,
                IsDefault = model.IsDefault
            };

            _cityService.Add(city);


            try
            {
                await _uow.SaveChangesAsync();
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return RedirectToAction(MVC.Admin.City.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.City.Index());

        }
        #endregion

        #region Edit
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var city = _cityService.SingleOrDefault(id);
            if (city != null && !city.IsDeleted)
            {
                city.IsDeleted = true;
                await _uow.SaveChangesAsync();
                this.MessageInformation(Messages.MissionSuccess, Messages.RemoveSuccess);
            }

            return RedirectToAction(MVC.Admin.City.Index());
        }

        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = _cityService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.City.Index());

            LoadState(model.StateId);

            return PartialView(MVC.Admin.City.Views._Edit,
                new CityModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsActive = model.IsActive,
                    StateId = model.StateId,
                    IsDefault = model.IsDefault
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(CityModel model, ActionType actionType = ActionType.Save)
        {

            var city = _cityService.SingleOrDefault(model.Id);
            if (city == null)
                return RedirectToAction(MVC.Admin.City.Index());

            LoadState(model.StateId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.City.Index());
            }

            city.Name = model.Name;
            city.IsDefault = model.IsDefault;
            city.StateId = model.StateId;
            city.IsActive = model.IsActive;

            _cityService.Update(city);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return RedirectToAction(MVC.Admin.City.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.City.Index());
        }

        #endregion



        #region Private

        private void LoadState(int? selectedId = null)
        {
            var list = _stateService.All().Where(x => !x.IsDeleted).ToList();
            ViewBag.States = new SelectList(list, "Id", "Name", selectedId);
        }


        #endregion




    }
}