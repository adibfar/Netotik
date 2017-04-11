using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using Netotik.Domain.Entity;

namespace Netotik.ViewModels.Support.Issue
{
    public class TableIssueModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastResponse { get; set; }
        public string LastUserResponse { get; set; }
        public IssueStatus Status { get; set; }

    }

}
