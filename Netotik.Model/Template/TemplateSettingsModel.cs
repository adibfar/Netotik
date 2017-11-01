using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Template
{
    public class TemplateSettings
    {
        public string id { get; set; }
        public bool RegisterButton { get; set; }
        public string RegisterButtonText { get; set; }
        public string RegisterButtonLink { get; set; }

        public bool PanelButton { get; set; }
        public string PanelButtonText { get; set; }
        public string PanelButtonLink { get; set; }

        public bool CustomButton { get; set; }
        public string CustomButtonLink { get; set; }
        public string CustomButtonText { get; set; }

        public bool TelegramButton { get; set; }
        public string TelegramButtonLink { get; set; }

        public bool InstagramButton { get; set; }
        public string InstagramButtonLink { get; set; }

        public bool Redirect { get; set; }
        public string RedirectToUrl { get; set; }
        public bool RedirectToTelegram { get; set; }
        public bool RedirectToInstagram { get; set; }

        public bool ShowTrialButton { get; set; }

        public bool AutoLogin { get; set; }
        public int? AutoLoginAfterSec { get; set; }
        public string AutoLoginUser { get; set; }
    }
}
