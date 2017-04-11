using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Domain.Entity
{
    public class InboxMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateDate{ get; set; }
    }
}
