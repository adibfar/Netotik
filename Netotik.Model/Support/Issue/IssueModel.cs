using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.Support.Issue
{
    public class IssueModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "عنوان کار را وارد کنید")]
        [MaxLength(300, ErrorMessage = "حدااکثر 300 کاراکتر")]
        [MinLength(2, ErrorMessage = "حداقل 2 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        [UIHint("SummerNoteEditor")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name="اهمیت کار")]
        public IssuePeriority Periority { get; set; }

        [Display(Name = "کاربران مسئول")]
        public long[] UserIds { get; set; }
        [Display(Name = "حوزه های مسئول")]
        public int[] RoleIds { get; set; }
        [Display(Name = "برچسب ها")]
        public int[] LabelIds { get; set; }
    }
}
