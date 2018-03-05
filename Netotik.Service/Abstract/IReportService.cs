using Netotik.Common.DataTables;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Common.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.ViewModels.Identity.UserClient;
using System.Web.Mvc;

namespace Netotik.Services.Abstract
{
    public interface IReportService
    {
        ActionResult Print(List<UserModel> userlist, long id);
    }
}
