using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ImageGalleryItem
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int PictureId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual Content Content { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
