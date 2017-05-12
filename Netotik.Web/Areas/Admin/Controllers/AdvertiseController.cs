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
using Netotik.ViewModels.CMS.Advertise;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Web.Areas.Admin.Controllers
{
    [BreadCrumb(Title = "لیست بنرها", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
 Order = 0, GlyphIcon = "icon icon-table")]
    [Mvc5Authorize(Roles = AssignableToRolePermissions.CanAccessAdvertise)]
    public partial class AdvertiseController : BaseController
    {

        #region ctor
        private readonly IAdvertiseService _advertiseService;
        private readonly IUnitOfWork _uow;

        public AdvertiseController(
            IAdvertiseService advertiseService,
            IUnitOfWork uow)
        {
            _advertiseService = advertiseService;
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

            var result = _advertiseService.GetList(model, out totalCount, out showCount);

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
            return PartialView(MVC.Admin.Advertise.Views._Create, new AdvertiseModel { Order = 0 });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Create(AdvertiseModel model, ActionType actionType)
        {
            if (!ModelState.IsValid || model.Image == null)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Advertise.Index());
            }

            #region Initial Content
            var ads = new Netotik.Domain.Entity.Advertise()
            {
                Alt = model.Alt,
                Url = model.Url,
                Order = model.Order,
            };

            #endregion

            #region Add Content Image
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var fileName = SaveFile(model.Image, FilePathes._imagesAdvertisePath);

                var picture = new Picture
                {
                    FileName = fileName,
                    OrginalName = model.Image.FileName,
                    MimeType = model.Image.ContentType
                };
                ads.Picture = picture;
            }
            #endregion

            _advertiseService.Add(ads);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.AddError);
                return RedirectToAction(MVC.Admin.Advertise.Index());
            }
            this.MessageSuccess(Messages.MissionSuccess, Messages.AddSuccess);
            return RedirectToAction(MVC.Admin.Advertise.Index());
        }
        #endregion


        #region Edit
        [HttpPost]
        public virtual async Task<ActionResult> Remove(int id)
        {
            var advertise = _advertiseService.All().Where(x => x.Id == id).Include(x => x.Picture).FirstOrDefault();
            if (advertise != null)
            {
                DeleteFile(Server.MapPath(Path.Combine(FilePathes._imagesAdvertisePath, advertise.Picture.FileName)));
                _advertiseService.Remove(advertise);
                await _uow.SaveChangesAsync();

            }
            return RedirectToAction(MVC.Admin.Advertise.Index());
        }

        public virtual async Task<ActionResult> Edit(int id)
        {
            var model = await _advertiseService.All().Include(x => x.Picture).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return RedirectToAction(MVC.Admin.Advertise.Index());


            var editModel = new AdvertiseModel
            {
                Id = model.Id,
                Url = model.Url,
                Alt = model.Alt,
                Order = model.Order,
                Picture = model.Picture
            };

            return PartialView(MVC.Admin.Advertise.Views._Edit, editModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(AdvertiseModel model, ActionType actionType)
        {
            var ads = _advertiseService.SingleOrDefault(model.Id);
            if (ads == null)
                return RedirectToAction(MVC.Admin.Advertise.Index());


            if (!ModelState.IsValid)
            {
                this.MessageError(Messages.MissionFail, Messages.InvalidDataError);
                return RedirectToAction(MVC.Admin.Advertise.Index());
            }

            #region Update

            ads.Order = model.Order;
            ads.Url = model.Url;
            ads.Alt = model.Alt;

            if (model.Image != null)
            {
                #region Add Content Image
                if (model.Image != null && model.Image.ContentLength > 0)
                {
                    var fileName = SaveFile(model.Image, FilePathes._imagesAdvertisePath);

                    var picture = new Picture
                    {
                        FileName = fileName,
                        OrginalName = model.Image.FileName,
                        MimeType = model.Image.ContentType
                    };
                    ads.Picture = picture;
                }
                #endregion
            }
            #endregion

            _advertiseService.Update(ads);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.MessageError(Messages.MissionFail, Messages.UpdateError);
                return RedirectToAction(MVC.Admin.Advertise.Index());
            }

            this.MessageSuccess(Messages.MissionSuccess, Messages.UpdateSuccess);
            return RedirectToAction(MVC.Admin.Advertise.Index());
        }
        #endregion
    }
}
