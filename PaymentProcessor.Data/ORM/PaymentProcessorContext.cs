using Microsoft.EntityFrameworkCore;
using PaymentProcessor.Core.Concrete;
using System;

namespace PaymentProcessor.Data
{
    public class PaymentProcessorContext:DbContext
    {
        public PaymentProcessorContext(DbContextOptions<PaymentProcessorContext> options)
           : base(options)
        {
        }
       

        public DbSet<PaymentState> PaymentState { get; set; }

        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentState>().ToTable("PaymentState");
            modelBuilder.Entity<Payment>().ToTable("Payment");            
        }
    }
}
