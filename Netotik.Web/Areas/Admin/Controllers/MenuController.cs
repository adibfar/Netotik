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
using Netotik.ViewModels.CMS.Menu;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using Netotik.Common.Extensions;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessMenu)]
    [BreadCrumb(Title = "MenuesList", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon-th-large")]
    public partial class MenuController : BaseController
    {

        #region ctor
        private readonly ILanguageService _languageService;
        private readonly IMenuService _menuService;
        private readonly IUnitOfWork _uow;

        public MenuController(
            ILanguageService languageService,
            IMenuService menuService,
            IUnitOfWork uow)
        {
            _languageService = languageService;
            _menuService = menuService;
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

            var result = _menuService.GetList(model, out totalCount, out showCount);

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
            PopulateLangauges();
            PopulateParents();
            return View(MVC.Admin.Menu.Views._Create, new MenuModel() { Order = 0, MenuLocation = MenuLocation.Header });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(MenuModel model, ActionType actionType = ActionType.Save)
        {
            PopulateParents(model.ParentId);

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Menu.Index());
            }

            var menu = new Menu()
            {
                Text = model.Text,
                Url = model.Url,
                ParentId = model.ParentId,
                IsActive = model.IsActive,
                Icon = model.Icon,
                LanguageId = model.LanguageId,
                Description = model.Description,
                Order = model.Order,
                MenuLocation = model.MenuLocation
            };

            _menuService.Add(menu);


            try
            {
                await _uow.SaveChangesAsync();
                ModelState.Clear();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.Menu.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.Menu.Index());
        }
        #endregion

        #region Edit
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var menu = _menuService.SingleOrDefault(id);
            if (menu != null)
            {
                _menuService.Remove(menu);
                await _uow.SaveChangesAsync();
                this.MessageInformation(Captions.MissionSuccess, Captions.RemoveSuccess);
            }
            return RedirectToAction(MVC.Admin.Menu.Index());
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _menuService.SingleOrDefault(id);
            if (model == null) return RedirectToAction(MVC.Admin.Menu.Index());

            PopulateParents(model.ParentId, model.Id);
            PopulateLangauges(model.LanguageId);
            return PartialView(MVC.Admin.Menu.Views._Edit, new MenuModel
            {
                Id = model.Id,
                Text = model.Text,
                Url = model.Url,
                Order = model.Order,
                Icon = model.Icon,
                LanguageId = model.LanguageId,
                ParentId = model.ParentId,
                Description = model.Description,
                IsActive = model.IsActive,
                MenuLocation = model.MenuLocation
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(MenuModel model, ActionType actionType = ActionType.Save)
        {

            var menu = _menuService.SingleOrDefault(model.Id);
            if (menu == null)
                return RedirectToAction(MVC.Admin.Menu.Index());

            PopulateParents(model.ParentId, model.Id.Value);

            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Menu.Index());
            }

            menu.Text = model.Text;
            menu.IsActive = model.IsActive;
            menu.Url = model.Url;
            menu.ParentId = model.ParentId;
            menu.LanguageId = model.LanguageId;
            menu.Order = model.Order;
            menu.Icon = model.Icon;
            menu.MenuLocation = model.MenuLocation;

            _menuService.Update(menu);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return RedirectToAction(MVC.Admin.Menu.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Menu.Index());
        }

        #endregion

        #region private
        private void PopulateParents(int? selectedId = null, int menuId = 0)
        {
            var list = _menuService.All().ToList().Where(x => x.Id != menuId)
                .Select(x => new Menu { Text = MenuExtension.GetName(x.Parent, x.Text), Id = x.Id }).ToList();
            ViewBag.Menues = new SelectList(list, "Id", "Text", selectedId);
        }


        private void PopulateLangauges(int? selectedId = null)
        {
            var list = _languageService.All().Where(x => x.Published).ToList();
            ViewBag.Languages = new SelectList(list, "Id", "Name", selectedId);
        }

        #endregion
    }
}