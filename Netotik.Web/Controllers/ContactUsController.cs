using Netotik.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Netotik.Data;
using System.Threading.Tasks;
using Netotik.Domain.Entity;
using Netotik.Common.Controller;
using Netotik.Web.Caching;
using CaptchaMvc.Attributes;
using Netotik.ViewModels.Common.ContactUs;
using Netotik.Resources;

namespace Netotik.Web.Controllers
{
    public partial class ContactUsController : Controller
    {
        private readonly IInboxContactUsMessageService _inboxMessageService;
        private readonly ISettingService _settingService;
        private readonly IUnitOfWork _uow;
        public ContactUsController(IInboxContactUsMessageService inboxMessageService, ISettingService settingService, IUnitOfWork uow)
        {
            _settingService = settingService;
            _inboxMessageService = inboxMessageService;
            _uow = uow;
        }

        public virtual ActionResult Index()
        {
            ViewBag.siteConfig = WebCache.GetSiteConfig(HttpContext, _settingService);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaVerify("تصویر امنیتی را درست وارد کنید")]
        public virtual async Task<ActionResult> Index(MessageModel model)
        {
            ViewBag.siteConfig = WebCache.GetSiteConfig(HttpContext, _settingService);
            if (ModelState.IsValid)
            {
                try
                {


                    InboxContactUsMessage entity = new InboxContactUsMessage
                    {
                        Name = model.Name,
                        Message = model.Text,
                        PhoneNumber = model.MobileNumber,
                        Email = model.Email,
                        CreateDate = DateTime.Now
                    };
                    _inboxMessageService.Add(entity);
                    await _uow.SaveChangesAsync();

                    this.MessageSuccess("ثبت شد","پیام شما با موفقیت برای مدیریت ارسال شد.");

                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    this.MessageError(Messages.MissionFail, "مشکلی در ارسال پیام بوجود امده، لطفا با ما تماس بگیرید.");
                }

            }
            return View();
        }
    }
}