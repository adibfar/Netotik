using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class UserAdmin
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
    }
}
