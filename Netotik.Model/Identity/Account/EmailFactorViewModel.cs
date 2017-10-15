using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.Account
{
    public class EmailFactorViewModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string ViewName { get; set; }
        public string CompanyName { get; set; }
        public long FactorId { get; set; }
        public string ServiceName { get; set; }
        public long Price { get; set; }
        public DateTime FactorDate { get; set; }

    }
}
