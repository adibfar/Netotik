using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Picture
    {
        public Picture()
        {
            this.Contents = new List<Content>();
            this.ImageGalleryItems = new List<ImageGalleryItem>();
            this.Users = new List<User>();
            this.Sliders = new List<Slider>();
            this.PaymentTypes = new List<PaymentType>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string OrginalName { get; set; }
        public string MimeType { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<ImageGalleryItem> ImageGalleryItems { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Slider> Sliders { get; set; }
        public virtual ICollection<PaymentType> PaymentTypes { get; set; }


    }
}
