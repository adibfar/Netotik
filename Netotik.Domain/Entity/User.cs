using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>
    {
        public User()
        {
            this.IssuesCreated = new List<Ticket>();
            this.IssuesResponsed = new List<Ticket>();
            this.IssueUsers = new List<Ticket>();
            this.ContentsCreated = new List<Content>();
            this.ContentsEdited = new List<Content>();
            this.ContentComments = new List<ContentComment>();
            this.Addresses = new List<Address>();
            this.Roles = new List<UserRole>();
            this.SmsLogs = new List<SmsLog>();
            this.Factores = new List<Factor>();
        }

        public bool IsBanned { get; set; }
        public bool IsDeleted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShowName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime EditDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public string LastLoginIpAddress { get; set; }
        public Nullable<int> PictureId { get; set; }
        public UserType UserType { get; set; }
        public virtual ICollection<SmsLog> SmsLogs { get; set; }
        public virtual ICollection<Content> ContentsEdited { get; set; }
        public virtual ICollection<Content> ContentsCreated { get; set; }
        public virtual ICollection<Ticket> IssuesResponsed { get; set; }
        public virtual ICollection<TicketTrack> IssuesTracksResponsed { get; set; }
        public virtual ICollection<Ticket> IssuesCreated { get; set; }
        public virtual ICollection<Ticket> IssueUsers { get; set; }
        public virtual ICollection<ContentComment> ContentComments { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual UserAdmin UserAdmin { get; set; }
        public virtual UserReseller UserReseller { get; set; }
        public virtual UserCompany UserCompany { get; set; }
        public List<Address> Addresses { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<UserLogin> Logins { get; set; }
        public virtual ICollection<Factor> Factores { get; set; }
    }

    public enum UserType : short
    {
        UserAdmin = 0,
        UserReseller = 1,
        UserCompany = 2,
        Client = 3
    }
}
