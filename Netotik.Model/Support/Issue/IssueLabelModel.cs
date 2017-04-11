using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;

namespace Netotik.ViewModels.Support.Issue
{
    public class IssueLabelModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "نام برچسب را وارد کنید")]
        [MaxLength(300, ErrorMessage = "حدااکثر 300 کاراکتر")]
        [MinLength(2, ErrorMessage = "حداقل 2 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "رنگ برچسب را وارد کنید")]
        [Display(Name = "رنگ برچسب")]
        public string Color { get; set; }
    }
}
