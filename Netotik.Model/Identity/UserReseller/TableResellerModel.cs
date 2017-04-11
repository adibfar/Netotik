using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.UserReseller
{
    public class TableResellerModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string PersonCode { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int UserCount { get; set; }
        public int PackageCount { get; set; }
        public bool IsActive { get; set; }
    }
}
