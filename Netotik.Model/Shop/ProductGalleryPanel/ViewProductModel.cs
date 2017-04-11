using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;

namespace Netotik.ViewModels.Shop.ProductGalleryPanel
{
    public class ViewProductGalleryModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(ResourceType = typeof(Captions), Name = "Manufacturer")]
        public int? ManufacturerId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(200, ErrorMessage = "حدااکثر 100 کاراکتر")]
        [MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "BodyOverview")]
        [UIHint("Multiline")]
        public string ShortDescription { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        [UIHint("SummerNoteEditor")]
        [AllowHtml]
        public string FullDescription { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaKeywords")]
        [MaxLength(400, ErrorMessage = "حدااکثر 400 کاراکتر")]
        [UIHint("Multiline")]
        public string MetaKeywords { get; set; }

        [UIHint("Multiline")]
        [Display(ResourceType = typeof(Captions), Name = "MetaDescription")]
        public string MetaDescription { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MetaTitle")]
        [MaxLength(400, ErrorMessage = "حدااکثر 400 کاراکتر")]
        [UIHint("Multiline")]
        public string MetaTitle { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }

    }
}
