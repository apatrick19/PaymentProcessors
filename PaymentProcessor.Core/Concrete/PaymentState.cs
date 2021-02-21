using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentProcessor.Core.Concrete
{
    public class PaymentState:Entity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
