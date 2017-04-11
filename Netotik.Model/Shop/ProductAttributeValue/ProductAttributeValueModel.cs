using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;

namespace Netotik.ViewModels.Shop.ProductAttributeValue
{
    public class ProductAttributeValueModel
    {
        public int ProductId { get; set; }
        public string FieldName { get; set; }
        public int ProductAttributeId { get; set; }
        public string Value { get; set; }

       
    }
}
