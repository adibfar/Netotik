using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.Role
{
    public class RoleModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        [Remote("IsRoleNameAvailable", "Role", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "IsDefaultRoleRegisteredUser")]
        public bool IsDefaultForRegister { get; set; }

        public string[] PermissionNames { get; set; }
    }
}
