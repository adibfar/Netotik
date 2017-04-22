using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Netotik.Domain.Entity
{
    public partial class TicketTag
    {
        public TicketTag()
        {
            this.Tickets = new List<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }




    }

}
