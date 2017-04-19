using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class State
    {
        public State()
        {
            this.Addresses = new List<Address>();
            this.Cities = new List<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<Warehouse> WareHoses { get; set; }
    }
}
