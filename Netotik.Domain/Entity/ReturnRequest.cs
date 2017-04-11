using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ReturnRequest
    {
        public int Id { get; set; }
        public int OrderItemId { get; set; }
        public long UserId { get; set; }
        public int Quantity { get; set; }
        public string ReasonForReturn { get; set; }
        public string UserComment { get; set; }
        public string StaffComment { get; set; }
        public int ReturnRequestStatus { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime EditDate { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        public virtual User User { get; set; }
    }
}
