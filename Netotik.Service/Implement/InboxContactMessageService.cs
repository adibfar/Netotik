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
using Netotik.Model.Common.ContactUs;
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class InboxContactUsMessageService : BaseService<InboxContactUsMessage>, IInboxContactUsMessageService
    {
        public InboxContactUsMessageService(IUnitOfWork unit)
            : base(unit)
        {

        }


        public IList<MessageItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<InboxContactUsMessage> all = dbSet.AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.
                    Where(x => x.Name.Contains(model.sSearch) ||
                    x.PhoneNumber.Contains(model.sSearch) ||
                    x.Email.Contains(model.sSearch)).AsQueryable();
            }


            // Apply Sorting
            Func<InboxContactUsMessage, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.Name :
                                                            model.iSortCol_0 == 2 ? x.Email : x.PhoneNumber);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new MessageItem
                {
                    PhoneNumber = x.PhoneNumber,
                    Name = x.Name,
                    Email = x.Email,
                    Id = x.Id,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();

        }


    }
}
