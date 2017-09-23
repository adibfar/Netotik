using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public class UserCompanyLogClient
    {
        public UserCompanyLogClient()
        {
        }
        public long Id { get; set; }
        public DateTime MikrotikCreateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string SrcIp { get; set; }
        public string SrcMac { get; set; }
        public string Url { get; set; }
        public string DstIp { get; set; }
        public string Method { get; set; }

        public string Protocol { get; set; }
        public long UserCompanyId { get; set; }
        public virtual UserCompany UserCompany { get; set; }
    }
}
