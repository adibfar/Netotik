using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.ViewModels;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IOrderPaymentService : IBaseService<OrderPayment>
    {
        OrderPayment GetById(int id);
    }
}
