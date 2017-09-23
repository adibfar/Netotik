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
using Netotik.Model.Common.ContactUs;
using Netotik.Common.DataTables;

namespace Netotik.Services.Implement
{
    public class UserCompanyLogClientService : BaseService<UserCompanyLogClient>, IUserCompanyLogClientService
    {
        public UserCompanyLogClientService(IUnitOfWork unit)
            : base(unit)
        {

        }


        public IList<UserCompanyLogClient> GetList(long CompanyId)
        {
            return dbSet.AsQueryable().Where(
                x => x.UserCompanyId == CompanyId).ToList();
        }
        //public IList<UserCompanyLogClient> GetRangeListList(long CompanyId,DateTime StartDateTime,DateTime EndDateTime)
        //{
        //    return dbSet.AsQueryable().Where(
        //        x => x.UserCompanyId == CompanyId).ToList()
        //        .Where(x => DateTime.Compare(x..AddMinutes(5), DateTime.Now) > 0).ToList();
        //}


    }
}
