﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Netotik.Domain.Entity
{
    public partial class Issue
    {
        public Issue()
        {
            this.IssueTracks = new List<IssueTrack>();
            this.FilesAttach = new List<File>();
            this.IssueLabels = new List<IssueLabel>();
            this.IssueUsers = new List<User>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MessageCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastResponseDate { get; set; }
        public long CreatedUserId { get; set; }
        public long LastResponseUserId { get; set; }
        public IssueStatus status { get; set; }
        public IssuePeriority Periority { get; set; }
        public virtual User UserCreated { get; set; }
        public virtual User LastResponseUser { get; set; }
        public virtual ICollection<File> FilesAttach { get; set; }
        public virtual ICollection<User> IssueUsers { get; set; }
        public virtual ICollection<Role> IssueRoles { get; set; }
        public virtual ICollection<IssueTrack> IssueTracks { get; set; }

        public virtual ICollection<IssueLabel> IssueLabels { get; set; }


        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string statusName
        {
            get
            {
                switch (status)
                {
                    case IssueStatus.close:
                        return "بسته شده";
                    case IssueStatus.open:
                        return "ایجاد شده";
                    case IssueStatus.ResponseByUser:
                        return "پاسخ داده شده توسط مسئول";
                    case IssueStatus.ResponseByAdmin:
                        return "پاسخ داده شده توسط ناظر";
                    case IssueStatus.Reopened:
                        return "نیازمند بررسی";
                    default:
                        return "";
                }

            }
        }


        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string PeriorityName
        {
            get
            {
                switch (Periority)
                {
                    case IssuePeriority.Normal:
                        return "معمولی";
                    case IssuePeriority.Important:
                        return "مهم";
                    case IssuePeriority.VeryImportant:
                        return "بسیار مهم";
                    default:
                        return "";
                }

            }
        }

    }



    public enum IssueStatus : short
    {
        close = 0,
        open,
        ResponseByUser,
        ResponseByAdmin,
        Reopened,
    }

    public enum IssuePeriority : short
    {
        [Display(Name = "معمولی")]
        Normal,
        [Display(Name = "مهم")]
        Important,
        [Display(Name = "بسیار مهم")]
        VeryImportant
    }

}
