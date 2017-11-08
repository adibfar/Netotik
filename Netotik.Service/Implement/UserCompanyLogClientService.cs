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
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class UserRouterLogClientService : BaseService<UserRouterLogClient>, IUserRouterLogClientService
    {
        public UserRouterLogClientService(IUnitOfWork unit)
            : base(unit)
        {

        }


        public IList<UserRouterLogClient> GetList(long Id)
        {
            return dbSet.AsQueryable().Where(
                x => x.UserRouterId == Id && x.UserRouter.WebsitesLogs).ToList();
        }
        public IList<UserRouterLogClient> GetList(long Id, DateTime FromTime, DateTime ToTime)
        {
            return dbSet.AsQueryable().Where(
                x => x.UserRouterId == Id && x.UserRouter.WebsitesLogs &&
                x.MikrotikCreateDate >= FromTime && x.MikrotikCreateDate <= ToTime
                ).ToList();
        }

    }
}
