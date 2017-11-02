using Netotik.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Template
{
    public class TemplateSettings
    {
        public string id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "RegisterButton")]
        public bool RegisterButton { get; set; }
        public string RegisterButtonText { get; set; }
        public string RegisterButtonLink { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "PanelButton")]
        public bool PanelButton { get; set; }
        public string PanelButtonText { get; set; }
        public string PanelButtonLink { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "CustomButton")]
        public bool CustomButton { get; set; }
        public string CustomButtonLink { get; set; }
        public string CustomButtonText { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "TelegramButton")]
        public bool TelegramButton { get; set; }
        public string TelegramButtonLink { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "InstagramButton")]
        public bool InstagramButton { get; set; }
        public string InstagramButtonLink { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Redirect")]
        public bool Redirect { get; set; }
        public string RedirectToUrl { get; set; }
        public bool RedirectToTelegram { get; set; }
        public bool RedirectToInstagram { get; set; }
        public string RedirectCheckBokValue { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ShowTrialButton")]
        public bool ShowTrialButton { get; set; }

        public bool AutoLogin { get; set; }
        public int? AutoLoginAfterSec { get; set; }
        public string AutoLoginUser { get; set; }
    }
}
