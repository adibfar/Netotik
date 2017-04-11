using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.ProductAttributeValue;

namespace Netotik.Services.Abstract
{
    public interface IProductAttributeValueService : IBaseService<ProductAttributeValue>
    {
        void AddOrUpdateProductAttributes(IEnumerable<ProductAttributeValueModel> values);
    }
}
