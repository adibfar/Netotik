using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Shop.ProductGalleryPanel;
using Netotik.ViewModels.Shop.ProductGalleryAdmin;

namespace Netotik.Services.Abstract
{
    public interface IProductGalleryService : IBaseService<ProductGallery>
    {
        IEnumerable<BoxProductGalleryModel> GetForPublicView(out int total,int? categoryId,int? ManufacturerId, int page, int pageSize, string search);
        List<BoxProductGalleryModel> GetRelativeProduct(int size, int[] categoryIds, int productId);
        Task<ProductGallery> GetByNameAsync(string name);
        IQueryable<TableProductGalleryModel> GetDataTable(string search);
        Task Remove(int id);
        Task<ProductGallery> SingleOrDefaultAsync(int primaryKey);
    }
}
