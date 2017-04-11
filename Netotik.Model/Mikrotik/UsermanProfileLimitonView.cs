using Netotik.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.ViewModels.Mikrotik
{
    public class UsermanProfileLimitionView
    {
        public UsermanLimition UsermanLimition { get; set; }
        public UsermanProfileLimition UsermanProfileLimition { get; set; }
        public UsermanProfile UsermanProfile { get; set; }
    }
}
