using PaymentProcessor.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PaymentProcessor.Logic.Contracts;
using System.Diagnostics;

namespace PaymentProcessor.Logic.Models
{
   public  class PaymentProfile: Profile, IPaymentProfile
    {
        private readonly IMapper _mapper;

        public PaymentProfile(IMapper map)
        {
            _mapper = map;
            this.CreateMap<PaymentModel, Payment>().ReverseMap();
        }

        public PaymentProfile()
        {          
            this.CreateMap<PaymentModel, Payment>().ReverseMap();
        }

        public Payment MapPayment(PaymentModel model)
        {
            try
            {
                var payment = _mapper.Map<Payment>(model);
                return payment;
            }
            catch (Exception ex)
            {

                Trace.TraceInformation($"An error occurred, {ex.Message}");
                return null;
            }

        }

    }
}
