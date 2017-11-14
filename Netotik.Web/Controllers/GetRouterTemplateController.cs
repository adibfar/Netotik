using Netotik.Data;
using Netotik.Services.Abstract;
using Netotik.Services.Identity;
using Netotik.ViewModels.Template;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Netotik.Web.Controllers
{
    public partial class GetRouterTemplateController : Controller
    {
        private readonly IApplicationUserManager _applicationUserManager;
        private readonly IMikrotikServices _mikrotikServices;
        private readonly IUnitOfWork _uow;

        public GetRouterTemplateController(
            IMikrotikServices mikrotikServices,
            IApplicationUserManager applicationUserManager,
            IUnitOfWork uow)
        {
            _mikrotikServices = mikrotikServices;
            _applicationUserManager = applicationUserManager;
            _uow = uow;
        }
        public virtual async Task<ActionResult> GetLogin(string RouterCode)
        {
            var user = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (user == null) return Content("Error Not Detect Your Router.");

            DirectoryInfo directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Content\Upload\TemplateSettingsXML"));
            var file = directory.GetFiles().Where(x => x.Name == user.Id.ToString()).FirstOrDefault();

            if (file == null) return Content("Error Not Detect Your Settings.");

            TemplateSettings Tempset = new TemplateSettings();

            var settingXml = XDocument.Load(file.FullName).Elements("TemplateSetting").First();
            Tempset.AutoLogin = settingXml.Element("AutoLogin").Value == "true" ? true : false;
            Tempset.AutoLoginAfterSec = settingXml.Element("AutoLoginAfterSec").Value == "" ? 0 : int.Parse(settingXml.Element("AutoLoginAfterSec").Value);
            Tempset.AutoLoginUser = settingXml.Element("AutoLoginUser").Value;
            Tempset.CustomButton = settingXml.Element("CustomButton").Value == "true" ? true : false;
            Tempset.CustomButtonLink = settingXml.Element("CustomButtonLink").Value;
            Tempset.CustomButtonText = settingXml.Element("CustomButtonText").Value;
            Tempset.InstagramButton = settingXml.Element("InstagramButton").Value == "true" ? true : false;
            Tempset.InstagramButtonLink = settingXml.Element("InstagramButtonLink").Value;
            Tempset.PanelButton = settingXml.Element("PanelButton").Value == "true" ? true : false;
            Tempset.PanelButtonText = settingXml.Element("PanelButtonText").Value;
            Tempset.RedirectToInstagram = settingXml.Element("RedirectToInstagram").Value == "true" ? true : false;
            Tempset.RedirectToTelegram = settingXml.Element("RedirectToTelegram").Value == "true" ? true : false;
            Tempset.RedirectToUrl = settingXml.Element("RedirectToUrl").Value;
            Tempset.RegisterButton = settingXml.Element("RegisterButton").Value == "true" ? true : false;
            Tempset.RegisterButtonText = settingXml.Element("RegisterButtonText").Value;
            Tempset.ShowTrialButton = settingXml.Element("ShowTrialButton").Value == "true" ? true : false;
            Tempset.TelegramButton = settingXml.Element("TelegramButton").Value == "true" ? true : false;
            Tempset.TelegramButtonLink = settingXml.Element("TelegramButtonLink").Value;
            Tempset.Redirect = settingXml.Element("Redirect").Value == "true" ? true : false;
            Tempset.PanelButtonLink = Url.Action(MVC.Login.Client("", user.UserRouter.RouterCode), protocol: "https");
            Tempset.RegisterButtonLink = Url.Action(MVC.Register.Client(user.UserRouter.RouterCode), protocol: "https");

            return View(Tempset);
        }
        public virtual async Task<ActionResult> GetLogout(string RouterCode)
        {
            var user = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (user == null) return Content("Error Not Detect Your Router.");

            DirectoryInfo directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Content\Upload\TemplateSettingsXML"));
            var file = directory.GetFiles().Where(x => x.Name == user.Id.ToString()).FirstOrDefault();

            if (file == null) return Content("Error Not Detect Your Settings.");

            TemplateSettings Tempset = new TemplateSettings();

            var settingXml = XDocument.Load(file.FullName).Elements("TemplateSetting").First();
            Tempset.AutoLogin = settingXml.Element("AutoLogin").Value == "true" ? true : false;
            Tempset.AutoLoginAfterSec = settingXml.Element("AutoLoginAfterSec").Value == "" ? 0 : int.Parse(settingXml.Element("AutoLoginAfterSec").Value);
            Tempset.AutoLoginUser = settingXml.Element("AutoLoginUser").Value;
            Tempset.CustomButton = settingXml.Element("CustomButton").Value == "true" ? true : false;
            Tempset.CustomButtonLink = settingXml.Element("CustomButtonLink").Value;
            Tempset.CustomButtonText = settingXml.Element("CustomButtonText").Value;
            Tempset.InstagramButton = settingXml.Element("InstagramButton").Value == "true" ? true : false;
            Tempset.InstagramButtonLink = settingXml.Element("InstagramButtonLink").Value;
            Tempset.PanelButton = settingXml.Element("PanelButton").Value == "true" ? true : false;
            Tempset.PanelButtonText = settingXml.Element("PanelButtonText").Value;
            Tempset.RedirectToInstagram = settingXml.Element("RedirectToInstagram").Value == "true" ? true : false;
            Tempset.RedirectToTelegram = settingXml.Element("RedirectToTelegram").Value == "true" ? true : false;
            Tempset.RedirectToUrl = settingXml.Element("RedirectToUrl").Value;
            Tempset.RegisterButton = settingXml.Element("RegisterButton").Value == "true" ? true : false;
            Tempset.RegisterButtonText = settingXml.Element("RegisterButtonText").Value;
            Tempset.ShowTrialButton = settingXml.Element("ShowTrialButton").Value == "true" ? true : false;
            Tempset.TelegramButton = settingXml.Element("TelegramButton").Value == "true" ? true : false;
            Tempset.TelegramButtonLink = settingXml.Element("TelegramButtonLink").Value;
            Tempset.Redirect = settingXml.Element("Redirect").Value == "true" ? true : false;
            Tempset.PanelButtonLink = Url.Action(MVC.Login.Client("", user.UserRouter.RouterCode), protocol: "https");
            Tempset.RegisterButtonLink = Url.Action(MVC.Register.Client(user.UserRouter.RouterCode), protocol: "https");

            return View(Tempset);
        }
        public virtual async Task<ActionResult> GetStatus(string RouterCode)
        {
            var user = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (user == null) return Content("Error Not Detect Your Router.");

            DirectoryInfo directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Content\Upload\TemplateSettingsXML"));
            var file = directory.GetFiles().Where(x => x.Name == user.Id.ToString()).FirstOrDefault();

            if (file == null) return Content("Error Not Detect Your Settings.");

            TemplateSettings Tempset = new TemplateSettings();

            var settingXml = XDocument.Load(file.FullName).Elements("TemplateSetting").First();
            Tempset.AutoLogin = settingXml.Element("AutoLogin").Value == "true" ? true : false;
            Tempset.AutoLoginAfterSec = settingXml.Element("AutoLoginAfterSec").Value == "" ? 0 : int.Parse(settingXml.Element("AutoLoginAfterSec").Value);
            Tempset.AutoLoginUser = settingXml.Element("AutoLoginUser").Value;
            Tempset.CustomButton = settingXml.Element("CustomButton").Value == "true" ? true : false;
            Tempset.CustomButtonLink = settingXml.Element("CustomButtonLink").Value;
            Tempset.CustomButtonText = settingXml.Element("CustomButtonText").Value;
            Tempset.InstagramButton = settingXml.Element("InstagramButton").Value == "true" ? true : false;
            Tempset.InstagramButtonLink = settingXml.Element("InstagramButtonLink").Value;
            Tempset.PanelButton = settingXml.Element("PanelButton").Value == "true" ? true : false;
            Tempset.PanelButtonText = settingXml.Element("PanelButtonText").Value;
            Tempset.RedirectToInstagram = settingXml.Element("RedirectToInstagram").Value == "true" ? true : false;
            Tempset.RedirectToTelegram = settingXml.Element("RedirectToTelegram").Value == "true" ? true : false;
            Tempset.RedirectToUrl = settingXml.Element("RedirectToUrl").Value;
            Tempset.RegisterButton = settingXml.Element("RegisterButton").Value == "true" ? true : false;
            Tempset.RegisterButtonText = settingXml.Element("RegisterButtonText").Value;
            Tempset.ShowTrialButton = settingXml.Element("ShowTrialButton").Value == "true" ? true : false;
            Tempset.TelegramButton = settingXml.Element("TelegramButton").Value == "true" ? true : false;
            Tempset.TelegramButtonLink = settingXml.Element("TelegramButtonLink").Value;
            Tempset.Redirect = settingXml.Element("Redirect").Value == "true" ? true : false;
            Tempset.PanelButtonLink = Url.Action(MVC.Login.Client("", user.UserRouter.RouterCode), protocol: "https");
            Tempset.RegisterButtonLink = Url.Action(MVC.Register.Client(user.UserRouter.RouterCode), protocol: "https");

            return View(Tempset);
        }
        public virtual async Task<ActionResult> GetError(string RouterCode)
        {
            var user = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (user == null) return Content("Error Not Detect Your Router.");

            DirectoryInfo directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Content\Upload\TemplateSettingsXML"));
            var file = directory.GetFiles().Where(x => x.Name == user.Id.ToString()).FirstOrDefault();

            if (file == null) return Content("Error Not Detect Your Settings.");

            TemplateSettings Tempset = new TemplateSettings();

            var settingXml = XDocument.Load(file.FullName).Elements("TemplateSetting").First();
            Tempset.AutoLogin = settingXml.Element("AutoLogin").Value == "true" ? true : false;
            Tempset.AutoLoginAfterSec = settingXml.Element("AutoLoginAfterSec").Value == "" ? 0 : int.Parse(settingXml.Element("AutoLoginAfterSec").Value);
            Tempset.AutoLoginUser = settingXml.Element("AutoLoginUser").Value;
            Tempset.CustomButton = settingXml.Element("CustomButton").Value == "true" ? true : false;
            Tempset.CustomButtonLink = settingXml.Element("CustomButtonLink").Value;
            Tempset.CustomButtonText = settingXml.Element("CustomButtonText").Value;
            Tempset.InstagramButton = settingXml.Element("InstagramButton").Value == "true" ? true : false;
            Tempset.InstagramButtonLink = settingXml.Element("InstagramButtonLink").Value;
            Tempset.PanelButton = settingXml.Element("PanelButton").Value == "true" ? true : false;
            Tempset.PanelButtonText = settingXml.Element("PanelButtonText").Value;
            Tempset.RedirectToInstagram = settingXml.Element("RedirectToInstagram").Value == "true" ? true : false;
            Tempset.RedirectToTelegram = settingXml.Element("RedirectToTelegram").Value == "true" ? true : false;
            Tempset.RedirectToUrl = settingXml.Element("RedirectToUrl").Value;
            Tempset.RegisterButton = settingXml.Element("RegisterButton").Value == "true" ? true : false;
            Tempset.RegisterButtonText = settingXml.Element("RegisterButtonText").Value;
            Tempset.ShowTrialButton = settingXml.Element("ShowTrialButton").Value == "true" ? true : false;
            Tempset.TelegramButton = settingXml.Element("TelegramButton").Value == "true" ? true : false;
            Tempset.TelegramButtonLink = settingXml.Element("TelegramButtonLink").Value;
            Tempset.Redirect = settingXml.Element("Redirect").Value == "true" ? true : false;
            Tempset.PanelButtonLink = Url.Action(MVC.Login.Client("", user.UserRouter.RouterCode), protocol: "https");
            Tempset.RegisterButtonLink = Url.Action(MVC.Register.Client(user.UserRouter.RouterCode), protocol: "https");

            return View(Tempset);

        }
        public virtual async Task<ActionResult> GetAlogin(string RouterCode)
        {
            var user = await _applicationUserManager.FindByRouterCodeAsync(RouterCode);
            if (user == null) return Content("Error Not Detect Your Router.");

            DirectoryInfo directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Content\Upload\TemplateSettingsXML"));
            var file = directory.GetFiles().Where(x => x.Name == user.Id.ToString()).FirstOrDefault();

            if (file == null) return Content("Error Not Detect Your Settings.");

            TemplateSettings Tempset = new TemplateSettings();

            var settingXml = XDocument.Load(file.FullName).Elements("TemplateSetting").First();
            Tempset.AutoLogin = settingXml.Element("AutoLogin").Value == "true" ? true : false;
            Tempset.AutoLoginAfterSec = settingXml.Element("AutoLoginAfterSec").Value == "" ? 0 : int.Parse(settingXml.Element("AutoLoginAfterSec").Value);
            Tempset.AutoLoginUser = settingXml.Element("AutoLoginUser").Value;
            Tempset.CustomButton = settingXml.Element("CustomButton").Value == "true" ? true : false;
            Tempset.CustomButtonLink = settingXml.Element("CustomButtonLink").Value;
            Tempset.CustomButtonText = settingXml.Element("CustomButtonText").Value;
            Tempset.InstagramButton = settingXml.Element("InstagramButton").Value == "true" ? true : false;
            Tempset.InstagramButtonLink = settingXml.Element("InstagramButtonLink").Value;
            Tempset.PanelButton = settingXml.Element("PanelButton").Value == "true" ? true : false;
            Tempset.PanelButtonText = settingXml.Element("PanelButtonText").Value;
            Tempset.RedirectToInstagram = settingXml.Element("RedirectToInstagram").Value == "true" ? true : false;
            Tempset.RedirectToTelegram = settingXml.Element("RedirectToTelegram").Value == "true" ? true : false;
            Tempset.RedirectToUrl = settingXml.Element("RedirectToUrl").Value;
            Tempset.RegisterButton = settingXml.Element("RegisterButton").Value == "true" ? true : false;
            Tempset.RegisterButtonText = settingXml.Element("RegisterButtonText").Value;
            Tempset.ShowTrialButton = settingXml.Element("ShowTrialButton").Value == "true" ? true : false;
            Tempset.TelegramButton = settingXml.Element("TelegramButton").Value == "true" ? true : false;
            Tempset.TelegramButtonLink = settingXml.Element("TelegramButtonLink").Value;
            Tempset.Redirect = settingXml.Element("Redirect").Value == "true" ? true : false;
            Tempset.PanelButtonLink = Url.Action(MVC.Login.Client("", user.UserRouter.RouterCode), protocol: "https");
            Tempset.RegisterButtonLink = Url.Action(MVC.Register.Client(user.UserRouter.RouterCode), protocol: "https");

            return View(Tempset);

        }
    }
}