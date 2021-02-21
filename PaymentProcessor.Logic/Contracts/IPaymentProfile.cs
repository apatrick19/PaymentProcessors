using PaymentProcessor.Core.Concrete;
using PaymentProcessor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Logic.Contracts
{
   public  interface IPaymentProfile
    {
        public Payment MapPayment(PaymentModel model);


    }
}
