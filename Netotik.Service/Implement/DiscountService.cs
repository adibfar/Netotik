using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PersianDate;
using Netotik.Common;
using Netotik.ViewModels.Shop.Discount;
using Netotik.ViewModels;

namespace Netotik.Services.Implement
{
    public class DiscountService : BaseService<Discount>, IDiscountService
    {
        public DiscountService(IUnitOfWork unit)
            : base(unit)
        {

        }



        public IQueryable<TableDiscountModel> GetDataTable(string search)
        {
            IQueryable<TableDiscountModel> selected = dbSet.Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Name)
                .Include(x => x.DiscountUsageHistories)
                .AsNoTracking()
                                            .Select(x => new TableDiscountModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                DiscountType = x.DiscountType,
                                                EndDate = x.EndDate,
                                                StartDate = x.StartDate,
                                                TimesUse = x.DiscountUsageHistories.Count
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selected = selected.Where(x => x.Name.Contains(search)).AsQueryable();

            return selected;
        }

        public IList<Discount> GetDiscountesbyIds(int[] ids)
        {
            var discountes = dbSet.ToList();
            IList<Discount> list = new List<Discount>();
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    var t = discountes.FirstOrDefault(x => x.Id == item);
                    if (t != null)
                        list.Add(t);
                }
            }
            return list;
        }

        private decimal calculatePercentagePrice(decimal price, int percentage)
        {
            return price * ((decimal)percentage / (decimal)100);
        }

        public async Task<decimal> GetDsicountFactorAsync(decimal factorPrice)
        {
            var date = DateTime.Now;
            var date1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            var date2 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            var list = await dbSet
                            .AsNoTracking()
                            .Where(x =>
                                !x.IsDeleted
                                && x.DiscountType == DiscountType.OrderDiscount
                                && (x.MaximumDiscountQuantity.HasValue ? x.DiscountUsageHistories.Count < x.MaximumDiscountQuantity : true)
                                && (x.StartDate.HasValue ? x.StartDate.Value <= date1 : true)
                                && (x.EndDate.HasValue ? x.EndDate.Value >= date2 : true))
                            .ToListAsync();

            decimal off = 0;
            foreach (var item in list)
            {
                if (item.UsePercentage && item.DiscountPercentage.HasValue)
                {
                    decimal offItem = calculatePercentagePrice(factorPrice, item.DiscountPercentage.Value);
                    if (item.MaximumDiscountAmount.HasValue ? offItem <= item.MaximumDiscountAmount.Value : true)
                        off += offItem;
                    else
                        off += item.MaximumDiscountAmount.Value;
                }
                else if (item.DiscountAmount.HasValue)
                {
                    off += item.DiscountAmount.Value;
                }
            }

            return off;

        }


        public async Task<decimal> GetCouponDiscount(decimal totalPrice,string coupon)
        {
            var date = DateTime.Now;
            var date1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            var date2 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            decimal off = 0;
            var discount = await dbSet
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                !x.IsDeleted
                                && x.CouponCode == coupon
                                && x.DiscountType == DiscountType.OrderDiscount
                                && x.RequiersCouponCode
                                && (x.MaximumDiscountQuantity.HasValue ? x.DiscountUsageHistories.Count < x.MaximumDiscountQuantity : true)
                                && (x.StartDate.HasValue ? x.StartDate.Value <= date1 : true)
                                && (x.EndDate.HasValue ? x.EndDate.Value >= date2 : true));

            if (discount!=null)
            {
                if (discount.UsePercentage && discount.DiscountPercentage.HasValue)
                {
                    decimal offItem = calculatePercentagePrice(totalPrice, discount.DiscountPercentage.Value);
                    if (discount.MaximumDiscountAmount.HasValue ? offItem <= discount.MaximumDiscountAmount.Value : true)
                        off += offItem;
                    else
                        off += discount.MaximumDiscountAmount.Value;
                }
                else if (discount.DiscountAmount.HasValue)
                {
                    off += discount.DiscountAmount.Value;
                }
            }


            return off;
        }
    }
}
