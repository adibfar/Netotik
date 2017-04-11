using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ShippingByWeight
    {
        public int Id { get; set; }
        public int ShippingMethodId { get; set; }
        public Nullable<int> FromWeight { get; set; }
        public Nullable<int> ToWeight { get; set; }
        public decimal AdditionalFixedPrice { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
    }
}
