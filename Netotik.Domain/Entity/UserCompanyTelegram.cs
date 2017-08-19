using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public class UserCompanyTelegram
    {
        public UserCompanyTelegram()
        {
        }
        public long Id { get; set; }


        public virtual UserCompany UserCompany { get; set; }
    }
}
