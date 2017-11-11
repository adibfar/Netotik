using Netotik.Domain.Entity;
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
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public string Title { get; set; }

        [MaxLength(200, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Url")]
        public string Url { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Body")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [AllowHtml]
        public string Body { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "BodyOverview")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public string BodyOverview { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaKeywords")]
        [MaxLength(400, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        public string MetaKeywords { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaDescription")]
        public string MetaDescription { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaTitle")]
        [MaxLength(400, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        public string MetaTitle { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsPublish")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool IsPublished { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AllowComment")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool AllowComments { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "AllowViewComments")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool AllowViewComments { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "HasSideBar")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool HasSideBar { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DontShowBlog")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool DontShowBlog { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "DontShowImageDetail")]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        public bool DontShowImageDetail { get; set; }


        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Language")]
        public int LanguageId { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "StartDatePublish")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "StartTimePublish")]
        public string StartDateTime { get; set; }

        
        public Picture Picture { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }

        public IList<Netotik.Domain.Entity.ContentCategory> ContentCategories { get; set; }
        public IEnumerable<SelectListItem> ContentTages { get; set; }

        public int[] TagIds { get; set; }
        public int[] CategoryIds { get; set; }
    }
}
