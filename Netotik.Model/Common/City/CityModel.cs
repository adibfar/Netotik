using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;

namespace Netotik.ViewModels.Common.City
{
    public class CityModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(500, ErrorMessageResourceType =typeof(Messages),ErrorMessageResourceName="MaxLengthError" )]
        [MinLength(2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "State")]
        public int StateId { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsDefault")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public bool IsDefault { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Active")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public bool IsActive { get; set; }
    }
}
