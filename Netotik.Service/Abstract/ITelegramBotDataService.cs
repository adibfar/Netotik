﻿using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.Model.Common.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ITelegramBotDataService : IBaseService<TelegramBotData>
    {
        IList<TelegramBotData> GetList(long CompanyId, long ChatID);
    }
}