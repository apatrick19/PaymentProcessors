using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Core.Contracts
{
   public  interface IEntity
    {
        public long ID { get; set; }
    }
}
