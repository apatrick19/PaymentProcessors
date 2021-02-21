using PaymentProcessor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Logic.Contracts
{
   public  interface IExpensivePaymentGateway
    {
        public ResponseModel ProcessExpensivePayment(PaymentModel request);
    }
}
