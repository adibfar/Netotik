using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Address
    {
        public Address()
        {
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Nullable<int> AddressCityId { get; set; }
        public string AddressDescription { get; set; }
        public long PostalCode { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual City AddressCity { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual User User { get; set; }
    }
}
