using Netotik.Common;
using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.Services.Enums;
using Netotik.ViewModels.Common.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface ILocaleStringResourceService : IBaseService<Netotik.Domain.Entity.LocaleStringResource>
    {
        IList<ResourceItem> GetList(RequestListModel model, int LanguageId, out long TotalCount, out long ShowCount);
    }
}
