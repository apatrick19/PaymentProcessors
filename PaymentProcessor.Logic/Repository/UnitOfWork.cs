using PaymentProcessor.Data;
using PaymentProcessor.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessor.Logic.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentProcessorContext _context;
        public IPaymentRepository _paymentRepository { get; }
        public IPaymentStateRepository _paymentStateRepository { get; }
              

        public UnitOfWork(PaymentProcessorContext paymentDbContext, IPaymentRepository paymentRepository, IPaymentStateRepository state)
        {
            this._context = paymentDbContext;
            this._paymentRepository = paymentRepository;
            this._paymentStateRepository = state;


        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
