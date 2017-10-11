using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.Shop.Factor
{
    public class FactorUserItem
    {
        public long Id { get; set; }
        public long RowNumber { get; set; }
        public FactorType FactorType { get; set; }
        public FactorStatus FactorStatus { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? RegisterPay { get; set; }
        public string GetId { get; set; }
        public string TransactionId { get; set; }
        public string IpAddress { get; set; }
        public long PaymentPrice { get; set; }

    }
}
