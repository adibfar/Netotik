using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.CMS.Menu
{
    public class MenuItem
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
        public string Parent { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        
    }
}
