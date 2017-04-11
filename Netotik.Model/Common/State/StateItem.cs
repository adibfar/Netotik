using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Common.State
{
    /// <summary>
    /// using DataTables
    /// </summary>
    public class StateItem
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

    }
}
