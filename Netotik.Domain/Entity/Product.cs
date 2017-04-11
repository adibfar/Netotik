using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Product
    {
        public Product()
        {
            this.OrderItems = new List<OrderItem>();
            this.ProductAttributeValues = new List<ProductAttributeValue>();
            this.Discounts = new List<Discount>();
            this.Pictures = new List<Picture>();
            this.ProductComments = new List<ProductComment>();
        }

        public int Id { get; set; }
        public int? ManufacturerId { get; set; }
        public int? TaxId { get; set; }
        public int? DeliveryDateId { get; set; }
        public bool IsFreeShipping { get; set; }
        public int Quantity { get; set; }
        public int MinQuantityNotify { get; set; }
        public bool IsMinQuantityNotify { get; set; }
        public bool DisplayQuantityForUser { get; set; }
        public int OrderMaximumQuantity { get; set; }
        public int CountView { get; set; }
        public bool AllowUserComment { get; set; }
        public bool AllowViewComments { get; set; }
        public int CommentCount { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
        public bool ShowOnHomePage { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public Nullable<int> PictureId { get; set; }
        public bool DisableBuyButton { get; set; }
        public bool CallForPrice { get; set; }
        public bool CanBuyIfNotInStock { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> MaxOffPrice { get; set; }
        public int Weight { get; set; }
        public Nullable<System.DateTime> AvailableStartDate { get; set; }
        public Nullable<System.DateTime> AvailableEndDate { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime EditDate { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual DeliveryDate DeliveryDate { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Tax Tax { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
    }
}
