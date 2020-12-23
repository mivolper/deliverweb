using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Models
{
    public class CustomerOrder
    {
        [Key]
        public int ID_SubOrder { get; set; }

        [Display(Name = "كود المرسل")]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "اسم المستلم")]
        public string Recipient { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "رقم هاتف المستلم 1")]
        public string RecipientPhone1 { get; set; }

        [Display(Name = "رقم هاتف المستلم 2")]
        public string RecipientPhone2 { get; set; }

        [Display(Name = "عنوان المستلم")]
        public string Address { get; set; }

        [Required(ErrorMessage = "يجب ملء هذا العنصر . . .")]
        [Display(Name = "مدينة المستلم")]
        public int ID_City { get; set; }
        [ForeignKey("ID_City")]
        public virtual City City { get; set; }

        [Required]
        [Display(Name = "قيمة الطرد")]
        public float PackagePrice { get; set; }

        [Required]
        [Display(Name = "عدد الطرود")]
        public float PackageNumber { get; set; }
    }
}
