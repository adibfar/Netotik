using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Netotik.Domain.Entity
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            this.Products = new List<Product>();
            this.Discounts = new List<Discount>();
        }

        public int Id { get; set; }
        [DisplayName("نام")]
        public string Url { get; set; }
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("عنوان متا")]
        public string MetaTitle { get; set; }
        [DisplayName("کلمات متا")]
        public string MetaKeywords { get; set; }
        [DisplayName("توضیحات متا")]
        public string MetaDescription { get; set; }
        public Nullable<int> PictureId { get; set; }
        [DisplayName("نمایش در صفحه اصلی")]
        public bool ShowOnHomePage { get; set; }
        [DisplayName("متشر شده")]
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("ترتیب نمایش")]
        public int DisplayOrder { get; set; }
        [DisplayName("تاریخ ایجاد")]
        public System.DateTime CreateDate { get; set; }
        [DisplayName("تاریخ آخرین ویرایش")]
        public System.DateTime EditDate { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
    }
}
