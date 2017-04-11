using Netotik.ViewModels.Identity.Permisson;
using System;
using System.Collections.Generic;

namespace Netotik.Services.Identity
{
    public  class PermissionRecord
    {
        public string RoleName { get; set; }
        public IEnumerable<PermissionModel> Permissions { get; set; }
    }
}
