using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.Account
{
    public class EmailClientUserPassViewModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string ViewName { get; set; }
        public string RouterCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Profile { get; set; }
        public string PanelLoginLink { get; set; }

    }
}
