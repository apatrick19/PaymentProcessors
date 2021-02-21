using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Logic.Contracts
{
    public interface IRestIntegration
    {
        public T UrlPost<T>(string url, object theObject);
    }
}
