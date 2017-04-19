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
using Netotik.ViewModels.ShopPublic;
using Netotik.ViewModels.Shop.Order;

namespace Netotik.Services.Implement
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IDiscountService _discountService;
        private readonly IShippingMethodService _shippingMethodService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IStateService _addressStateService;
        private readonly ICityService _addressCityService;
        private readonly IProductService _productSrevice;

        public OrderService(IUnitOfWork unit,
                            IDiscountService discountService,
                            IProductService productService,
                            IPaymentTypeService paymentTypeService,
                            IShippingMethodService shippingMethodService,
                            ICityService addressCityService,
                            IStateService addressStateService)
            : base(unit)
        {
            _productSrevice = productService;
            _discountService = discountService;
            _paymentTypeService = paymentTypeService;
            _shippingMethodService = shippingMethodService;
            _addressCityService = addressCityService;
            _addressStateService = addressStateService;
        }


        public async Task<OrderDetail> GetFactorDetailAsync(FactorModel model, List<ShoppingCartModel> cart)
        {
            var state = await _addressStateService.All().FirstOrDefaultAsync(x => x.IsActive && x.Id == model.StateId);
            var city = await _addressCityService.All().FirstOrDefaultAsync(x => x.IsActive && x.Id == model.CityId);
            var shipping = await _shippingMethodService.All().FirstOrDefaultAsync(x => x.IsActive && !x.IsDelete && x.Id == model.ShipingMethodId);
            var payment = await _paymentTypeService.All().FirstOrDefaultAsync(x => x.IsActive && x.Id == model.PaymentTypeId);
            List<ShoppingCartFactorModel> shopingCartList = await _productSrevice.GetForFactorByIdsAsync(cart);


            var FactorDetail = new OrderDetail();
            FactorDetail.Address = model.Address;
            FactorDetail.Description = model.Description;
            FactorDetail.Email = model.Email;
            FactorDetail.Name = model.Name;
            FactorDetail.MobileNumber = model.MobileNumber;
            FactorDetail.PostalCode = model.PostalCode;
            FactorDetail.StateId = model.StateId;
            FactorDetail.StateName = state.Name;
            FactorDetail.CityId = model.CityId;
            FactorDetail.CityName = city.Name;
            FactorDetail.ShipingMethodId = model.ShipingMethodId.Value;
            FactorDetail.ShippingMethodName = shipping.Name;
            FactorDetail.PaymentTypeId = model.PaymentTypeId.Value;
            FactorDetail.PaymentTypeName = payment.Name;
            FactorDetail.Coupon = model.coupon;


            FactorDetail.Cart = shopingCartList;


            FactorDetail.TotalPrice = shopingCartList.Sum(x => x.UnitPrice * x.Count);
            FactorDetail.ShippingPrice = _productSrevice.CalculateShipmentPrice(cart, shipping.Id);
            FactorDetail.ProductDiscountPrice = shopingCartList.Sum(x => x.UnitOffPrice * x.Count);
            FactorDetail.CouponDiscountPrice = await _discountService.GetCouponDiscount(FactorDetail.TotalPrice, model.coupon); ;
            FactorDetail.FactorDiscountPrice = await _discountService.GetDsicountFactorAsync(FactorDetail.TotalPrice);
            FactorDetail.TaxPrice = shopingCartList.Sum(x => x.UnitTaxPrice * x.Count);

            FactorDetail.PaymentPrice =
                FactorDetail.TotalPrice -
                FactorDetail.ProductDiscountPrice -
                FactorDetail.CouponDiscountPrice -
                FactorDetail.FactorDiscountPrice +
                FactorDetail.TaxPrice +
                FactorDetail.ShippingPrice;



            return FactorDetail;
        }


        public async Task<Order> RegisterFactorAsync(FactorModel model, List<ShoppingCartModel> cart, string Ip, BuyerType buyerType)
        {
            var state = await _addressStateService.All().FirstOrDefaultAsync(x => x.IsActive && x.Id == model.StateId);
            var city = await _addressCityService.All().FirstOrDefaultAsync(x => x.IsActive && x.Id == model.CityId);
            var shipping = await _shippingMethodService.All().FirstOrDefaultAsync(x => x.IsActive && !x.IsDelete && x.Id == model.ShipingMethodId);
            var payment = await _paymentTypeService.All().FirstOrDefaultAsync(x => x.IsActive && x.Id == model.PaymentTypeId);
            List<ShoppingCartFactorModel> shopingCartList = await _productSrevice.GetForFactorByIdsAsync(cart);

            var address = new Address();
            address.AddressCityId = city.Id;
            address.CreateDate = DateTime.Now;
            address.Email = model.Email;
            address.MobileNumber = model.MobileNumber;
            address.PostalCode = model.PostalCode;
            address.Name = model.Name;
            address.AddressDescription = model.Address;

            var order = new Order
            {
                BuyerType = buyerType,
                OrderStatus = OrderStatus.WaitForProcess,
                IPAddress = Ip,
                PaymentStatus = PaymentStatus.Fail,
                Description = model.Description,
                Address = address,
                CreateDate = DateTime.Now
            };


            foreach (var item in shopingCartList)
            {
                var orderItem = new OrderItem();
                orderItem.ProductId = item.Id;
                orderItem.IsFreeShipping = item.IsFreeShipping;
                orderItem.Quantity = item.Count;
                orderItem.UnitWeight = item.weight;
                orderItem.UnitPrice = item.UnitPrice;
                orderItem.UnitDiscountPrice = item.UnitOffPrice;
                orderItem.UnitTaxPrice = item.UnitTaxPrice;
                orderItem.PeymentPrice = (item.UnitPrice * item.Count) - (item.UnitOffPrice * item.Count) + (item.UnitTaxPrice * item.Count);
                order.OrderItems.Add(orderItem);
                order.ShippingMethod = shipping;
                order.ShippingMethodId = shipping.Id;
            }


            order.TotalPrice = order.OrderItems.Sum(x => x.UnitPrice * x.Quantity);
            order.TotalShippingPrice = _productSrevice.CalculateShipmentPrice(cart, shipping.Id);
            order.TotalProductDiscountPrice = order.OrderItems.Sum(x => x.UnitDiscountPrice * x.Quantity);
            order.TotalFactorCouponDiscountPrice = await _discountService.GetCouponDiscount(order.TotalPrice, model.coupon);
            order.CouponText = order.TotalFactorCouponDiscountPrice > 0 ? model.coupon : string.Empty;
            order.TotalFactorDiscountPrice = await _discountService.GetDsicountFactorAsync(order.TotalPrice);
            order.TotalTaxPrice = order.OrderItems.Sum(x => x.UnitTaxPrice * x.Quantity);

            order.PaymentPrice =
                order.TotalPrice -
                order.TotalProductDiscountPrice -
                order.TotalFactorCouponDiscountPrice -
                order.TotalFactorDiscountPrice +
                order.TotalTaxPrice +
                order.TotalShippingPrice;


            var orderPayment = new OrderPayment();
            orderPayment.Amont = order.PaymentPrice;
            orderPayment.CreateDate = DateTime.Now;
            orderPayment.PaymentType = payment;
            orderPayment.PaymentTypeId = payment.Id;
            orderPayment.IsSucess = false;
            order.OrderPayments.Add(orderPayment);



            dbSet.Add(order);
            await UnitOfWork.SaveChangesAsync();

            return order;
        }

        public IQueryable<TableOrderModel> GetOrderDataTable(string search)
        {
            var list = dbSet
                .AsNoTracking()
                .Include(x => x.Address)
                .Where(x => x.PaymentStatus == PaymentStatus.Success)
                .Select(x => new TableOrderModel
                {
                    buyerName = x.Address.Name,
                    CreateDate = x.CreateDate,
                    Id = x.Id,
                    OrderStatus = x.OrderStatus,
                    PaymentPrice = x.PaymentPrice
                });

            if (!string.IsNullOrEmpty(search)) list = list.Where(x => x.buyerName.Contains(search));

            return list.OrderBy(x => x.CreateDate).AsQueryable();
        }

        public IQueryable<TableOrderModel> GetOrderNotPaymentDataTable(string search)
        {
            var list = dbSet
                .AsNoTracking()
                .Include(x => x.Address)
                .Where(x => x.PaymentStatus != PaymentStatus.Success)
                .Select(x => new TableOrderModel
                {
                    buyerName = x.Address.Name,
                    CreateDate = x.CreateDate,
                    Id = x.Id,
                    OrderStatus = x.OrderStatus,
                    PaymentPrice = x.PaymentPrice
                });

            if (!string.IsNullOrEmpty(search)) list = list.Where(x => x.buyerName.Contains(search));

            return list.OrderBy(x => x.CreateDate).AsQueryable();
        }
    }
}
