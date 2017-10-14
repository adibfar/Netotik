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
        public short FactorType { get; set; }
        public short FactorStatus { get; set; }
        public string RegisterDate { get; set; }
        public string TransactionId { get; set; }
        public string PaymentPrice { get; set; }

    }
}
