using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.SmsPackage
{
    public class SmsPackageModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions),Name="DisplayOrder")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public int Order { get; set; }

        //[Display(ResourceType = typeof(Captions), Name = "DisplayOrder")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public int SmsCount { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Price")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public long Price { get; set; }


        //[Display(ResourceType = typeof(Captions), Name = "UnitPrice")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public long UnitPrice { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Active")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool IsActive { get; set; }

    }
}
