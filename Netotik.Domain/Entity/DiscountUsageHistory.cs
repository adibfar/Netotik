using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class DiscountUsageHistory
    {
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public int OrderId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual Order Order { get; set; }
    }
}
