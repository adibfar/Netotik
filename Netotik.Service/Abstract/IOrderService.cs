using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.ShopPublic;
using Netotik.ViewModels.Shop.Order;

namespace Netotik.Services.Abstract
{
    public interface IOrderService : IBaseService<Order>
    {
        IQueryable<TableOrderModel> GetOrderDataTable(string search);
        IQueryable<TableOrderModel> GetOrderNotPaymentDataTable(string search);
        Task<OrderDetail> GetFactorDetailAsync(FactorModel model, List<ShoppingCartModel> cart);
        Task<Order> RegisterFactorAsync(FactorModel model, List<ShoppingCartModel> cart, string Ip, BuyerType buyerType);

    }
}
