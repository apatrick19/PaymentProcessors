using Microsoft.EntityFrameworkCore;
using PaymentProcessor.Core.Concrete;
using PaymentProcessor.Data;
using PaymentProcessor.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Logic.Repository
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly PaymentProcessorContext _context;
        public PaymentRepository(PaymentProcessorContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Payment entity)
        {
            await _context.Payment.AddAsync(entity);
            return 1;
        }

        public async Task<bool> Delete(int id)
        {
            var catalogue = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(catalogue);
            return true;
        }

        public async Task<Payment> Get(int id)
        {
            return await _context.Payment.FindAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            return await _context.Payment.ToListAsync();
        }

        public async Task<bool> Update(Payment entity)
        {
            var catalogue = await _context.Payment.FindAsync(entity);
            this._context.Entry(catalogue).CurrentValues.SetValues(entity);
            return true;
        }

      

       
    }


    public class PaymentStateRepository : IPaymentStateRepository
    {
        private readonly PaymentProcessorContext _context;
        public PaymentStateRepository(PaymentProcessorContext context)
        {
            _context = context;
        }
        public async Task<int> Add(PaymentState entity)
        {
            await _context.PaymentState.AddAsync(entity);
            return 1;
        }

        public async Task<bool> Delete(int id)
        {
            var catalogue = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(catalogue);
            return true;
        }

        public async Task<PaymentState> Get(int id)
        {
            return await _context.PaymentState.FindAsync(id);
        }

        public async Task<IEnumerable<PaymentState>> GetAll()
        {
            return await _context.PaymentState.ToListAsync();
        }

        public async Task<bool> Update(PaymentState entity)
        {
            var catalogue = await _context.PaymentState.FindAsync(entity);
            this._context.Entry(catalogue).CurrentValues.SetValues(entity);
            return true;
        }




    }
}
