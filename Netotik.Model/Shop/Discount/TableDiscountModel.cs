using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.Shop.Discount
{
    public class TableDiscountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimesUse { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DiscountType DiscountType { get; set; }

        public string DiscountTypeName
        {
            get
            {
                switch (DiscountType)
                {
                    case DiscountType.ManufacturerDiscount:
                        return "تخفیف برندها";
                    case DiscountType.CategoryDiscount:
                        return "تخفیف دسته ها";
                    case DiscountType.ProductDiscount:
                        return "تخفیف محصولات";
                    case DiscountType.OrderDiscount:
                        return "تخفیف فاکتور";
                    default:
                        return "";
                }

            }
        }
    }
}
