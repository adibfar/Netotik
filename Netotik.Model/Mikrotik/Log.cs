using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Mikrotik
{
    public class Log
    {
        public string id { get; set; }

        public string time { get; set; }

        public string topics { get; set; }

        public string message { get; set; }
    }
}
