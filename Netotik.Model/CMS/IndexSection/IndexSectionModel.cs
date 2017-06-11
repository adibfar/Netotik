using Netotik.Domain.Entity;
using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Netotik.ViewModels.CMS.IndexSection
{
    public class IndexSectionModel
    {
        public int? Id { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Body")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [AllowHtml]
        public string Html { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "DisplayOrder")]
        public int Order { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }
    }
}
