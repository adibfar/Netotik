using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.Shop.Tax
{
    public class TaxModel
    {
        public int? Id { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Range(1, 100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RangeError")]
        [Display(ResourceType = typeof(Captions), Name = "TaxPercent")]
        public int Percent { get; set; }
    }
}
