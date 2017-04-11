using DnmaGroups.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnmaGroups.Service.Abstract
{
    public interface IPermissonService : IBaseService<Permisson>
    {
        Task<IList<Permisson>> GetPermissonesbyIdsAsync(int[] ids);


    }
}
