using Netotik.Common;
using Netotik.Domain.Entity;
using Netotik.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{
    public interface IContentTagService : IBaseService<ContentTag>
    {
        Task<ContentTag> SingleOrDefaultAsync(int primaryKey);

        Task<bool> IsExistByName(string name,int? id);

        IList<ContentTag> GetTagesbyIdsAsync(int[] ids);
    }
}
