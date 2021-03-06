using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Common.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IInboxContactUsMessageService : IBaseService<InboxContactUsMessage>
    {
        IList<MessageItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);
    }
}
