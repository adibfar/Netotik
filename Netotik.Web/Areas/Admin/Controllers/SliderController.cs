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
using DNTBreadCrumb;
using Netotik.ViewModels.Identity.Security;
using Netotik.ViewModels.CMS.Slider;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "SlidesList", UseDefaultRouteUrl = true,Order = 0, GlyphIcon = "icon-th-large")]
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessSlider)]
    public partial class SliderController : BaseController
    {

        #region ctor
        private readonly ILanguageService _languageService;
        private readonly ISliderService _sliderService;
        private readonly IUnitOfWork _uow;

        public SliderController(
            ILanguageService languageService,
            ISliderService sliderService,
            IUnitOfWork uow)
        {
            _languageService = languageService;
            _sliderService = sliderService;
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
            var result = _sliderService
                .All().Include(x => x.Language)
                .ToList()
                .Select((x, index) => new SliderItem
                {
                    Id = x.Id,
                    ImageFileName = Path.Combine(FilePathes._imagesSliderPath, x.Picture.FileName),
                    FlagLanguage = x.Language.FlagImageFileName,
                    IsActive = x.IsActive,
                    Order = x.Order,
                    Url = x.Url,
                    RowNumber = ++index
                })
                .ToList();

            return Json(new
            {
                sEcho = model.sEcho,
                iTotalRecords = result.Count,
                iTotalDisplayRecords = result.Count,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create
        public virtual ActionResult Create()
        {
            PopulateLangauges();
            return PartialView(MVC.Admin.Slider.Views._Create, new SliderModel { Order = 0, IsActive = true });
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(SliderModel model, ActionType actionType = ActionType.Save)
        {
            if (!ModelState.IsValid || model.Image == null)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Slider.Index());
            }

            #region Initial Slider
            var slider = new Netotik.Domain.Entity.Slider()
            {
                LanguageId = model.LanguageId,
                Url = model.Url,
                Order = model.Order,
                IsActive = model.IsActive
            };

            #endregion

            #region Add Content Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesSliderPath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                slider.Picture = picture;
            }
            #endregion

            _sliderService.Add(slider);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.AddError);
                return RedirectToAction(MVC.Admin.Slider.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.AddSuccess);
            return RedirectToAction(MVC.Admin.Slider.Index());
        }
        #endregion

        #region Edit
        public virtual async Task<ActionResult> Remove(int id = 0)
        {
            var slider = _sliderService.All().Where(x => x.Id == id).Include(x => x.Picture).FirstOrDefault();
            if (slider != null)
            {
                DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesSliderPath, slider.Picture.FileName)));
                _sliderService.Remove(slider);
                await _uow.SaveChangesAsync();

            }
            return RedirectToAction(MVC.Admin.Slider.Index());
        }

        [BreadCrumb(Title = "ویرایش اسلاید", Order = 1)]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = await _sliderService.All().Include(x => x.Picture).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return RedirectToAction(MVC.Admin.Slider.ActionNames.Index);

            ViewBag.Avatar = Path.Combine(FilePathes._imagesSliderPath, model.Picture.FileName);
            PopulateLangauges(model.LanguageId);
            var editModel = new SliderModel
            {
                Id = model.Id,
                Url = model.Url,
                LanguageId = model.LanguageId,
                Order = model.Order,
                IsActive = model.IsActive
            };

            return View(MVC.Admin.Slider.Views._Edit, editModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(SliderModel model, ActionType actionType)
        {
            var slider = _sliderService.SingleOrDefault(model.Id);
            if (slider == null)
                return HttpNotFound();


            if (!ModelState.IsValid)
            {
                this.MessageError(Captions.MissionFail, Captions.InvalidDataError);
                return RedirectToAction(MVC.Admin.Slider.Index());
            }

            #region Update

            slider.IsActive = model.IsActive;
            slider.Order = model.Order;
            slider.Url = model.Url;
            slider.LanguageId = model.LanguageId;

            if (model.Image != null)
            {
                #region Add Content Image
                if (model.Image != null && model.Image.ContentLength > 0)
                {
                    var fileName = SaveFile(model.Image, FilePathes._imagesSliderPath);

                    var picture = new Picture
                    {
                        FileName = fileName,
                        OrginalName = model.Image.FileName,
                        MimeType = model.Image.ContentType
                    };
                    slider.Picture = picture;
                }
                #endregion
            }
            ViewBag.Avatar = Path.Combine(FilePathes._imagesSliderPath, slider.Picture.FileName);
            #endregion

            _sliderService.Update(slider);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                this.MessageError(Captions.MissionFail, Captions.UpdateError);
                return RedirectToAction(MVC.Admin.Slider.Index());
            }

            this.MessageSuccess(Captions.MissionSuccess, Captions.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Slider.Index());

        }
        #endregion

        private void PopulateLangauges(int? selectedId = null)
        {
            var list = _languageService.All().Where(x => x.Published).ToList();
            ViewBag.Languages = new SelectList(list, "Id", "Name", selectedId);
        }

    }
}