using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Domain.Entity
{
    public class SmsPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SmsCount { get; set; }
        public int Order { get; set; }
        public long Price { get; set; }
        public bool IsActive { get; set; }
    }
}
