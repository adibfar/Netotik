using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.Permisson
{
    public class PermissionModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SectionPermisson Section { get; set; }
    }

    public enum SectionPermisson : short
    {
        User = 0,
        Role,
        Permisson,
        AdminPanel,
        CmsContentCategory,
        CmsContent,
        CmsTag,
        CmsComments,
        Configuration,
        ConfigurationContactUs,
        ConfigurationMenu,
        ProductCategory,
        DeliveryDate,
        Discount,
        Manufactur,
        PaymentType,
        ProductAttribute,
        ProductComments,
        ShopShippingByWeight,
        ShopShippingByMethod,
        Tax,
        WareHouse,
        Product,
        ProductGallery,
        Factor,
        ProductColor,
        ProductSize,
        Ticket
    }
}
