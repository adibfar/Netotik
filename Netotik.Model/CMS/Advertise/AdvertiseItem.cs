using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Netotik.ViewModels.CMS.Advertise
{
    public class AdvertiseItem
    {
        public int? Id { get; set; }
        public long RowNumber { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
    }
}
