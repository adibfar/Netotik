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
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using Netotik.ViewModels.Common.Language;
using System.Xml.Linq;
using System.IO;
using System.Globalization;

namespace Netotik.Web.Areas.Admin.Controllers
{

    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessState)]
    [BreadCrumb(Title = "لیست زبان ها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    public partial class LanguageController : BaseController
    {

        #region ctor
        private readonly ILocaleStringResourceService _resourceService;
        private readonly ILanguageService _languageService;
        private readonly IUnitOfWork _uow;

        public LanguageController(
            ILocaleStringResourceService resourceService,
            ILanguageService languageService,
            IUnitOfWork uow)
        {
            _resourceService = resourceService;
            _languageService = languageService;
            _uow = uow;
        }
        #endregion


        #region Language

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetList(RequestListModel model)
        {
            long totalCount;
            long showCount;

            var result = _languageService.GetList(model, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Create()
        {
            LoadFlags();
            LoadCultures();
            return PartialView(MVC.Admin.Language.Views._Create, new LanguageModel { Published = true });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(LanguageModel model, ActionType actionType = ActionType.Save)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            if (model.ResourcesXml == null)
            {
                this.MessageError(Messages.MissionFail, "فایل xml را وارد کنید");
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            var lang = new Language()
            {
                Name = model.Name,
                LanguageCulture = model.LanguageCulture,
                UniqueSeoCode = model.UniqueSeoCode,
                FlagImageFileName = model.FlagImageFileName,
                DisplayOrder = model.DisplayOrder,
                Published = model.Published,
                IsDefault = model.IsDefault,
                Rtl = model.Rtl
            };

            try
            {
                var resources = XDocument.Load(model.ResourcesXml.InputStream)
                    .Element("Language")
                    .Elements("LocaleResource")
                    .Select(e => new Netotik.Domain.Entity.LocaleStringResource
                    {
                        Name = e.Attribute("Name").Value,
                        Value = e.Value,
                    });
                lang.LocaleStringResources = resources.ToList();
            }
            catch
            {
                this.MessageError(Messages.MissionFail, "فایل xml معتبر نیست");
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            _languageService.Add(lang);


            try
            {
                await _uow.SaveChangesAsync();
                Netotik.Web.Infrastructure.Caching.LanguageCache.RemoveLanguageCache(HttpContext);
                ModelState.Clear();
            }
            catch
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.Language.Index());

        }



        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var lang = _languageService.SingleOrDefault(id);
            if (lang != null && !lang.IsDefault)
            {
                _languageService.Remove(lang);
                await _uow.SaveChangesAsync();
                Netotik.Web.Infrastructure.Caching.LanguageCache.RemoveLanguageCache(HttpContext);
                this.MessageInformation(Messages.MissionSuccess, Messages.RemoveSuccess);
            }

            return RedirectToAction(MVC.Admin.Language.Index());
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _languageService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.Language.Index());

            LoadFlags(model.FlagImageFileName);
            LoadCultures(model.LanguageCulture);

            return PartialView(MVC.Admin.Language.Views._Edit,
                new LanguageModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    DisplayOrder = model.DisplayOrder,
                    FlagImageFileName = model.FlagImageFileName,
                    LanguageCulture = model.LanguageCulture,
                    Published = model.Published,
                    UniqueSeoCode = model.UniqueSeoCode,
                    Rtl = model.Rtl,
                    IsDefault = model.IsDefault
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(LanguageModel model, ActionType actionType = ActionType.Save)
        {
            var lang = _languageService.SingleOrDefault(model.Id);
            if (lang == null)
                return RedirectToAction(MVC.Admin.Language.Index());

            LoadFlags(model.FlagImageFileName);
            LoadCultures(model.LanguageCulture);


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            lang.UniqueSeoCode = model.UniqueSeoCode;
            lang.DisplayOrder = model.DisplayOrder;
            lang.FlagImageFileName = model.FlagImageFileName;
            lang.LanguageCulture = model.LanguageCulture;
            lang.Rtl = model.Rtl;
            lang.Published = model.Published;
            lang.IsDefault = model.IsDefault;

            if (model.ResourcesXml != null)
            {
                try
                {
                    var resources = XDocument.Load(model.ResourcesXml.InputStream)
                        .Element("Language")
                        .Elements("LocaleResource")
                        .Select(e => new Netotik.Domain.Entity.LocaleStringResource
                        {
                            Name = e.Attribute("Name").Value,
                            Value = e.Value,
                        });
                    lang.LocaleStringResources.Clear();
                    lang.LocaleStringResources = resources.ToList();
                }
                catch
                {
                    this.MessageError(Messages.MissionFail, "فایل xml معتبر نیست");
                    return RedirectToAction(MVC.Admin.Language.Index());
                }
            }

            _languageService.Update(lang);

            try
            {
                await _uow.SaveChangesAsync();
                Netotik.Web.Infrastructure.Caching.LanguageCache.RemoveLanguageCache(HttpContext);
            }
            catch
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Language.Index());
        }

        #endregion


        #region Resource
        public virtual ActionResult Resources(int Id)
        {
            return View(MVC.Admin.Language.Views.IndexResources);
        }


        public virtual JsonResult GetResourceList(RequestListModel model, int Id)
        {
            long totalCount;
            long showCount;

            var result = _resourceService.GetList(model, Id, out totalCount, out showCount);

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = showCount,
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }


        public virtual ActionResult EditResource(int id)
        {
            var model = _resourceService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.Language.Index());

            return PartialView(MVC.Admin.Language.Views._EditResource,
                new ResourceModel
                {
                    Id = model.Id,
                    Value = model.Value,
                    Key = model.Name
                });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> EditResource(ResourceModel model, ActionType actionType = ActionType.Save)
        {
            var resource = _resourceService.SingleOrDefault(model.Id);
            if (resource == null)
                return RedirectToAction(MVC.Admin.Language.Index());

            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Language.Resources(resource.LanguageId));
            }

            resource.Value = model.Value;
            _resourceService.Update(resource);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return RedirectToAction(MVC.Admin.Language.Resources(resource.LanguageId));
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Language.Resources(resource.LanguageId));
        }

        #endregion


        #region Private
        private void LoadFlags(string selectedItem = "")
        {
            var files = System.IO.Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/images/flags"))
                .Select(Path.GetFileName);

            ViewBag.Flags = files
                .Select(x => new SelectListItem { Value = x, Text = x, Selected = selectedItem == x ? true : false })
                .ToList();
        }

        private void LoadCultures(string selectedItem = "")
        {
            var list = CultureInfo.GetCultures(CultureTypes.AllCultures)
                          .Except(CultureInfo.GetCultures(CultureTypes.SpecificCultures));

            ViewBag.Cultures = list
                .Select(x => new SelectListItem { Value = x.Name, Text = string.Format("{0} - {1}", x.Name, x.EnglishName), Selected = selectedItem == x.Name ? true : false })
                .ToList();
        }

        #endregion


    }
}