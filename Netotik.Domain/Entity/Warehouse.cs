using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            this.ShipmentItems = new List<ShipmentItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AddressStateId { get; set; }
        public int AddressCityId { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public virtual State AddressState { get; set; }
        public virtual City AddressCity { get; set; }
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}
