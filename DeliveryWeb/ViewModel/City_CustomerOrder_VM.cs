using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryWeb.Models;

namespace DeliveryWeb.ViewModel
{
    public class City_CustomerOrder_VM
    {       
        public IEnumerable<City> Cities { get; set; }

        public CustomerOrder CustomerOrder { get; set; }
       
        public string Code { get; set; }
    }
}
