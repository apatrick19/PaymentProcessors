using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaymentProcessor.Core.Concrete
{
   public class Payment:Entity
    {
        [Required]
        [StringLength(50)]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string CardHolder { get; set; }

        [Required]
        [FutureDate]
        public DateTime ExpirationDate { get; set; }

        [StringLength(3)]
        public string SecurityCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]

        public decimal Amount { get; set; }

        public virtual PaymentState PaymentState { get; set; }


    }
}
