using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Logic.Contracts
{
   public  interface IUnitOfWork:IDisposable
    {
        IPaymentRepository _paymentRepository { get; }

        IPaymentStateRepository _paymentStateRepository { get; }
        int Complete();
    }
}
