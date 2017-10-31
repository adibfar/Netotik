using Netotik.Common.Helpers;
using Netotik.Common.Security.RijndaelEncryption;
using Netotik.IocConfig;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace Netotik.Web
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        #region Fields
        private readonly IEncryptSettingsProvider _settings;
        #endregion

        #region Ctor
        public StructureMapControllerFactory()
        {
            _settings = new EncryptSettingsProvider();
        }

        #endregion

        #region override CreateController

        private IRijndaelStringEncrypter GetDecrypter(System.Web.Routing.RequestContext requestContext)
        {
            var decrypter = new RijndaelStringEncrypter(_settings, requestContext.GetActionKey());
            return decrypter;
        }
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            var routeData = requestContext.RouteData;
            if (routeData.Values.ContainsKey("MS_DirectRouteMatches"))
            {
                routeData = ((IEnumerable<RouteData>)routeData.Values["MS_DirectRouteMatches"]).First();
            }

            var parameters = requestContext.HttpContext.Request.Params;
            var encryptedParamKeys = parameters.AllKeys.Where(x => x.StartsWith(_settings.EncryptionPrefix)).ToList();

            IRijndaelStringEncrypter decrypter = null;

            foreach (var key in encryptedParamKeys)
            {
                if (decrypter == null)
                {
                    decrypter = GetDecrypter(requestContext);
                }

                var oldKey = key.Replace(_settings.EncryptionPrefix, string.Empty);
                var oldValue = decrypter.Decrypt(parameters[key]);
                if (routeData.Values[oldKey] != null)
                {
                    if (routeData.Values[oldKey].ToString() != oldValue)
                        throw new ApplicationException("Form values is modified!");
                }

                routeData.Values[oldKey] = oldValue;
            }

            decrypter?.Dispose();
            
            return base.CreateController(requestContext, controllerName);
        }

        #endregion

        #region override GetControllerInstance


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
         

            if (controllerType == null)
            {

                requestContext.RouteData.Values["action"] = MVC.Error.ActionNames.NotFound;
                requestContext.RouteData.Values["controller"] = MVC.Error.Name;
                return CreateController(requestContext, MVC.Error.Name);

                //throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));

            }

            var controller = ProjectObjectFactory.Container.GetInstance(controllerType);

            if (controller != null)
            {
                (controller as Controller).TempDataProvider = ProjectObjectFactory.Container.GetInstance<ITempDataProvider>();

            }

            return controller as Controller;
        }
        #endregion
    }
}




















