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
        public string Message { get; set; }
        public string From { get; set; }
        public string Url { get; set; }
        public string UrlText { get; set; }
        public string ViewName { get; set; }
        public string CompanyName { get; set; }
        public int FactorNum { get; set; }
        public string ServiceName { get; set; }
        public int Price { get; set; }
        public DateTime FactorDate { get; set; }

    }
}
