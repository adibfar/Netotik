using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.Common.Language
{
    public class LanguageXmlModel
    {
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "XmlResource")]
        public HttpPostedFileBase ResourcesXml { get; set; }
    }
}
