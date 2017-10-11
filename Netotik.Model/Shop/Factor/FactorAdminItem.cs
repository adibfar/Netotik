using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Shop.PaymentType
{
    public class PaymentTypeItem
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string GateWayUrl { get; set; }
        public string MerchantId { get; set; }
        public string Description { get; set; }
        public string imgName { get; set; }

    }
}
