using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Factor
    {
        public Factor()
        {
        }

        public long Id { get; set; }
        public FactorType FactorType { get; set; }
        public FactorStatus FactorStatus { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? RegisterPay { get; set; }
        public string GetId { get; set; }
        public string TransactionId { get; set; }
        public string IpAddress { get; set; }
        public long PaymentPrice { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual FactorSmsDetail FactorSmsDetail { get; set; }
    }
    public enum FactorStatus : short
    {

        Success = 0,
        Unpaid,
        Fail
    }

    public enum FactorType : short
    {
        CompanyBySmsPackage = 0
    }
}
