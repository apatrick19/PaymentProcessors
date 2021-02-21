using PaymentProcessor.Core.Concrete;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentProcessor.Logic.Models
{
    public class PaymentModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Credit Card length can't be more than 50.")]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Credit Holder length can't be more than 200.")]
        public string CardHolder { get; set; }


        [Required]
        [FutureDate] //this is a custom class handler
        public DateTime ExpirationDate { get; set; }


        [StringLength(3, ErrorMessage = "Security Code length can't be more than 3.")]
        public string SecurityCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]        
        [GreaterThanZero(ErrorMessage = "Only positive number allowed.")]
        public decimal Amount { get; set; }
    }
}
