using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Identity.UserCompany
{
    public class CompanyEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "CompanyName")]
        [RegularExpression(@"(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "JustEnglishNumeric")]
        [System.Web.Mvc.Remote("IsCompanyCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string CompanyName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(2, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        [RegularExpression(@"^0?9\d{9}$", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [System.Web.Mvc.Remote("IsCompanyPhoneNumberAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string MobileNumber { get; set; }


        [Required(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        [System.Web.Mvc.Remote("IsCompanyEmailAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "ExistError")]
        public string Email { get; set; }

        [MaxLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "PostalCode")]
        public string PostalCode { get; set; }

        [MaxLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(10, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        [System.Web.Mvc.Remote("IsCompanyNationalCodeAvailable", "Remote", System.Web.Mvc.AreaReference.UseRoot, AdditionalFields = "Id", HttpMethod = "POST", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string PersonCode { get; set; }


        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        public string Username { get; set; }

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

        public int Reseller_Id { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MaxLengthError")]
        [MinLength(3, ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "MinLengthError")]
        [Display(ResourceType = typeof(Captions), Name = "UsermanCustomer")]
        [RegularExpression(@"(^$)|(^[a-zA-Z0-9:.-_]*$)", ErrorMessageResourceType = typeof(Captions), ErrorMessageResourceName = "NotValidError")]
        public string Userman_Customer { get; set; }
    }
}
