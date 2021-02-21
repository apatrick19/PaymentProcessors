using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Core.Concrete
{
    public enum State
    {
        Pending=0,
        Processed=1,
        Failed=2
    }
}
