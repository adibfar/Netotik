using Netotik.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.UserReseller
{
    public class RegisterViewModel
    {
        
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(6, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "نام شرکت")]
        [RegularExpression(@"^[a-zA-Z1-9]+$", ErrorMessage = "انگلیسی وارد کنید")]
        [System.Web.Mvc.Remote("IsResellerCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string ResellerCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotValidError")]
        //[System.Web.Mvc.Remote("IsPhoneNumberAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ExistError")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(10, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinLengthError")]
        [Display(Name = "کد ملی")]
        //[System.Web.Mvc.Remote("IsNationalCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "این کد ملی قبلا ثبت شده است.")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل را به شکل صحیح وارد کنید")]
        [DisplayName("ایمیل")]
        [StringLength(256, ErrorMessage = "حداکثر طول ایمیل 256 حرف است")]
        //[Remote("IsEmailAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, ErrorMessage = "این ایمیل قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        public string Email { get; set; }

        [Required(ErrorMessage = "گذرواژه را وارد کنید")]
        [StringLength(50, ErrorMessage = "گذرواژه نباید کمتر از 5 حرف و بیتشر از 50 حرف باشد", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DisplayName("گذروژه")]
        [Remote("CheckPassword", "Remote", System.Web.Mvc.AreaReference.UseRoot, ErrorMessage = "این گذرواژه به راحتی قابل تشخیص است", HttpMethod = "POST")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار گذرواژه را وارد کنید")]
        [DataType(DataType.Password)]
        [DisplayName("تکرار گذرواژه")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "گذرواژه های وارد شده مطابقت ندارند")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [DisplayName("نام کاربری")]
        [StringLength(256, ErrorMessage = "نام کاربری نباید کمتر از 5 حرف و بیتشر از 256 حرف باشد", MinimumLength = 5)]
        [Remote("IsUserNameAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, ErrorMessage = "این نام کاربری قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "فقط از حروف انگلیسی و اعداد استفاده کنید")]
        public string UserName { get; set; }

    }
}