using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.CMS.Slider
{
    public class SliderModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Language")]
        public int LanguageId { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Url")]
        public string Url { get; set; }
        [Display(ResourceType = typeof(Captions),Name="DisplayOrder")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public int Order { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Active")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool IsActive { get; set; }

    }
}
