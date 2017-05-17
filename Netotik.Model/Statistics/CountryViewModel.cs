using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netotik.ViewModels.Statistics
{
    public class CountryViewModel
    {
        public string CountryName { get; set; }
        public int ViewCount { get; set; }
        public int TotalVisits { get; set; }
        public int Percentage { get; set; }


    }
}