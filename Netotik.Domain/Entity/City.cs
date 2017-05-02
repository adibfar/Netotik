using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class City
    {
        public City()
        {
            this.Addresses = new List<Address>();
        }

        public int Id { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual State State { get; set; }
    }

}
