using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Common.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Identity.UserClient;

namespace Netotik.Services.Abstract
{
    public interface IReportService
    {
        void Print(List<UserModel> userlist, long id);
    }
}
