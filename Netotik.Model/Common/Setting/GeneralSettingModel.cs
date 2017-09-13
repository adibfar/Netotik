using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Common.Setting
{
    public class GeneralSettingModel
    {
        #region General
        [Display(ResourceType = typeof(Captions), Name = "HomePageTitle")]
        public string HomePageTitle { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "HomePageDescription")]
        public string HomePageDescription { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "HomePageKeywords")]
        public string HomePageKeywords { get; set; }

        #endregion

        #region Company Info
        [Display(ResourceType = typeof(Captions), Name = "CompanyName")]
        public string CompanyName { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Address")]
        public string CompanyAddress { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Tel")]
        public string CompanyTel1 { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Tel")]
        public string CompanyTel2 { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        public string CompanyEmail1 { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        public string CompanyEmail2 { get; set; }
        #endregion

        #region Sms Api
        [Display(ResourceType = typeof(Captions), Name = "SmsNumber")]
        public string SmsNumber { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "SmsUsername")]
        public string SmsUsername { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "SmsPassword")]
        public string SmsPassword { get; set; }
        #endregion


        #region Social Networks

        [Display(ResourceType = typeof(Captions), Name = "Facebook")]
        public string Facebook { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Twitter")]
        public string Twitter { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Instagram")]
        public string Instagram { get; set; }
        [Display(Name = "GooglePlus")]
        public string GooglePlus { get; set; }

        #endregion
    }
}
