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
    public class ProductCommentService : BaseService<ProductComment>, IProductCommentService
    {
        public ProductCommentService(IUnitOfWork unit)
            : base(unit)
        {

        }

        public async Task<ProductComment> SingleOrDefaultAsync(int primaryKey)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == primaryKey);
        }
    }
}
