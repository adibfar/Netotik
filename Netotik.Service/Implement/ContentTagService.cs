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
using PersianDate;
using Netotik.Common;

namespace Netotik.Services.Implement
{
    public class ContentTagService : BaseService<ContentTag>, IContentTagService
    {
        public ContentTagService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public async Task<ContentTag> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey);
        }


        public async Task<bool> IsExistByName(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Text == name && x.Id != id);
            return await dbSet.AnyAsync(x => x.Text == name);
        }


        public IList<ContentTag> GetTagesbyIdsAsync(int[] ids)
        {
            var tag = dbSet.ToList();
            IList<ContentTag> list = new List<ContentTag>();
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
