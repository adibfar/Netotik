using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel;

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class MikrotikConfModel
    {
        public long Id { get; set; }

        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]{5,30}$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [Display(ResourceType = typeof(Captions), Name = "RouterAddress")]
        public string R_Host { get; set; }

        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]{5,30}$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [Display(ResourceType = typeof(Captions), Name = "RouterUsername")]
        public string R_User { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"(^$)|(^.{5,40}$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [Display(ResourceType = typeof(Captions), Name = "RouterPassword")]
        public string R_Password { get; set; }

        [Range(1, 65536, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RangeError")]
        [Display(ResourceType = typeof(Captions), Name = "ApiPort")]
        public int R_Port { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsCloudActive")]
        public bool cloud { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "UsermanCustomer")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]{4,30}$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Userman_Customer { get; set; }
    }
}
