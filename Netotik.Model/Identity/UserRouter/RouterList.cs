using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel;

namespace Netotik.ViewModels.Identity.UserRouter
{
    public class RouterList
    {
        public long Id { get; set; }

        public bool IsBanned { get; set; }
        public long? UserResellerId { get; set; }

        public bool? EmailConfirmed { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Name")]
        public string FirstName { get; set; }
        
        [Display(ResourceType = typeof(Captions), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "MobileNumber")]
        public string PhoneNumber { get; set; }
        
        [Display(ResourceType = typeof(Captions), Name = "NationalCode")]
        public string NationalCode { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "Email")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "RouterAddress")]
        public string R_Host { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "RouterUsername")]
        public string R_User { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Captions), Name = "RouterPassword")]
        public string R_Password { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ApiPort")]
        public int R_Port { get; set; }
        
        [Display(ResourceType = typeof(Captions), Name = "IsCloudActive")]
        public bool cloud { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "UsermanCustomer")]
        public string Userman_Customer { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "UserName")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "RouterName")]
        public string RouterCode { get; set; }

        [Display(ResourceType = typeof(Captions), Name = "ImageProfile")]
        public string ImageAvatar { get; set; }
    }
}
