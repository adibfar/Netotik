using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Domain.Entity
{
    public class FactorSmsDetail
    {
        public int Id { get; set; }
        public int SmsCount { get; set; }
        public long UnitPrice { get; set; }
        public long TotalPrice { get; set; }
        public Factor Factor { get; set; }
    }
}
