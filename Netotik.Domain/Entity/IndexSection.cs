using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class IndexSection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Html { get; set; }
        public int Order { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
