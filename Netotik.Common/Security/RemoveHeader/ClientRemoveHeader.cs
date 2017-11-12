using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.Common.Security.RemoveHeader
{
    public class ClientRemoveHeader
    {
        private static readonly List<string> _headersToRemoveCache
           = new List<string>
                             {
                                 "X-AspNet-Version",
                                 "X-AspNetMvc-Version",
                                 "Server"
                             };

        public static void CheckPreSendRequestHeaders(Object sender)
        {
            //capture the current request
            var currentResponse = ((HttpApplication)sender).Response;

            //removing headers
            //it only works with IIS 7.x's integrated pipeline
            _headersToRemoveCache.ForEach(h => currentResponse.Headers.Remove(h));
        }
    }
}
