using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Tax
    {
        public Tax()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
