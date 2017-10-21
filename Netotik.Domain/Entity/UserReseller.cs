using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class UserReseller
    {
        public UserReseller()
        {
            this.UserCompanies = new List<UserRouter>();
        }

        public long Id { get; set; }
        public string ResellerCode { get; set; }
        public string Address { get; set; }
        public long PostalCode { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<UserRouter> UserCompanies { get; set; }
        public virtual User User { get; set; }
    }
}
