using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.Common.Security.RemoveHeader
{
    public class RemoveHeaderModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.PreSendRequestHeaders += app_PreSendRequestHeaders;
        }

        static void app_PreSendRequestHeaders(object sender, EventArgs e)
        {
            ClientRemoveHeader.CheckPreSendRequestHeaders(sender);
        }

        public void Dispose() { }
    }
}
