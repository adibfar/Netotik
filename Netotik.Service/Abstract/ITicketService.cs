using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Ticket.Issue;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ITicketService : IBaseService<Ticket>
    {
        IQueryable<TableIssueModel> GetContentTable(string search, long userId, string[] Roles);
    }
}
