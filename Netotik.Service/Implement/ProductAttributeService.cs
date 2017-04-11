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
using Netotik.ViewModels.Shop.ProductAttribute;
using Netotik.ViewModels.Shop.ProductAttributeValue;

namespace Netotik.Services.Implement
{
    public class ProductAttributeService : BaseService<ProductAttribute>, IProductAttributeService
    {
        private readonly IProductService _productService;
        public ProductAttributeService(IUnitOfWork unit, IProductService productService)
            : base(unit)
        {
            _productService = productService;
        }


        public IQueryable<TableProductAttributeModel> GetDataTable(string search)
        {
            IQueryable<TableProductAttributeModel> selected = dbSet.Include(x => x.Category).AsNoTracking()
                .OrderByDescending(x => x.CategoryId)
                                            .Select(x => new TableProductAttributeModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                CategoryId = x.CategoryId,
                                                CategoryName = x.Category.Name,
                                                Description = x.Description,
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selected = selected.Where(x => x.Name.Contains(search)).AsQueryable();

            return selected;
        }
        public async Task Remove(int id)
        {
            var _Role = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_Role != null)
                Remove(_Role);
        }



        public IEnumerable<ProductAttributeValueModel> GetAttributesByProductId(int productId)
        {
            var prod = _productService.SingleOrDefault(productId);
            if (prod == null) return null;

            return dbSet//.Where(x => x.CategoryId == prod.CategoryId)
                    .Include(x => x.ProductAttributeValues)
                    .Select(x => new ProductAttributeValueModel
                    {
                        FieldName = x.Name,
                        ProductId = productId,
                        ProductAttributeId = x.Id,
                        Value =
                        x.ProductAttributeValues.Any(y => y.ProductId == productId && y.ProductAttributeId == x.Id)
                        ?
                        x.ProductAttributeValues.FirstOrDefault(y => y.ProductId == productId && y.ProductAttributeId == x.Id).Value :""

                    });
        }




    }
}
