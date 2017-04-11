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

namespace Netotik.Services.Implement
{
    public class ImageGalleryItemService : BaseService<ImageGalleryItem>, IImageGalleryItemService
    {
        public ImageGalleryItemService(IUnitOfWork unit)
            : base(unit)
        {

        }


        public async Task<IList<ImageGalleryItem>> GetByGalleryId(int galleryId)
        {
            return await dbSet.Where(x => x.ContentId == galleryId).ToListAsync();
        }

        public async Task Remove(int id)
        {
            var _Role = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_Role != null)
                Remove(_Role);
        }
    }
}
