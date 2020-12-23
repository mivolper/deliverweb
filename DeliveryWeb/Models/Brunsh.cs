using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Models
{
    public class Brunsh
    {
        [Key]
        public int ID_Brunsh { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "اسم الفرع")]
        public string BrunshName { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "رمز الفرع")]
        public string Code { get; set; }

        public bool Exist { get; set; }

    }
}
