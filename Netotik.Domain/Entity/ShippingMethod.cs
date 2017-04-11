using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ShippingMethod
    {
        public ShippingMethod()
        {
            this.ShippingByWeights = new List<ShippingByWeight>();
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDefault { get; set; }
        public bool PriceAfterRecive { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int? PictureId { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual ICollection<ShippingByWeight> ShippingByWeights { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
