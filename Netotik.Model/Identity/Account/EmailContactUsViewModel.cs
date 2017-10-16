using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.Account
{
    public class EmailContactUsViewModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
        public string ViewName { get; set; }


        public string ContactUsMessage { get; set; }
        public string ContactUsEmail { get; set; }
        public string ContactUsName { get; set; }
        public string ContactUsPhoneNumber { get; set; }
        public DateTime ContactUsCreateDate { get; set; }
    }
}
