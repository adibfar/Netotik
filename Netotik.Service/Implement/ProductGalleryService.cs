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
using EntityFramework.Extensions;
using Netotik.ViewModels.Shop.ProductGalleryAdmin;
using Netotik.ViewModels.Shop.ProductGalleryPanel;

namespace Netotik.Services.Implement
{
    public class ProductGalleryService : BaseService<ProductGallery>, IProductGalleryService
    {
        public ProductGalleryService(IUnitOfWork unit)
            : base(unit)
        {
        }

        public async Task<ProductGallery> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey);
        }

        public async Task<ProductGallery> GetByNameAsync(string name)
        {
            return await dbSet.SingleAsync(x => x.Name.Equals(name));
        }


        public IQueryable<TableProductGalleryModel> GetDataTable(string search)
        {
            IQueryable<TableProductGalleryModel> selected = dbSet.Where(x => !x.IsDeleted).Include(x => x.Pictures).Include(x => x.Categories)
                .OrderByDescending(x => x.Name)
                                            .Select(x => new TableProductGalleryModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                CategoryName = "اصلاح شود",//x.Category.Name,
                                                PictureCount = x.Pictures.Count,
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

        public IEnumerable<BoxProductGalleryModel> GetForPublicView(out int total, int? categoryId, int? ManufacturerId, int page, int pageSize, string search)
        {

            var list = dbSet
                .Where(x => !x.IsDeleted)
                .Include(x => x.Picture)
                .AsQueryable();

            if (categoryId.HasValue) list = list.Where(x => x.Categories.Any(y => y.Id == categoryId.Value)).AsQueryable();
            if (ManufacturerId.HasValue) list = list.Where(x => x.ManufacturerId == ManufacturerId.Value).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) list = list.Where(x => x.Name.Contains(search)).AsQueryable();

            var totalQuery = list.FutureCount();

            var result = list.OrderBy(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new BoxProductGalleryModel
            {
                Id = x.Id,
                Name = x.Name,
                imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
            }).AsQueryable();

            total = totalQuery.Value;
            return result.ToList();
        }


        public List<BoxProductGalleryModel> GetRelativeProduct(int size, int[] categoryIds, int productId)
        {
            DateTime date = DateTime.Now;
            return dbSet
                .Where(x => x.IsDeleted == false && x.Id != productId &&
                            x.Categories.Any(y => categoryIds.Any(z => z == y.Id)))
                 .OrderBy(x => x.CreateDate)
                 .Select(x => new BoxProductGalleryModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     imgName = (x.PictureId.HasValue) ? x.Picture.FileName : ""
                 }).Take(size).ToList();
        }
    }
}
