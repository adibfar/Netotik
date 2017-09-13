using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public class UserCompanyClient
    {
        public UserCompanyClient()
        {
        }
        public long Id { get; set; }

        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Age { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Name { get; set; }
        public bool? IsMale { get; set; }
        public string NationalCode { get; set; }

        public long UserCompanyId { get; set; }
        public virtual UserCompany UserCompany { get; set; }
    }
}
