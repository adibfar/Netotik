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
    public class TelegramBotDataService : BaseService<TelegramBotData>, ITelegramBotDataService
    {
        public TelegramBotDataService(IUnitOfWork unit)
            : base(unit)
        {

        }


        public IList<TelegramBotData> GetList(long CompanyId, long ChatID)
        {
            return dbSet.AsQueryable().Where(
                x => x.CompanyId == CompanyId
                && x.ChatID == ChatID).ToList().Where(x => DateTime.Compare(x.MessageDate.AddMinutes(20), DateTime.Now) > 0).ToList();
        }


    }
}
