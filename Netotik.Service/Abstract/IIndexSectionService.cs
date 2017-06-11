using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.ViewModels.CMS.IndexSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IIndexSectionService : IBaseService<IndexSection>
    {
        IList<IndexSectionItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);
        IList<IndexSection> GetAll(int languageId);
    }
}
