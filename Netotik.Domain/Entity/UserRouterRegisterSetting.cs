using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public class UserRouterRegisterSetting
    {
        public UserRouterRegisterSetting()
        {
        }
        public long Id { get; set; }
        public FieldType MobileNumber { get; set; }
        public FieldType Email { get; set; }
        public UsernameFieldType Username { get; set; }
        public FieldType Age { get; set; }
        public PasswordFieldType Password { get; set; }
        public FieldType PasswordConfirm { get; set; }
        public FieldType BirthDate { get; set; }
        public FieldType Name { get; set; }
        public FieldType Sex { get; set; }
        public FieldType NationalCode { get; set; }
        public bool SendEmailUserPass { get; set; }
        

        public virtual UserRouter UserRouter { get; set; }
    }
    public enum FieldType : short
    {
        [Display(ResourceType = typeof(Captions), Name = "NoneType")]
        None = 0,
        [Display(ResourceType = typeof(Captions), Name = "ShowType")]
        Show = 1,
        [Display(ResourceType = typeof(Captions), Name = "RequiredType")]
        Required
    }

    public enum UsernameFieldType : short
    {
        [Display(ResourceType = typeof(Captions), Name = "NoneType")]
        None = 0,
        [Display(ResourceType = typeof(Captions), Name = "RequiredType")]
        Required,
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        MobileNumber,
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        NationalCode,
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        Email,
    }
    public enum PasswordFieldType : short
    {
        [Display(ResourceType = typeof(Captions), Name = "NoneType")]
        None = 0,
        [Display(ResourceType = typeof(Captions), Name = "RequiredType")]
        Required,
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        MobileNumber,
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        NationalCode,
    }

}
