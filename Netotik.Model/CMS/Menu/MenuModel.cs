using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.ComponentModel;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.CMS.Menu
{
    public class MenuModel
    {
        public int? Id { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Parent")]
        public int? ParentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Language")]
        public int LanguageId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Location")]
        public MenuLocation MenuLocation { get; set; }


        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(30, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Text { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Url")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public string Url { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Icon")]
        public string Icon { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DisplayOrder")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public int Order { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Active")]
        public bool IsActive { get; set; }
    }
}
