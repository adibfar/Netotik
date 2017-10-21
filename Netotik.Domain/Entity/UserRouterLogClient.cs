using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public class UserRouterLogClient
    {
        public UserRouterLogClient()
        {
        }
        public long Id { get; set; }
        public DateTime MikrotikCreateDate { get; set; }
        public string SrcIp { get; set; }
        public int SrcPort { get; set; }
        public string SrcMac { get; set; }
        public string Url { get; set; }
        public string DstIp { get; set; }
        public int DstPort { get; set; }
        public string Method { get; set; }
        public long UserRouterId { get; set; }
        public virtual UserRouter UserRouter { get; set; }
    }
}
