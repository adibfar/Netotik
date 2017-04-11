using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ProductAttribute
    {
        public ProductAttribute()
        {
            this.ProductAttributeValues = new List<ProductAttributeValue>();
        }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
    }
}
