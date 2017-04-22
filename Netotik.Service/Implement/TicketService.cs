using Netotik.Common.Security;
using Netotik.Data;
using Netotik.Domain.Entity;
using Netotik.Services.Abstract;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PersianDate;
using Netotik.Common;
using Netotik.ViewModels.Ticket.Issue;
using Netotik.ViewModels.Identity.Security;

namespace Netotik.Services.Implement
{
    public class TicketService : BaseService<Ticket>, ITicketService
    {
        public TicketService(IUnitOfWork unit)
            : base(unit)
        {

        }


        public IQueryable<TableIssueModel> GetContentTable(string search, long userId, string[] Roles)
        {

            var issues = dbSet.AsNoTracking()
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (Roles.Any(x => x == AssignableToRolePermissions.CanViewAllIssue))
            {

            }
            else if (Roles.Any(x => x == AssignableToRolePermissions.CanAccessIssue))
            {
                issues = issues.Where(x => x.CreatedUserId == userId);
            }
            else
            {
                issues = issues.Where(x => 1 == 0);
            }


            if (!string.IsNullOrWhiteSpace(search))
                issues = issues.Where(x => x.Title.Contains(search)).AsQueryable();

            return issues.Select(x => new TableIssueModel
            {
                Id = x.Id,
                Title = x.Title,
                Status = x.status,
                LastUserResponse = x.LastResponseUser.FirstName + " " + x.LastResponseUser.LastName,
                LastResponse = x.LastResponseDate,
                CreateDate = x.CreateDate
            }).AsQueryable();
        }

    }
}
