using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.ViewModels.Identity.Role
{
    public class RoleViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual bool IsActive { get; set; }
        public bool IsSystemRole { get; set; }
        public bool IsDefaultForRegister { get; set; }
    }
}
