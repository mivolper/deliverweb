using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Models
{
    public class Customer:IdentityUser
    {
        public string Code { get; set; }

        [Display(Name = "اسم المتجر")]
        public string Name { get; set; }

        [Display(Name = "رقم الهاتف 1")]
        public string Phone1 { get; set; }

        [Display(Name = "رقم الهاتف 2")]
        public string Phone2 { get; set; }

        [Display(Name = "العنوان")]
        public string Address { get; set; }

        public bool Exist { get; set; }

        public string Role { get; set; }

    }
}
