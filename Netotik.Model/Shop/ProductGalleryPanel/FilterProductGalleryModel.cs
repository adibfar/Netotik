using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Shop.ProductGalleryPanel
{
    public class FilterProductGalleryModel
    {
        public IEnumerable<Category.TableCategoryModel> Categories;
        public IEnumerable<Manufacturer.TableManufacturerModel> Brands;

    }
}
