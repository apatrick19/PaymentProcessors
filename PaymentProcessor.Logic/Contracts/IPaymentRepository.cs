using PaymentProcessor.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Logic.Contracts
{
    public interface IPaymentRepository
    {
        
            Task<Payment> Get(int id);
            Task<IEnumerable<Payment>> GetAll();
            Task<int> Add(Payment entity);
            Task<bool> Delete(int id);
            Task<bool> Update(Payment catalogueEntity);
        
    }


    public interface IPaymentStateRepository
    {

        Task<PaymentState> Get(int id);
        Task<IEnumerable<PaymentState>> GetAll();
        Task<int> Add(PaymentState entity);
        Task<bool> Delete(int id);
        Task<bool> Update(PaymentState catalogueEntity);

    }
}
