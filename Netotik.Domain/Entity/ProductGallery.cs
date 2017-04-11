using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class ProductGallery
    {
        public ProductGallery()
        {
            this.Categories = new List<Category>();
            this.Pictures = new List<Picture>();
        }

        public int Id { get; set; }
        public int? ManufacturerId { get; set; }
        public int CountView { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public Nullable<int> PictureId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime EditDate { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
