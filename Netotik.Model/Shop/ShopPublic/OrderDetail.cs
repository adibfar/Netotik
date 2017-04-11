using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;

namespace Netotik.ViewModels.ShopPublic
{
    public class OrderDetail
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public long PostalCode { get; set; }
        public string Email { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int ShipingMethodId { get; set; }
        public string ShippingMethodName{ get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public decimal FactorDiscountPrice { get; set; }
        public decimal CouponDiscountPrice { get; set; }
        public string Coupon { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TaxPrice { get; set; }
        public decimal PaymentPrice { get; set; }
        public IList<ShoppingCartFactorModel> Cart { get; set; }
    }
}
