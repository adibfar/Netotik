using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public class Role : IdentityRole<long, UserRole>
    {
        public string Description { get; set; }
        public virtual bool IsSystemRole { get; set; }
        public virtual bool IsDefaultForRegister { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual string Permissions { get; set; }
        public string SecurityStamp { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
