using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;

namespace Netotik.ViewModels.Common.State
{
    /// <summary>
    /// ViewModel For Add Or Edit
    /// </summary>
    public class StateModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(500, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "StateName")]
        [Remote("IsNameExist", "State", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsDefault")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool IsDefault { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Active")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool IsActive { get; set; }
    }
}
