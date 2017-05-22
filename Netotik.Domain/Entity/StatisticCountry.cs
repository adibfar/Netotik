using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Domain.Entity
{
    public class StatisticCountry
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int ViewCount { get; set; }
    }
}
