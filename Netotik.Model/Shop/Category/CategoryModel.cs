using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;

namespace Netotik.ViewModels.Shop.Category
{
    public class CategoryModel
    {
        public int? Id { get; set; }
        [Display(ResourceType = typeof(Captions), Name = "Parent")]
        public int? ParentId { get; set; }
  
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        [Remote("IsCategoryNameExist", "ShopCategory", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "این نام قبلا در سیستم ثبت شده است.")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Description")]
        [UIHint("Multiline")]
        public string Description { get; set; }

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

        [Display(ResourceType = typeof(Captions), Name = "ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsPublish")]
        public bool IsPublished { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Order")]
        public int DisplayOrder { get; set; }


        [Display(ResourceType = typeof(Captions), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }


        public int[] DiscountIds { get; set; }
    }
}
