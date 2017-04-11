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
    public class FileService : BaseService<File>, IFileService
    {
        public FileService(IUnitOfWork unit)
            : base(unit)
        {

        }
     
        public async Task RemoveAsync(int id)
        {
            var pic = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (pic != null)
                Remove(pic);
        }



    }
}
