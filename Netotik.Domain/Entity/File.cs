using System;
using System.Collections.Generic;

namespace Netotik.Domain.Entity
{
    public partial class File
    {
        public File()
        {
            this.Contents = new List<Content>();
            this.Issues = new List<Issue>();
            this.IssueTracks = new List<IssueTrack>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string OrginalName { get; set; }
        public string MimeType { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<IssueTrack> IssueTracks { get; set; }
    }
}
