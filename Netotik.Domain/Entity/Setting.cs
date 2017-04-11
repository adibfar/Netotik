using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
