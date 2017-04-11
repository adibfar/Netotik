using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            this.OrderPayments = new List<OrderPayment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> PictureId { get; set; }
        public string Description { get; set; }
        public string GateWayUrl { get; set; }
        public long TerminalId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime EditDate { get; set; }
        public virtual Picture Picture{ get; set; }
        public virtual ICollection<OrderPayment> OrderPayments { get; set; }
    }
}
