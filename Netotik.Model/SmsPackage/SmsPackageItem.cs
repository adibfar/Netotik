using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.SmsPackage
{
    public class SmsPackageItem
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string Name { get; set; }
        public int SmsCount { get; set; }
        public long UnitPrice { get; set; }
        public long Price { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }

    }
}
