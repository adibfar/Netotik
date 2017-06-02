using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class UserAdmin
    {
        public int Id { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string Website { get; set; }

        public string ShortBio { get; set; }
        public virtual User User { get; set; }
    }
}
