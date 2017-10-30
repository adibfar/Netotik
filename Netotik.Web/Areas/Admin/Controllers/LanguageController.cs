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
using System.Xml;
using System.Text;
using System.Net;

namespace Netotik.Web.Areas.Admin.Controllers
{

    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessState)]
    [BreadCrumb(Title = "LanguagesList", UseDefaultRouteUrl = true, Order = 0, GlyphIcon = "icon-th-large")]
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
            return PartialView(MVC.Admin.Language.Views._Create, new LanguageModel { Published = true });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(LanguageModel model, ActionType actionType = ActionType.Save)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            if (model.ResourcesXml == null)
            {
                this.MessageError(Captions.MissionFail, Captions.XmlNotValid);
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
                this.MessageError(Captions.MissionFail,Captions.XmlNotValid);
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
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.Language.Index());

        }

        public virtual ActionResult CreateByXml()
        {
            return PartialView(MVC.Admin.Language.Views._CreateByXml);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> CreateByXml(LanguageXmlModel model, ActionType actionType = ActionType.Save)
        {
            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            if (model.ResourcesXml == null)
            {
                this.MessageError(Captions.MissionFail, Captions.XmlNotValid);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            try
            {
                var lang = XDocument.Load(model.ResourcesXml.InputStream).Element("Language");


                var entity = new Language()
                {
                    Name = lang.Attribute("Name").Value,
                    LanguageCulture = lang.Attribute("LanguageCulture").Value,
                    UniqueSeoCode = lang.Attribute("UniqueSeoCode").Value,
                    FlagImageFileName = lang.Attribute("Image").Value,
                    DisplayOrder = int.Parse(lang.Attribute("DisplayOrder").Value),
                    Published = false,
                    IsDefault = false,
                    Rtl = lang.Attribute("Rtl").Value == "true" ? true : false
                };



                var resources = lang.Elements("LocaleResource")
                    .Select(e => new Netotik.Domain.Entity.LocaleStringResource
                    {
                        Name = e.Attribute("Name").Value,
                        Value = e.Value,
                    });

                entity.LocaleStringResources = resources.ToList();
                _languageService.Add(entity);
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.XmlNotValid);
                return RedirectToAction(MVC.Admin.Language.Index());
            }


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.Language.Index());

        }

        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var lang = _languageService.SingleOrDefault(id);
            if (lang != null && !lang.IsDefault)
            {
                
                foreach (var item in lang.LocaleStringResources.ToList())
                    _uow.MarkAsDeleted<LocaleStringResource>(item);

                _languageService.Remove(lang);

                await _uow.SaveChangesAsync();
                Netotik.Web.Infrastructure.Caching.LanguageCache.RemoveLanguageCache(HttpContext);
                this.MessageInformation(Captions.MissionSuccess, Captions.RemoveSuccess);
            }

            return RedirectToAction(MVC.Admin.Language.Index());
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _languageService.SingleOrDefault(id);
            if (model == null)
                return RedirectToAction(MVC.Admin.Language.Index());

            LoadFlags(model.FlagImageFileName);

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
            var lang = await _languageService.All().Include(c => c.LocaleStringResources).FirstOrDefaultAsync(x => x.Id == model.Id);
            if (lang == null)
                return RedirectToAction(MVC.Admin.Language.Index());

            LoadFlags(model.FlagImageFileName);


            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
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
                            LanguageId = lang.Id
                        });
                    foreach (var item in lang.LocaleStringResources.ToList())
                        _uow.MarkAsDeleted<LocaleStringResource>(item);
                    lang.LocaleStringResources = resources.ToList();
                }
                catch (Exception ex)
                {
                    this.MessageError(Captions.MissionFail, Captions.XmlNotValid);
                    return RedirectToAction(MVC.Admin.Language.Index());
                }
            }

            _languageService.Update(lang);
            try
            {
                await _uow.SaveChangesAsync();
                Infrastructure.Caching.LanguageCache.RemoveLanguageCache(base.HttpContext);
            }
            catch (Exception ex)
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return RedirectToAction(MVC.Admin.Language.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Language.Index());
        }

        #endregion

        public virtual ActionResult Export(int Id)
        {
            var language = _languageService.All().Include(x => x.LocaleStringResources).FirstOrDefault(x => x.Id == Id);
            if (language == null) return HttpNotFound();

            var items = language.LocaleStringResources.ToList();
            var xml = new XDocument(new XElement("Language",
                new XAttribute("Name", language.Name),
                new XAttribute("Image", language.FlagImageFileName),
                new XAttribute("DisplayOrder", language.DisplayOrder),
                new XAttribute("LanguageCulture", language.LanguageCulture),
                new XAttribute("Rtl", language.Rtl),
                new XAttribute("UniqueSeoCode", language.UniqueSeoCode),
                from item in items
                select new XElement("LocaleResource", new XAttribute("Name", item.Name), item.Value)));
            return File(Encoding.UTF8.GetBytes(xml.ToString()), "application/force-download", "Language" + language.Name + ".xml");
        }

        #region Resource
        [BreadCrumb(Title = "Texts", Order = 2, GlyphIcon = "icon-file-word-o")]
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
                    Key = model.Name,
                    Value = model.Value
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
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Language.Resources(resource.LanguageId));
            }

            resource.Value = model.Value;
            _resourceService.Update(resource);

            try
            {
                await _uow.SaveChangesAsync();
                Infrastructure.Caching.LanguageCache.RemoveLanguageCache(base.HttpContext);
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return RedirectToAction(MVC.Admin.Language.Resources(resource.LanguageId));
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
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

        #endregion


    }
}