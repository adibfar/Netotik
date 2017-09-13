using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.ComponentModel;
using System.Web;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class RegisterSettingModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        //[Display(ResourceType = typeof(Captions), Name = "Mobile")]
        [Display(Name = "SendEmailUserPass")]
        public bool SendEmailUserPass { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        //[Display(ResourceType = typeof(Captions), Name = "Mobile")]
        [Display( Name = "SendSmsUserPass")]
        public bool SendSmsUserPass { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display( Name = "Mobile")]
        public FieldType MobileNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        public FieldType Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display( Name = "Username")]
        public FieldType Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(Name = "Age")]
        public FieldType Age { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display( Name = "Password")]
        public FieldType Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(Name = "PasswordConfirm")]
        public FieldType PasswordConfirm { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(Name = "BirthDate")]
        public FieldType BirthDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public FieldType Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(Name = "IsMale")]
        public FieldType IsMale { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(Name = "NationalCode")]
        public FieldType NationalCode { get; set; }
    }
}
