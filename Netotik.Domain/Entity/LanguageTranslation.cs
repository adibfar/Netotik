using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Domain.Entity
{
    public class LanguageTranslation
    {
        public int Id { get; set; }
        public string EntityId { get; set; }
        public string ObjectName { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
