using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            this.Factores = new List<Factor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> PictureId { get; set; }
        public string Description { get; set; }
        public string GateWayUrl { get; set; }
        public string MerchantId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDelete { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual Picture Picture{ get; set; }

        public virtual ICollection<Factor> Factores { get; set; }
    }
}
