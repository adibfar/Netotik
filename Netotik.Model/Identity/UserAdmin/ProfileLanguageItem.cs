using Netotik.Domain.Entity;
using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.Identity.UserAdmin
{
    public class ProfileLanguageItem
    {
        public Language Language { get; set; }
        public string ShowName { get; set; }
        public string ShortBio { get; set; }
    }
}
