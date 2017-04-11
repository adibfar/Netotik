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

namespace Netotik.Services.Implement
{
    public class OrderPaymentService : BaseService<OrderPayment>, IOrderPaymentService
    {
        public OrderPaymentService(IUnitOfWork unit)
            : base(unit)
        {
        }



        public OrderPayment GetById(int id)
        {
            return dbSet
                .Include(x => x.Order)
                .Include(x => x.Order.Address)
                .Include(x => x.Order.Address.AddressCity)
                .Include(x => x.Order.Address.AddressCity.State)
                .Include(x => x.PaymentType)
                .Include(x=>x.Order.ShippingMethod)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
