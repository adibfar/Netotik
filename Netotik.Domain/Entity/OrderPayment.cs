using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class OrderPayment
    {
        public int Id { get; set; }
        public int PaymentTypeId { get; set; }
        public int OrderId { get; set; }
        public long? UserId { get; set; }
        public decimal Amont { get; set; }
        public string TransactionId { get; set; }
        public string GetId { get; set; }
        public string Result { get; set; }
        public bool IsSucess { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual Order Order { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual User User { get; set; }
    }
}
