using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Netotik.ViewModels.Shop.ProductAttributeValue;

namespace Netotik.Services.Implement
{
    public class ProductAttributeValueService : BaseService<ProductAttributeValue>, IProductAttributeValueService
    {
        public ProductAttributeValueService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public void AddOrUpdateProductAttributes(IEnumerable<ProductAttributeValueModel> values)
        {
            if (values == null) return;
            if (!values.Any()) return;
            var productId = values.FirstOrDefault().ProductId;
            var prodAttributes = dbSet.Where(x => x.ProductId == productId).ToList();

            List<ProductAttributeValue> ProdValues = new List<ProductAttributeValue>();
            int counter = 0;
            foreach (var item in values)
            {
                var value = prodAttributes.FirstOrDefault(x => x.ProductId == item.ProductId && x.ProductAttributeId == item.ProductAttributeId);
                if (value != null)
                {
                    prodAttributes.ElementAt(counter).Value = item.Value;
                }
                else
                {
                    dbSet.Add(new ProductAttributeValue { Value = item.Value, ProductId = item.ProductId, ProductAttributeId = item.ProductAttributeId });
                }
                counter++;
            }
        }
    }
}
