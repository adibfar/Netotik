using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Common.City
{
    public class CityItem
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

    }
}
