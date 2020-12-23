using DeliveryWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.ViewModel
{
    public class City_Province
    {
        public List<Province> Provinces { get; set; }

        public City City { get; set; }

        public string Code { get; set; }
    }
}
