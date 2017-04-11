using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;
using System.Web.Mvc;

namespace Netotik.ViewModels.Support.Issue
{
    public class IssueTrackModel
    {
        public long IssueId { get; set; }
        
        [UIHint("SummerNoteEditorNoLabel")]
        [AllowHtml]
        public string Description { get; set; }
    }
}
