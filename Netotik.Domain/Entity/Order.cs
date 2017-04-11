using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class Order
    {
        public Order()
        {
            this.DiscountUsageHistories = new List<DiscountUsageHistory>();
            this.OrderItems = new List<OrderItem>();
            this.OrderPayments = new List<OrderPayment>();
            this.Shipments = new List<Shipment>();
        }

        public int Id { get; set; }
        public int AddressId { get; set; }
        public int ShippingMethodId { get; set; }
        public long? UserId { get; set; }
        public BuyerType BuyerType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string IPAddress { get; set; }
        public string CouponText { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PaymentPrice { get; set; }
        public decimal TotalProductDiscountPrice { get; set; }
        public decimal TotalFactorDiscountPrice { get; set; }
        public decimal TotalFactorCouponDiscountPrice { get; set; }
        public decimal TotalTaxPrice { get; set; }
        public decimal TotalShippingPrice { get; set; }
        public string GetId { get; set; }
        public string TransactionId { get; set; }
        public string Description { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderPayment> OrderPayments { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public enum BuyerType : short
    {
        user = 0,
        geust = 1
    }


    public enum OrderStatus : short
    {
        WaitForProcess = 0,
        WaitForSend,
        Send,
        Returned,
        canceled
    }

    public enum PaymentStatus : short
    {
        Registred = 0,
        Success = 1,
        Fail = 2
    }

}
