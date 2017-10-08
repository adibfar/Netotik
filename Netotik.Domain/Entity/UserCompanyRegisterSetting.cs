using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public class UserCompanyRegisterSetting
    {
        public UserCompanyRegisterSetting()
        {
        }
        public long Id { get; set; }
        public FieldType MobileNumber { get; set; }
        public FieldType Email { get; set; }
        public FieldType Username { get; set; }
        public FieldType Age { get; set; }
        public FieldType Password { get; set; }
        public FieldType PasswordConfirm { get; set; }
        public FieldType BirthDate { get; set; }
        public FieldType Name { get; set; }
        public FieldType IsMale { get; set; }
        public FieldType NationalCode { get; set; }



        public bool SendEmailUserPass { get; set; }
        

        public virtual UserCompany UserCompany { get; set; }
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

}
