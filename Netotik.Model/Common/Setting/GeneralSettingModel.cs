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
        [Display(ResourceType = typeof(Captions), Name = "SiteName")]
        public string SiteName { get; set; }
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
        [Display(ResourceType = typeof(Captions), Name = "Email2")]
        public string CompanyEmail1 { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Email2")]
        public string CompanyEmail2 { get; set; }
        #endregion

        #region Social Networks

        [Display(Name = "فیس بوک")]
        public string Facebook { get; set; }
        [Display(Name = "توییتر")]
        public string Twitter { get; set; }
        [Display(Name = "اینستاگرام")]
        public string Instagram { get; set; }
        [Display(Name = "گوگل پلاس")]
        public string GooglePlus { get; set; }

        #endregion
    }
}
