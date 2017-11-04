using Netotik.Data.Context;
using Netotik.Data.Migrations;
using Netotik.Common.Filters;
using CaptchaMvc.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Globalization;
using System.Threading;
using Netotik.Common.Binders;
using System.Text.RegularExpressions;
using System.Web.Http;
using Netotik.Web.WebTasks;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.IO;
using System.Web.Hosting;
using Netotik.Domain.Entity;
using Netotik.Services.Identity;
using Netotik.Services.Abstract;
using Netotik.Data;
using Netotik.IocConfig;

namespace Netotik.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // change captcha provider for using cookie
            //  CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();


            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());


            ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());

            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ApplicationStart.Config();

            ScheduledTasksRegistry.Init();


        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var cryptoEx = error as CryptographicException;
            if (cryptoEx != null)
            {
                FormsAuthentication.SignOut();
                Server.ClearError();
            }
        }
        protected void Application_BeginRequest()
        {
            if (!Request.IsLocal)
            {
                if (!Context.Request.IsSecureConnection)
                    Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));

            }

            bool isGet = HttpContext.Current.Request.RequestType.ToLowerInvariant().Contains("get");
            if (isGet && HttpContext.Current.Request.Url.AbsolutePath.Contains(".") == false)
            {
                string lowercaseURL = (Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.AbsolutePath);
                if (Regex.IsMatch(lowercaseURL, @"[A-Z]"))
                {
                    //You don't want to change casing on query strings
                    lowercaseURL = lowercaseURL.ToLower() + HttpContext.Current.Request.Url.Query;

                    Response.Clear();
                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", lowercaseURL);
                    Response.End();
                }

            }
        }


        protected void Application_End()
        {
            ScheduledTasksRegistry.End();
            //نکته مهم این روش نیاز به سرویس پینگ سایت برای زنده نگه داشتن آن است
            ScheduledTasksRegistry.WakeUp("https://www.Netotik.com");
        }
    }

    public static class UdpListenerClass
    {

        public static bool messageReceived = false;
        public static IPEndPoint e = new IPEndPoint(IPAddress.Any, 5140);
        public static UdpClient u = new UdpClient(e);
        public static IApplicationUserManager _applicationUserManager;
        public static IUserRouterLogClientService _UserRouterlogclientservice;
        public static IUnitOfWork _uow;
        public static UdpState s = new UdpState();


        public class UdpState
        {
            public UdpClient ut;
            public IPEndPoint e;
            public const int BufferSize = 1024;
            public byte[] buffer = new byte[BufferSize];
            public int counter = 0;
            internal UdpClient u;
        }

        public static async void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            Byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.UTF8.GetString(receiveBytes);


            HostingEnvironment.QueueBackgroundWorkItem(async cancellationToken =>
            {
                await WirteToDB(e.Address.ToString(), receiveString);
            });


            try
            {
                u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
            }
            catch (Exception ex)
            {
                //using (StreamWriter _testData = new StreamWriter(HostingEnvironment.MapPath("~/2.txt"), true))
                //{
                //    _testData.WriteLine(ex); // Write the file.
                //}
            }
        }

        public static void ReceiveMessages()
        {

            _applicationUserManager = ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>();
            _UserRouterlogclientservice = ProjectObjectFactory.Container.GetInstance<IUserRouterLogClientService>();
            _uow = ProjectObjectFactory.Container.GetInstance<IUnitOfWork>();

            s.e = e;
            s.u = u;
            try
            {
                u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
            }
            catch (Exception ex)
            {
                //using (StreamWriter _testData = new StreamWriter(HostingEnvironment.MapPath("~/1.txt"), true))
                //{
                //    _testData.WriteLine(ex); // Write the file.
                //}
            }
            // Do some work while we wait for a message. For this example,
            // we'll just sleep
        }

        public async static Task WirteToDB(string Ip, string Message)
        {


            var ActiveUsers = _applicationUserManager.GetUserRoutersWebsitesLogsActive();
            if (ActiveUsers != null)//&& UserRouter.WebsiteLogs == true
            {
                foreach (var User in ActiveUsers)
                {
                    if (User.UserRouter.R_Host == Ip || Dns.GetHostAddresses(User.UserRouter.R_Host).Any(x => x.Address.Equals(IPAddress.Parse(Ip))))
                    {
                        try
                        {
                            UserRouterLogClient http = new UserRouterLogClient();
                            http.MikrotikCreateDate = DateTime.Now;
                            http.UserRouterId = User.Id;
                            if (Message != null && Message.Split(' ')[3].Contains("http://"))
                            {
                                http.Url = Message.Split(' ')[3];
                                http.SrcIp = Message.Split(' ')[1];
                                http.DstPort = 80;
                            }
                            else
                            {
                                if (Message.Contains("mac"))
                                {
                                    if (Message != null && Message.Split(' ')[9].Contains(">"))
                                    {
                                        http.SrcMac = Message.Split(' ')[5].Split(',')[0];
                                        http.SrcIp = Message.Split(' ')[9].Split('-')[0].Split(':')[0];
                                        http.DstIp = Message.Split(' ')[9].Split('>')[1].Split(':')[0];
                                        http.SrcPort = Int32.Parse(Message.Split(' ')[9].Split('-')[0].Split(':')[1]);
                                        http.DstPort = Int32.Parse(Message.Split(' ')[9].Split('>')[1].Split(':')[1].Split(',')[0]);
                                    }
                                }
                                else
                                {
                                    if (Message != null && Message.Split(' ')[7].Contains(">"))
                                    {
                                        http.SrcIp = Message.Split(' ')[7].Split('-')[0].Split(':')[0];
                                        http.DstIp = Message.Split(' ')[7].Split('>')[1].Split(':')[0];
                                        http.SrcPort = Int32.Parse(Message.Split(' ')[7].Split('-')[0].Split(':')[1]);
                                        http.DstPort = Int32.Parse(Message.Split(' ')[7].Split('>')[1].Split(':')[1].Split(',')[0]);
                                    }
                                }

                            }
                            _UserRouterlogclientservice.Add(http);
                            await _uow.SaveAllChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            var exs = ex;
                        }
                    }
                }
            }

        }
    }
}