using DnmaGroups.Common.Security;
using DnmaGroups.DataAccess;
using DnmaGroups.Domain.Entity;
using DnmaGroups.Service.Abstract;
using DnmaGroups.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PersianDate;
using DnmaGroups.Common;

namespace DnmaGroups.Service.Implement
{
    public class PermissonService : BaseService<Permisson>, IPermissonService
    {
        public PermissonService(IUnitOfWork unit)
            : base(unit)
        {

        }
     
        public async Task<IList<Permisson>> GetPermissonesbyIdsAsync(int[] ids)
        {
            var roles = await dbSet.ToListAsync();
            IList<Permisson> list = new List<Permisson>();
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    var role = roles.FirstOrDefault(x => x.Id == item);
                    if (role != null)
                        list.Add(role);
                }
            }
            return list;
        }



    }
}
