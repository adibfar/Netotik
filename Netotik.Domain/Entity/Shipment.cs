using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Shipment
    {
        public Shipment()
        {
            this.ShipmentItems = new List<ShipmentItem>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public Nullable<long> TotalWeight { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string AdminComment { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}
