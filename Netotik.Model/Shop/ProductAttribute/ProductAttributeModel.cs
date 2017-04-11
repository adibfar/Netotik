using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;

namespace Netotik.ViewModels.Shop.ProductAttribute
{
    public class ProductAttributeModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "حدااکثر 100 کاراکتر")]
        [MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        [UIHint("Multiline")]
        public string Description { get; set; }

    }
}
