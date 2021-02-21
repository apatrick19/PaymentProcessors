using PaymentProcessor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Logic.Contracts
{
    public interface ICheapPaymentGateway
    {
        public ResponseModel ProcessPayment(PaymentModel request);
       
    }
}
