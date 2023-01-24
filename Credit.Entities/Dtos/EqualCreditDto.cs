using Credit.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credit.Entities.Dtos
{
    public class EqualCreditDto
    {
        [DisplayName("Kredi Türü")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public string? CreditModel { get; set; }

        [DisplayName("Kredi Tutarı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public double Amount { get; set; }

        [DisplayName("Vade(Ay)")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [Range(1,360, ErrorMessage ="{0}, {1}-{2} aralığında olmalıdır.")]
        public int Expiry { get; set; }

        [DisplayName("Aylık Oran(%)")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [Range(0.0001,50, ErrorMessage ="{0} {1}-{2} aralığında olmalıdır.")]
        public double Interest { get; set; }

        [DisplayName("Ödeme Sıklığı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [Range(1,12, ErrorMessage ="{0} {1}-{2} aralığında olmalıdır.")]
        public int PaymentFrequency { get; set; }
    }
}
