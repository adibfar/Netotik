using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.Shop.Order
{
    public class TableOrderModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string buyerName { get; set; }
        public decimal PaymentPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public string OrderStatusName
        {
            get
            {
                switch (OrderStatus)
                {
                    case OrderStatus.WaitForProcess:
                        return "در انتظار تایید";
                    case OrderStatus.WaitForSend:
                        return "در انتظار ارسال";
                    case OrderStatus.Send:
                        return "ارسال شده";
                    case OrderStatus.Returned:
                        return "مرجوع شده";
                    case OrderStatus.canceled:
                        return "لغو شده";
                    default:
                        return "";
                }

            }
        }
    }
}
