using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Models
{
    public class City
    {
        [Key]
        public int ID_City { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "المدينة")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "السعر")]
        public decimal PriceMen { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "مدينة المستلم")]
        public int ID_Province { get; set; }
        [ForeignKey("ID_Province")]
        public virtual Province Province { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "الأيام")]
        public string Days { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "رمز الفرع")]
        public string Brunsh { get; set; }

        public bool Exist { get; set; }

        public string ProvinceName { get; set; }

    }
}
