﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Netotik.Domain.Entity
{
    public partial class IssueTrack
    {
        public IssueTrack()
        {
            this.FilesAttach = new List<File>();
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public long IssueId { get; set; }
        public Issue Issue { get; set; }
        public long CreatedUserId { get; set; }
        public virtual User UserCreated { get; set; }
        public virtual ICollection<File> FilesAttach { get; set; }
    }
}
