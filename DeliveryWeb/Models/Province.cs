using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Models
{
    public class Province
    {
        [Key]
        public int ID_Province { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "اسم الاقليم")]
        public string ProvinceName { get; set; }

        public bool Exist { get; set; }
    }
}
