using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netotik.Resources;

namespace Netotik.ViewModels.Shop.WareHouse
{
    public class TableWareHouseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }

    }
}
