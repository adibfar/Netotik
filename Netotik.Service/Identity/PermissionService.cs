using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Netotik.Services.Identity;

namespace Netotik.Services.Identity
{
    public class PermissionService : IPermissionService
    {
        #region Fields

        private const string PermissionsElement = "Permissions";
        private const string PermissionElement = "Permission";
        #endregion

        #region Ctor

        public PermissionService()
        {

        }
        #endregion

        #region GetPermissionsAsXml
        public System.Xml.Linq.XElement GetPermissionsAsXml(params string[] permissionNames)
        {
            var permissionsAsXml = new XElement(PermissionsElement);
            foreach (var permissionName in permissionNames)
            {
                permissionsAsXml.Add(new XElement(PermissionElement, permissionName));
            }
            return permissionsAsXml;
        }

        #endregion

        #region GetUserPermissionsAsList
        public IList<string> GetUserPermissionsAsList(XElement permissionsAsXml)
        {
            return permissionsAsXml.Elements(PermissionElement).Select(a => a.Value).ToList();
        }

        #endregion


        public IList<string> GetUserPermissionsAsList(IList<XElement> permissionsAsXmls)
        {
            var permissions = new List<string>();
            foreach (var permissionsAsXml in permissionsAsXmls.Where(permissionsAsXml => permissionsAsXml != null))
            {
                permissions.AddRange(permissionsAsXml.Elements(PermissionElement).Select(a => a.Value).ToList());
            }
            return permissions;
        }


    }
}
