using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Permisson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object Roles { get; set; }
    }
}
