using Netotik.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.WindowsService
{
    partial class MikrotikLogger : ServiceBase
    {
        public MikrotikLogger()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            try
            {
                ReceiveMessages();
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter("C:\\MikrotikLoggerErrors.txt", true))
                {
                    writer.WriteLine("OnStartError ========================= \n" + ex);
                }
            }
        }


        #region Comment
        public static bool messageReceived = false;
        public static IPEndPoint e = new IPEndPoint(IPAddress.Any, 514);
        public static UdpClient u = new UdpClient(e);
        public static UdpState s = new UdpState();


        public class UdpState
        {
            public UdpClient ut;
            public IPEndPoint e;
            public const int BufferSize = 2048;
            public byte[] buffer = new byte[BufferSize];
            public int counter = 0;
            internal UdpClient u;
        }
        public async void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
                IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

                Byte[] receiveBytes = u.EndReceive(ar, ref e);
                string receiveString = Encoding.UTF8.GetString(receiveBytes);
                Task t = Task.Factory.StartNew(async () => { await DetectDataModel(e.Address.ToString(), receiveString); });

                u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter("C:\\MikrotikLoggerErrors.txt", true))
                {
                    writer.WriteLine("ReciveCallback ========================= \n" + ex);
                }
            }
        }

        public void ReceiveMessages()
        {
            s.e = e;
            s.u = u;
            try
            {
                u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter("C:\\MikrotikLoggerErrors.txt", true))
                {
                    writer.WriteLine("ReciveMessage ========================= \n" + ex);
                }
            }
        }

        #endregion

        #region Write to DB

        public async Task DetectDataModel(string address, string receiveString)
        {
            using (var db = new Netotik.Data.Context.NetotikDBContext())
            {

                var ActiveUsers = db.Users.Where(x => x.UserType == UserType.UserRouter && !x.IsDeleted && x.UserRouter.WebsitesLogs).ToList();


                if (ActiveUsers != null)//&& UserRouter.WebsiteLogs == true
                {
                    foreach (var User in ActiveUsers)
                    {
                        bool dnslookupflag = false;
                        IPHostEntry hostEntry;

                        hostEntry = Dns.GetHostEntry(User.UserRouter.R_Host);
                        if (hostEntry.AddressList.Length > 0)
                        {
                            var ip = hostEntry.AddressList[0];
                            IPAddress ip1 = IPAddress.Parse(address);
                            if (ip1.Equals(ip))
                                dnslookupflag = true;
                        }
                        if (User.UserRouter.R_Host == address|| dnslookupflag || Dns.GetHostAddresses(User.UserRouter.R_Host).Any(x => x.Address.Equals(IPAddress.Parse(address))))
                        {
                            try
                            {
                                UserRouterLogClient http = new UserRouterLogClient();
                                http.MikrotikCreateDate = DateTime.Now;
                                http.UserRouterId = User.Id;
                                if (receiveString != null && receiveString.Split(' ')[3].Contains("http://"))
                                {
                                    http.Url = receiveString.Split(' ')[3];
                                    http.SrcIp = receiveString.Split(' ')[1];
                                    http.DstPort = 80;
                                }
                                else
                                {
                                    if (receiveString.Contains("mac"))
                                    {
                                        if (receiveString != null && receiveString.Split(' ')[9].Contains(">") && receiveString.Split(' ')[1].Contains("srcnat"))
                                        {
                                            http.SrcMac = receiveString.Split(' ')[5].Split(',')[0];
                                            http.SrcIp = receiveString.Split(' ')[9].Split('-')[0].Split(':')[0];
                                            http.DstIp = receiveString.Split(' ')[9].Split('>')[1].Split(':')[0];
                                            http.SrcPort = Int32.Parse(receiveString.Split(' ')[9].Split('-')[0].Split(':')[1]);
                                            http.DstPort = Int32.Parse(receiveString.Split(' ')[9].Split('>')[1].Split(':')[1].Split(',')[0]);
                                        }
                                    }
                                    else
                                    {
                                        if (receiveString != null && receiveString.Split(' ')[7].Contains(">") && receiveString.Split(' ')[1].Contains("srcnat"))
                                        {
                                            http.SrcIp = receiveString.Split(' ')[7].Split('-')[0].Split(':')[0];
                                            http.DstIp = receiveString.Split(' ')[7].Split('>')[1].Split(':')[0];
                                            http.SrcPort = Int32.Parse(receiveString.Split(' ')[7].Split('-')[0].Split(':')[1]);
                                            http.DstPort = Int32.Parse(receiveString.Split(' ')[7].Split('>')[1].Split(':')[1].Split(',')[0]);
                                        }
                                    }

                                }
                                db.UserRouterLogClients.Add(http);
                                await db.SaveAllChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                using (StreamWriter writer = new StreamWriter("C:\\MikrotikLoggerErrors.txt", true))
                                {
                                    writer.WriteLine("DetectDataModel ========================= \n" + ex);
                                }
                            }
                        }
                    }
                }

            }
        }

        #endregion



        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
