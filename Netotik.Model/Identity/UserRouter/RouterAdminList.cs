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
    public class RouterAdminList
    {
        public long Id { get; set; }
        public long RowNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string ResellerName { get; set; }
        public long ResellerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageFileName { get; set; }
        public string LastLoginDate { get; set; }
        public bool IsBanned { get; set; }
    }
}
