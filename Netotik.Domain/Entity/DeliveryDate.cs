using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class DeliveryDate
    {
        public DeliveryDate()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; set; }

        public bool IsDelete { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
