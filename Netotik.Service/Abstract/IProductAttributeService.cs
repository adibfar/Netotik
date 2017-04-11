using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.ProductAttribute;
using Netotik.ViewModels.Shop.ProductAttributeValue;

namespace Netotik.Services.Abstract
{
    public interface IProductAttributeService : IBaseService<ProductAttribute>
    {

        IQueryable<TableProductAttributeModel> GetDataTable(string search);
        IEnumerable<ProductAttributeValueModel> GetAttributesByProductId(int productId);
        Task Remove(int id);
    }
}
