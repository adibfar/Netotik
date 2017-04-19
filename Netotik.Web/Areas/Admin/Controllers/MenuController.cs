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
using Netotik.ViewModels.CMS.Menu;
using Netotik.Common.Controller;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessMenu)]
    [BreadCrumb(Title = "لیست منو ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class MenuController : BaseController
    {

        #region ctor
        private readonly IMenuService _menuService;
        private readonly IContentService _contentService;
        private readonly IContentCategoryService _sessionSubject;
        private readonly IUnitOfWork _uow;

        public MenuController(
            IMenuService menuService,
            IContentService contentService,
            IContentCategoryService sessionSubject,
            IUnitOfWork uow)
        {
            _sessionSubject = sessionSubject;
            _contentService = contentService;
            _menuService = menuService;
            _uow = uow;
        }
        #endregion


        #region Index
        public virtual ActionResult Index(string Search = "")
        {

            var pageList = _menuService.All()
                .Include(x => x.Parent)
                .Where(x => x.Text.Contains(Search))
                .OrderBy(x => x.ParentId)
                .ToList()
                .Select(x => new TableAdminMenu { Id = x.Id, Url = x.Url, Name = MenuExtension.GetName(x.Parent, x.Text) })
                .ToList();

            if (Request.IsAjaxRequest())
                return View(MVC.Admin.Menu.Views._Table, pageList);
            else
                return View(MVC.Admin.Menu.ActionNames.Index, pageList);
        }
        #endregion


        #region Create

        [BreadCrumb(Title = "منو جدید", Order = 1)]
        public virtual ActionResult Create()
        {
            LoadPages();
            LoadParents();
            return View(new MenuModel() { Order = 0 });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(MenuModel model, ActionType actionType)
        {
            LoadParents(model.ParentId);
            LoadPages();

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View(model);
            }

            var menu = new Menu()
            {
                Text = model.Name,
                Url = model.Url,
                ParentId = model.ParentId,
                IsActive = model.IsActive,
                Icon = model.Icon,
                Description = model.Description,

            };

            _menuService.Add(menu);


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

            this.MessageError(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.Menu.Index());
        }
        #endregion


        #region Detail

        [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessMenu)]
        public virtual ActionResult Detail(int id)
        {
            var sub = _menuService.SingleOrDefault(id);
            if (sub == null)
                return RedirectToAction("Index");

            return PartialView(sub);
        }

        #endregion


        #region Edit
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var menu = new Menu { Id = id };
            _menuService.Remove(menu);
            if (!menu.SubMenues.Any())
                await _uow.SaveChangesAsync();

            return RedirectToAction(MVC.Admin.Menu.ActionNames.Index);
        }

        [BreadCrumb(Title = "ویرایش منو", Order = 1)]
        public virtual ActionResult Edit(int id)
        {
            var model = _menuService.SingleOrDefault(id);
            if (model == null) return RedirectToAction(MVC.Admin.Menu.ActionNames.Index);

            LoadPages();
            LoadParents(model.ParentId, model.Id);
            return View(new MenuModel
            {
                Id = model.Id,
                Name = model.Text,
                Url = model.Url,
                Order = model.Order,
                Icon = model.Icon,
                Description = model.Description,
                IsActive = model.IsActive
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(MenuModel model, ActionType actionType)
        {

            var menu = _menuService.SingleOrDefault(model.Id);
            if (menu == null)
                return RedirectToAction(MVC.Admin.Menu.ActionNames.Index);

            LoadPages();
            LoadParents(model.ParentId, model.Id.Value);

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return View();
            }

            menu.Text = model.Name;
            menu.IsActive = model.IsActive;
            menu.Url = model.Url;
            menu.ParentId = model.ParentId;
            menu.Order = model.Order;
            menu.Icon = model.Icon;

            _menuService.Update(menu);

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
            return RedirectToAction(MVC.Admin.Menu.Index());
        }

        #endregion



        #region private

        private void LoadParents(int? selectedId = null, int menuId = 0)
        {
            var list = _menuService.All().ToList().Where(x => x.Id != menuId)
                .Select(x => new Menu { Text = MenuExtension.GetName(x.Parent, x.Text), Id = x.Id }).ToList();

            ViewBag.Menues = new SelectList(list, "Id", "Text", selectedId);

        }

        private void LoadPages()
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem{Text="لیست صفحات موجود", Value=""},
                new SelectListItem{Text="تماس با ما", Value=Url.Action(MVC.ContactUs.ActionNames.Index,MVC.ContactUs.Name,new {Area=""},null)},
                new SelectListItem{Text="صفحه اصلی", Value=Url.Action(MVC.Home.ActionNames.Index,MVC.Home.Name,new {Area=""},null)},
                new SelectListItem{Text="اخرین مطالب", Value=Url.Action(MVC.Blog.ActionNames.Index, MVC.Blog.Name,new {Area=""},null)},
                new SelectListItem{Text="فروشگاه", Value=Url.Action(MVC.Product.ActionNames.Index, MVC.Product.Name,new {Area="",categoryId=0,brandId=0},null)},
            };
            ViewBag.Pages = list;
        }

        #endregion



    }
}