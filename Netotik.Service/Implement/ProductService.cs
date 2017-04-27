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
using Netotik.ViewModels.Shop.ProductAdmin;
using Netotik.ViewModels.Shop.ProductPanel;
using Netotik.ViewModels;
using Netotik.ViewModels.ShopPublic;
using EntityFramework.Extensions;

namespace Netotik.Services.Implement
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork unit)
            : base(unit)
        {
        }
        public async Task<bool> ExistsByNameAsync(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(name) && x.Id != id.Value);
            return await dbSet.AnyAsync(x => x.Name.Equals(name));
        }


        public async Task<Product> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey);
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await dbSet.SingleAsync(x => x.Name.Equals(name));
        }


        public IQueryable<TableProductModel> GetDataTable(string search)
        {
            IQueryable<TableProductModel> selected = dbSet.Where(x => !x.IsDeleted).Include(x => x.Pictures).Include(x => x.Categories)
                .OrderByDescending(x => x.Name)
                                            .Select(x => new TableProductModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                CategoryName = "اصلاح شود",//x.Category.Name,
                                                CommentCount = x.CommentCount,
                                                PictureCount = x.Pictures.Count,
                                                Price = x.Price ?? 0,
                                                ViewCount = x.CountView,
                                                imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selected = selected.Where(x => x.Name.Contains(search)).AsQueryable();

            return selected;
        }
        public async Task Remove(int id)
        {
            var _data = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_data != null)
                Remove(_data);
        }


        public List<BoxProductModel> GetLastProduct(int size)
        {
            return dbSet.Where(x => x.IsDeleted == false && x.IsPublished).Include(x => x.Categories)
                .OrderBy(x => x.AvailableStartDate)
                .Select(x => new BoxProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    //PriceOff = CalculateOff(x),
                    imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
                }).Take(size).ToList();
        }

        public List<BoxProductModel> GetSpecialProduct(int size)
        {
            return dbSet.Where(x => x.IsDeleted == false && x.ShowOnHomePage && x.IsPublished)
                .Include(x => x.Categories)
                .Include(x => x.Manufacturer.Discounts)
                .Include(x => x.Discounts)
                .OrderBy(x => x.AvailableStartDate)
                .Take(size).ToList()

                .Select(x => new BoxProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    //PriceOff = CalculateOff(x),
                    Price = x.Price,
                    imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
                }).ToList();
        }

        public decimal CalculateOff(Product prod)
        {
            decimal sumOffPrice = 0;
            if (!prod.Price.HasValue || prod.Price <= 1) return sumOffPrice;

            var date = DateTime.Now;
            var date1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            var date2 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            if (prod.Discounts.Any())
            {
                foreach (var item in prod.Discounts.Where(x => !x.IsDeleted))
                {
                    if (item.MaximumDiscountQuantity.HasValue && item.DiscountUsageHistories.Count >= item.MaximumDiscountQuantity.Value) continue;
                    if (item.StartDate.HasValue && DateTime.Compare(item.StartDate.Value, date1) > 0) continue;
                    if (item.EndDate.HasValue && DateTime.Compare(item.EndDate.Value, date2) < 0) continue;
                    if (item.UsePercentage && item.DiscountPercentage.HasValue)
                        sumOffPrice += prod.Price.Value * ((decimal)item.DiscountPercentage.Value / (decimal)100);
                    else if (!item.UsePercentage && item.DiscountAmount.HasValue)
                        sumOffPrice += item.DiscountAmount.Value;
                }
            }
            if (prod.Manufacturer.Discounts.Any())
            {
                foreach (var item in prod.Manufacturer.Discounts.Where(x => !x.IsDeleted))
                {
                    if (item.MaximumDiscountQuantity.HasValue && item.DiscountUsageHistories.Count >= item.MaximumDiscountQuantity.Value) continue;
                    if (item.StartDate.HasValue && DateTime.Compare(item.StartDate.Value, date1) > 0) continue;
                    if (item.EndDate.HasValue && DateTime.Compare(item.EndDate.Value, date2) < 0) continue;
                    if (item.UsePercentage && item.DiscountPercentage.HasValue)
                        sumOffPrice += prod.Price.Value * ((decimal)item.DiscountPercentage.Value / (decimal)100);
                    else if (!item.UsePercentage && item.DiscountAmount.HasValue)
                        sumOffPrice += item.DiscountAmount.Value;
                }
            }
            var CategoryDiscountList = new List<Discount>();
            prod.Categories.ToList().ForEach(x => CategoryDiscountList.AddRange(x.Discounts));
            if (CategoryDiscountList.Any())
            {
                foreach (var item in CategoryDiscountList.Distinct())
                {
                    if (item.MaximumDiscountQuantity.HasValue && item.DiscountUsageHistories.Count >= item.MaximumDiscountQuantity.Value) continue;
                    if (item.StartDate.HasValue && DateTime.Compare(item.StartDate.Value, date1) > 0) continue;
                    if (item.EndDate.HasValue && DateTime.Compare(item.EndDate.Value, date2) < 0) continue;
                    if (item.UsePercentage && item.DiscountPercentage.HasValue)
                        sumOffPrice += prod.Price.Value * ((decimal)item.DiscountPercentage.Value / (decimal)100);
                    else if (!item.UsePercentage && item.DiscountAmount.HasValue)
                        sumOffPrice += item.DiscountAmount.Value;
                }
            }

            if (prod.MaxOffPrice.HasValue && sumOffPrice > prod.MaxOffPrice.Value)
                return prod.MaxOffPrice.Value;
            return sumOffPrice;
        }


        public IEnumerable<BoxProductModel> GetForPublicView(out int total, int? categoryId, int? ManufacturerId, int page, int pageSize, string search)
        {

            var date = DateTime.Now;
            var date1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            var date2 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);


            var list = dbSet
                .Where(x => !x.IsDeleted && x.IsPublished)
                .Where(x => (x.AvailableStartDate.HasValue ? x.AvailableStartDate.Value <= date1 : true) &&
                            (x.AvailableEndDate.HasValue ? x.AvailableEndDate.Value >= date2 : true))
                .Include(x => x.OrderItems)
                .Include(x => x.Picture)
                .AsQueryable();

            if (categoryId.HasValue) list = list.Where(x => x.Categories.Any(y => y.Id == categoryId.Value)).AsQueryable();
            if (ManufacturerId.HasValue) list = list.Where(x => x.ManufacturerId == ManufacturerId.Value).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) list = list.Where(x => x.Name.Contains(search)).AsQueryable();

            var totalQuery = list.FutureCount();
            var result = list.OrderBy(x => x.AvailableStartDate).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new BoxProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                //PriceOff = CalculateOff(x),
                imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
            }).AsQueryable();

            total = totalQuery.Value;
            return result.ToList();
        }


        public List<BoxProductModel> GetRelativeProduct(int size, int[] categoryIds, int productId)
        {
            DateTime date = DateTime.Now;
            return dbSet
                .Where(x => (x.AvailableStartDate.HasValue ? x.AvailableStartDate.Value <= date : true) &&
                            (x.AvailableEndDate.HasValue ? x.AvailableEndDate.Value >= date : true) &&
                            x.IsDeleted == false && x.IsPublished && x.Id != productId &&
                            x.Categories.Any(y => categoryIds.Any(z => z == y.Id)))
                 .OrderBy(x => x.AvailableStartDate)
                 .Select(x => new BoxProductModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Price = x.Price,
                     //PriceOff = CalculateOff(x),
                     imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
                 }).Take(size).ToList();
        }


        private decimal calculatePercentagePrice(decimal price, int percentage)
        {
            return price * ((decimal)percentage / (decimal)100);
        }

        public async Task<List<ShoppingCartFactorModel>> GetForFactorByIdsAsync(List<ShoppingCartModel> list)
        {
            var ids = list.Select(z => z.Id).ToArray();

            var products = await dbSet
                .AsNoTracking()
                .Where(x => x.Price.HasValue && !x.IsDeleted && x.IsPublished && !x.CallForPrice && !x.DisableBuyButton)
                .Include(x => x.Picture)
                .Include(x => x.Manufacturer.Discounts)
                .Include(x => x.Categories)
                .Include(x => x.Discounts)
                .Where(x => x.Price.HasValue && ids.Any(y => y == x.Id))
                .ToListAsync();

            var result = new List<ShoppingCartFactorModel>();

            foreach (var item in list)
            {
                var prod = products.FirstOrDefault(x => x.Id == item.Id);

                result.Add(
                new ShoppingCartFactorModel
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    Manfacturer = prod.Manufacturer.Name,
                    Category = "",
                    //UnitTaxPrice = prod.TaxId.HasValue ? calculatePercentagePrice(prod.Price.Value - CalculateOff(prod), prod.Tax.Percentage) : 0,
                    imgName = prod.PictureId.HasValue ? prod.Picture.FileName : "",
                    IsFreeShipping = prod.IsFreeShipping,
                    weight = prod.Weight,
                    UnitPrice = prod.Price.Value,
                    UnitOffPrice = CalculateOff(prod),
                    Count = item.Count,
                });
            }
            return result;
        }
        

    }
}
