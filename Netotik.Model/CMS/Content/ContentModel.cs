using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Netotik.ViewModels.CMS.Content
{
    public class ContentModel 
    {
        public int? Id { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Body")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [UIHint("SummerNoteEditor")]
        [AllowHtml]
        public string Body { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "BodyOverview")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [UIHint("Multiline")]
        public string BodyOverview { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(ResourceType = typeof(Captions), Name = "StartDatePublish")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [UIHint("TimePicker")]
        [Display(ResourceType = typeof(Captions), Name = "StartTimePublish")]
        public TimeSpan? StartDateTime { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(ResourceType = typeof(Captions), Name = "EndDatePublish")]
        public Nullable<System.DateTime> EndDate { get; set; }

        [UIHint("TimePicker")]
        [Display(ResourceType = typeof(Captions), Name = "EndTimePublish")]
        public TimeSpan? EndDateTime { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaKeywords")]
        [MaxLength(400, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [UIHint("Multiline")]
        public string MetaKeywords { get; set; }

        [UIHint("Multiline")]
        [Display(ResourceType = typeof(Captions), Name = "MetaDescription")]
        public string MetaDescription { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaTitle")]
        [MaxLength(400, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [UIHint("Multiline")]
        public string MetaTitle { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsPublish")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public bool IsPublished { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AllowComment")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public bool AllowComments { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AllowViewComments")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        public bool AllowViewComments { get; set; }

        [UIHint("Multiline")]
        [Display(ResourceType = typeof(Captions), Name = "AdminComment")]
        public string AdminComment { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }

        public int[] TagIds { get; set; }
        public int[] CategoryIds { get; set; }
    }
}
