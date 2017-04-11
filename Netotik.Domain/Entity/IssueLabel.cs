using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Netotik.Domain.Entity
{
    public partial class IssueLabel
    {
        public IssueLabel()
        {
            this.Issues = new List<Issue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }




    }

}
