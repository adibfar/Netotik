using Netotik.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Netotik.ViewModels.Mikrotik
{
    public class GetUserLogModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }


        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }

    }
}
