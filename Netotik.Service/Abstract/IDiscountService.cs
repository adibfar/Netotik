using Netotik.Domain.Entity;
using Netotik.ViewModels.Shop.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IDiscountService : IBaseService<Discount>
    {
        IQueryable<TableDiscountModel> GetDataTable(string search);

        Task<decimal> GetCouponDiscount(decimal totalPrice, string coupon);
        Task<decimal> GetDsicountFactorAsync(decimal factorPrice);
        IList<Discount> GetDiscountesbyIds(int[] ids);
    }
}
