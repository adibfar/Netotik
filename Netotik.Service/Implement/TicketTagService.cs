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

namespace Netotik.Services.Implement
{
    public class TicketTagService : BaseService<TicketTag>, ITicketTagService
    {
        public TicketTagService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public async Task<TicketTag> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey);
        }


        public async Task<bool> IsExistByName(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name == name && x.Id != id);
            return await dbSet.AnyAsync(x => x.Name == name);
        }


        public IList<TicketTag> GetbyIds(int[] ids)
        {
            var tag = dbSet.ToList();
            var list = new List<TicketTag>();
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    var t = tag.FirstOrDefault(x => x.Id == item);
                    if (t != null)
                        list.Add(t);
                }
            }
            return list;
        }
    }
}
