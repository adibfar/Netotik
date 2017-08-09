using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Netotik.Services.Identity
{
    public interface IPermissionCompanyService
    {
        XElement GetPermissionsAsXml(params string[] permissionNames);
        IList<string> GetPermissionsAsList(XElement permissionsAsXml);
        IList<string> GetPermissionsAsList(IList<XElement> permissionsAsXmls);

    }
}
