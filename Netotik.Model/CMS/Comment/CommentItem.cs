using Netotik.Domain.Entity;
using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.CMS.Comment
{
    public class CommentItem
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CommentStatus Status { get; set; }
    }
}
