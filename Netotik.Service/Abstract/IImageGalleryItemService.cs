using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Netotik.Services.Abstract
{
    public interface IImageGalleryItemService : IBaseService<ImageGalleryItem>
    {
        Task<IList<ImageGalleryItem>> GetByGalleryId(int galleryId);

        Task Remove(int id);
    }
}
