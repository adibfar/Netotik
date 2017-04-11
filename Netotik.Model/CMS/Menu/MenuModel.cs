using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.ComponentModel;

namespace Netotik.ViewModels.CMS.Menu
{
    public class MenuModel
    {
        public int? Id { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Parent")]
        public int? ParentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(30, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Url")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public string Url { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        public string Description { get; set; }

        [DisplayName("آیکن")]
        public string Icon { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Order")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public int Order { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Active")]
        public bool IsActive { get; set; }
    }
}
