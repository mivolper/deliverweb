using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWeb.Models
{
    public class Order
    {
        [Key]
        public int ID_Order { get; set; }

        [Display(Name ="كود الطرد")]
        public string Barcode { get; set; }

        [Display]
        public string Customer { get; set; }

        [Display]
        public string CustomerPhone { get; set; }

        [Display(Name = "اسم المستلم")]
        public string Recipient { get; set; }

        [Display(Name = "رقم هاتف المستلم 1")]
        public string RecipientPhone1 { get; set; }

        [Display(Name = "رقم هاتف المستلم 2")]
        public string RecipientPhone2 { get; set; }

        [Display(Name = "عنوان المستلم")]
        public string Address { get; set; }

        [Display(Name = "مدينة المستلم")]
        public string City { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "قيمة الطرد")]
        public decimal PackagePrice { get; set; }

        [Display(Name = "عدد الطرود")]
        public int PackageNumber { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر التوصيل")]
        public decimal DeliveryPrice { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "اجمالي السعر")]
        public decimal TotalPrice { get; set; }

        [Display]
        public string Delegate { get; set; }

        [Display(Name = "حالة الطرد")]
        public string State { get; set; }

        [Display(Name = "تاريخ التسليم للمكتب")]
        public DateTime Date { get; set; }

        [Display]
        public string User { get; set; }

        [Display]
        public bool Exist { get; set; }

        [Display]
        public string Note { get; set; }


        [Display]
        public DateTime DateState { get; set; }

        [Display]
        public string Province { get; set; }

    }
}
