using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ITelegramBotDataService : IBaseService<TelegramBotData>
    {
        IList<TelegramBotData> GetList(long RouterId, long ChatID);
    }
}
