using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class DiscountRequirement
    {
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public string DiscountRequirementRuleSystemName { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
