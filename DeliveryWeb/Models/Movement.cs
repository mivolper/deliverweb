using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Models
{
    public class Movement
    {
        [Key]
        public int ID_Movement { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "اسم الحركة")]
        public string Name { get; set; }

        public bool Exist { get; set; }
    }
}
