using Netotik.Domain.Entity;
using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.CMS.Advertise
{
    public class AdvertiseModel
    {
        public int? Id { get; set; }
        [Display(Name = "متن تصویر (alt)")]
        public string Alt { get; set; }
        public string Url { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Order")]
        public int Order { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
        public Picture Picture { get; set; }
    }
}
