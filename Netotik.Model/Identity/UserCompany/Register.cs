﻿using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class Register
    {
        public long? UserResellerId { get; set; }

        public bool? EmailConfirmed { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        //[System.Web.Mvc.Remote("IsPhoneNumberAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        //[System.Web.Mvc.Remote("IsCompanyNationalCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "کد ملی اشتباه است یا قبلا در سیستم ثبت شده است.")]
        public string NationalCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        //[Remote("IsEmailAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", ErrorMessage = "این ایمیل قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        public string Email { get; set; }


        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(5, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "RouterAddress")]
        public string R_Host { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "RouterUsername")]
        public string R_User { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(4, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "RouterPassword")]
        public string R_Password { get; set; }

        [Range(1, 65536, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RangeError")]
        [Display(ResourceType = typeof(Captions), Name = "ApiPort")]
        public int R_Port { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "IsCloudActive")]
        public bool cloud { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "UsermanCustomer")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Userman_Customer { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        [StringLength(256, MinimumLength = 5,ErrorMessageResourceType =typeof(Captions),ErrorMessageResourceName ="LengthError")]
        [System.Web.Mvc.Remote("IsUserNameAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, ErrorMessageResourceType =typeof(Captions),ErrorMessageResourceName = "ExistError", HttpMethod = "POST")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "JustEnglishNumeric")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(6, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Password")]
        [System.Web.Mvc.Remote("CheckPassword", "Remote", System.Web.Mvc.AreaReference.UseRoot, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "PasswordEasy", HttpMethod = "POST")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [Compare("Password", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ConfirmPasswordNotValid")]
        [Display(ResourceType = typeof(Captions), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(6, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "نام شرکت")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "انگلیسی وارد کنید")]
        [System.Web.Mvc.Remote("IsCompanyCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string CompanyCode { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ImageProfile")]
        public System.Web.HttpPostedFileBase ImageAvatar { get; set; }

    }
}
