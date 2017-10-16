using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using CaptchaMvc.Attributes;
using Netotik.Web.Infrastructure;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.ViewModels.Common.ContactUs;
using Netotik.Domain.Entity;
using Netotik.Web.Infrastructure.Caching;
using Netotik.Common.Controller;
using Netotik.Resources;

namespace Netotik.Web.Controllers
{
    public partial class ContactUsController : BaseController
    {
        private readonly IInboxContactUsMessageService _inboxMessageService;
        private readonly ISettingService _settingService;
        private readonly IUserMailer _userMailer;
        private readonly IUnitOfWork _uow;
        public ContactUsController(IInboxContactUsMessageService inboxMessageService, ISettingService settingService, IUnitOfWork uow, IUserMailer userMailer)
        {
            _settingService = settingService;
            _userMailer = userMailer;
            _inboxMessageService = inboxMessageService;
            _uow = uow;
        }

        public virtual ActionResult Index()
        {
            ViewBag.siteConfig = PublicUICache.GetSiteConfig(HttpContext, _settingService);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaVerify("تصویر امنیتی را درست وارد کنید")]
        public virtual async Task<ActionResult> Index(MessageModel model)
        {
            ViewBag.siteConfig = PublicUICache.GetSiteConfig(HttpContext, _settingService);
            if (ModelState.IsValid)
            {
                try
                {
                    InboxContactUsMessage entity = new InboxContactUsMessage
                    {
                        Name = model.Name,
                        Message = model.Text,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        CreateDate = DateTime.Now
                    };
                    _inboxMessageService.Add(entity);
                    await _uow.SaveChangesAsync();


                    _userMailer.ContactUsEmail(new ViewModels.Identity.Account.EmailContactUsViewModel()
                    {
                        ContactUsCreateDate=entity.CreateDate,
                        ContactUsEmail = entity.Email,
                        ContactUsMessage=entity.Message,
                        ContactUsName = entity.Name,
                        ContactUsPhoneNumber=entity.PhoneNumber,
                        From=entity.Email,
                        Subject = "پیام از صفحه درباره ما سایت نتوتیک",
                        To ="ehsan2912.em@gmail.com",
                        ViewName= MVC.UserMailer.Views.ViewNames.ContactUs,
                    }).Send();
                    _userMailer.ContactUsEmail(new ViewModels.Identity.Account.EmailContactUsViewModel()
                    {
                        ContactUsCreateDate = entity.CreateDate,
                        ContactUsEmail = entity.Email,
                        ContactUsMessage = entity.Message,
                        ContactUsName = entity.Name,
                        ContactUsPhoneNumber = entity.PhoneNumber,
                        From = entity.Email,
                        Subject = "پیام از صفحه درباره ما سایت نتوتیک",
                        To = "pouriya.adibfar@gmail.com",
                        ViewName = MVC.UserMailer.Views.ViewNames.ContactUs,
                    }).Send();
                    _userMailer.ContactUsEmail(new ViewModels.Identity.Account.EmailContactUsViewModel()
                    {
                        ContactUsCreateDate = entity.CreateDate,
                        ContactUsEmail = entity.Email,
                        ContactUsMessage = entity.Message,
                        ContactUsName = entity.Name,
                        ContactUsPhoneNumber = entity.PhoneNumber,
                        From = entity.Email,
                        Subject = "نتوتیک - پیام شما دریافت شد",
                        To = entity.Email,
                        ViewName = MVC.UserMailer.Views.ViewNames.ContactUsReplayToClient,
                    }).Send();
                    ModelState.Clear();
                    this.MessageSuccess(Captions.MissionSuccess, Captions.Sended);
                }
                catch
                {
                }

            }
            return View();
        }
    }
}