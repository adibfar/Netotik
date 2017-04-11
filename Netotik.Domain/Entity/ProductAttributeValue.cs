using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ProductAttributeValue
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public string Value { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}
