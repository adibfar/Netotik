using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public partial class Discount
    {
        public Discount()
        {
            this.DiscountRequirements = new List<DiscountRequirement>();
            this.DiscountUsageHistories = new List<DiscountUsageHistory>();
            this.Categories = new List<Category>();
            this.Manufacturers = new List<Manufacturer>();
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DiscountType DiscountType { get; set; }
        public bool UsePercentage { get; set; }
        public int? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public Nullable<decimal> MaximumDiscountAmount { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public bool RequiersCouponCode { get; set; }
        public string CouponCode { get; set; }
        public int DiscountLimitationType { get; set; }
        public int? MaximumDiscountQuantity { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<DiscountRequirement> DiscountRequirements { get; set; }
        public virtual ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }



    public enum DiscountType : short
    {
        [Display(Name = "تخفیف برندها")]
        ManufacturerDiscount = 1,
        [Display(Name = "تخفیف دسته ها")]
        CategoryDiscount = 2,
        [Display(Name = "تخفیف محصولات")]
        ProductDiscount = 3,
        [Display(Name = "تخفیف فاکتورها")]
        OrderDiscount = 4
    }
}
