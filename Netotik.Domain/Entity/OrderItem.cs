using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class OrderItem
    {
        public OrderItem()
        {
            this.ReturnRequests = new List<ReturnRequest>();
            this.ShipmentItems = new List<ShipmentItem>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PeymentPrice { get; set; }
        public decimal UnitTaxPrice { get; set; }
        public decimal UnitDiscountPrice { get; set; }
        public bool IsFreeShipping { get; set; }
        public long UnitWeight { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ReturnRequest> ReturnRequests { get; set; }
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}
