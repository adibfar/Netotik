using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Link
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
    }
}
