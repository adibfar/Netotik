using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.ProductAdmin;
using Netotik.ViewModels.Shop.ProductPanel;
using Netotik.ViewModels;
using Netotik.ViewModels.ShopPublic;

namespace Netotik.Services.Abstract
{
    public interface IProductService : IBaseService<Product>
    {
        IEnumerable<BoxProductModel> GetForPublicView(out int total,int? categoryId,int? ManufacturerId, int page, int pageSize, string search);
        List<BoxProductModel> GetSpecialProduct(int size);
        List<BoxProductModel> GetRelativeProduct(int size, int[] categoryIds, int productId);
        List<BoxProductModel> GetLastProduct(int size);

        Task<List<ShoppingCartFactorModel>> GetForFactorByIdsAsync(List<ShoppingCartModel> list);

        Task<bool> ExistsByNameAsync(string name, int? id);

        Task<Product> GetByNameAsync(string name);
        IQueryable<TableProductModel> GetDataTable(string search);

        decimal CalculateOff(Product prod);
        Task Remove(int id);

        Task<Product> SingleOrDefaultAsync(int primaryKey);
    }
}
