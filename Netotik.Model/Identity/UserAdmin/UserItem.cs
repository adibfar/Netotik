using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Identity.UserAdmin
{
    public class UserItem
    {
        public long Id { get; set; }
        public long RowNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageFileName { get; set; }
        public string LastLoginDate { get; set; }
        public bool IsBanned { get; set; }

    }
}
