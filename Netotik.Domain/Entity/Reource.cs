using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Domain.Entity
{
    public class Resource
    {
        public int Id { get; set; }
        public string Culture { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
